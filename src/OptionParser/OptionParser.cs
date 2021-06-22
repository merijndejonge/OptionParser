using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace OpenSoftware.OptionParsing
{
    /// <summary>
    ///     Abstract option parser class.
    /// </summary>
    public abstract class OptionParser
    {
        private const string SwitchEscape = "--";

        /// <summary>
        ///     Creates a new instance of the OptionParser class.
        /// </summary>
        /// <param name="args">An array of strings holding the command line arguments and switches</param>
        protected OptionParser(IEnumerable<string> args)
        {
            Parse(args);
        }

        protected OptionParser()
        {
            Initialize();
        }

        /// <summary>
        ///     Returns the name of the application.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        ///     Returns a description of the application
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        ///     Returns the collection of remaining (unparsed) command line arguments after processing.
        /// </summary>
        public List<string> Arguments { get; private set; }

        /// <summary>
        ///     Returns the collection of switches defined in the CommandLineParser
        /// </summary>
        public List<Option> Options { get; private set; }

        /// <summary>
        ///     Gets a list of all the options that have successfully been processed.
        /// </summary>
        public List<Option> ProcessedOptions { get; private set; }

        [Option(Name = "--help", ShortName = "/?", Description = "Shows this usage information.", Index = 999)]
        public BoolOption Usage { get; set; }

        protected virtual string Copyright => null;

        private void Initialize()
        {
            SwitchValues.Reset();
            Options = new List<Option>();
            ProcessedOptions = new List<Option>();
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public |
                                              BindingFlags.FlattenHierarchy;
            var optionProperties =
                GetType().GetProperties(bindingFlags).Where(prop => typeof(Option).IsAssignableFrom(prop.PropertyType));
            foreach (var prop in optionProperties)
            {
                var option = (Option)Activator.CreateInstance(prop.PropertyType);
                var optionAttr = (OptionAttribute)prop.GetCustomAttributes(typeof(OptionAttribute), true).SingleOrDefault();
                if (optionAttr != null)
                {
                    option.SetValues(optionAttr);
                }
                if (option.Name == null)
                {
                    throw new SyntaxErrorException($"{prop.Name}.Name ({prop.PropertyType.Name}) property cannot be null.");
                }
                prop.SetValue(this, option, null);
                Options.Add(option);
            }
        }

        public void Parse(params string[] args)
        {
            Parse(args.ToList());
        }
        public void Parse(IEnumerable<string> args)
        {
            Initialize();
            Arguments = args.ToList();
            ProcessArguments();
            ValidateArguments();
        }

        /// <summary>
        ///     Override this method to implement validation of options and arguments after processing.
        /// </summary>
        protected virtual void ValidateArguments()
        {
            if(Usage.IsDefined || HasUnprocessedHelp())
            {
                // Stop validation when '--help' switch is used 
                return;
            }

            // Check if all required options have been specified.
            foreach(var option in Options.Where(arg => arg.Required))
            {
                if(ProcessedOptions.Contains(option) == false)
                {
                    throw new SyntaxErrorException($"Required switch `{option.Name}' is missing.");
                }
            }
        }

        private bool HasUnprocessedHelp()
        {
            return Arguments.Any(x => x == "--help" || x == "/?");
        }
        private void ProcessArguments()
        {
            while(Arguments.Count > 0)
            {
                // When we detect the SwitchEscape symbol, we stop processing.
                if(Arguments[0] == SwitchEscape)
                {
                    Arguments.RemoveAt(0);
                    break;
                }
                var cmdOption =
                    Options.FirstOrDefault(
                        option => option.Name == Arguments[0] || option.ShortName == Arguments[0]);
                if(cmdOption == null)
                {
                    throw new SyntaxErrorException($"Unknown switch `{Arguments[0]}'");
                }

                if(cmdOption is ISwitchOption)
                {
                    TrySetValue(cmdOption, cmdOption.EnumValue);
                }
                    // A boolean switch, e.g., '-v' can be specified as an unary switch '-v' or as a binary switch '-v [true|false]'
                else if(cmdOption is BoolOption && (Arguments.Count == 1 || IsSwitch(Arguments[1])))
                {
                    TrySetValue(cmdOption, bool.TrueString);
                }
                else
                {
                    if(Arguments.Count == 1)
                    {
                        throw new SyntaxErrorException($"Switch `{cmdOption.Name}' requires an argument.");
                    }
                    if(IsSwitch(Arguments[1]))
                    {
                        throw new SyntaxErrorException($"Switch '{cmdOption.Name}' requires an argument.");
                    }
                    TrySetValue(cmdOption, Arguments[1]);
                    Arguments.RemoveAt(0);
                }
                Arguments.RemoveAt(0);
                ProcessedOptions.Add(cmdOption);
                if(cmdOption is ICommandOption)
                {
                    break; // Stop option handling when reaching a subcommand switch
                }
            }
        }

        private static void TrySetValue(Option cmdOption, string value)
        {
            try
            {
                cmdOption.SetRawValue(value);
                cmdOption.IsDefined = true;
            }
            catch(InvalidOptionValueException e)
            {
                throw new SyntaxErrorException($@"The value ""{value}"" is invalid for switch ""{cmdOption.Name}""", e);
            }
        }

        private bool IsSwitch(string swStr)
        {
            return Options.Any(option => option.Name == swStr || option.ShortName == swStr) || swStr == SwitchEscape;
        }

        /// <summary>
        ///     Writes the usage information to the output stream.
        /// </summary>
        /// <param name="writer"></param>
        public void DisplayUsage(TextWriter writer)
        {
            DisplayUsage(writer, Console.IsOutputRedirected ? 80 : Console.WindowWidth);
        }

        /// <summary>
        ///     Writes application's usage information to the provided TextWriter. Output
        ///     will be formatted with the indicated width.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="width"></param>
        public void DisplayUsage(TextWriter writer, int width)
        {
            if(Copyright != null)
            {
                writer.WriteLine(WordWrap(Copyright, width, 0));
                writer.WriteLine();
            }
            writer.WriteLine("{0} -- {1}", Name, WordWrap(Description, width, Name.Length + 4));
            var usageText = new Dictionary<string, string>();

            writer.WriteLine();
            writer.WriteLine("Usage:");
            var options = Options.OrderBy(x => x.Index).ThenBy(x => x.Name).ToList();
            foreach(var option in options)
            {
                var key = option.Name;
                if(option.ShortName != null)
                    key += " (" + option.ShortName + ")";
                var description = option.Description;
                if(option.Required)
                    description += " (required).";
                if(option.DefaultValue != null)
                    description += " (default: " + option.DefaultValue + ").";
                usageText.Add(key, description);
            }
            var keyLength = usageText.Keys.Max(k => k.Length);
            const string separator = "    ";
            foreach(var line in usageText)
            {
                writer.WriteLine("{0}{1}{2}",
                                 line.Key.PadRight(keyLength),
                                 separator,
                                 WordWrap(line.Value, width, keyLength + separator.Length));
            }
        }

        private static string WordWrap(string str, int width, int indent)
        {
            if(str == null)
                return null;
            var prefix = "";
            for(var i = 0; i < indent; i++)
                prefix += " ";

            var words = str.Split(' ');
            var sw = new StringWriter();
            var line = "";
            var lineLength = 0;
            foreach (var word in words)
            {
                if(lineLength + word.Length + 1 >= width - indent)
                {
                    if(str.StartsWith(line) == false)
                    {
                        sw.Write(prefix);
                    }
                    sw.WriteLine(line);
                    line = "";
                    lineLength = 0;
                }
                if(lineLength > 0)
                {
                    line += " ";
                    lineLength++;
                }
                line += word;
                lineLength += word.Length;
            }
            if(str.StartsWith(line) == false)
            {
                sw.Write(prefix);
            }
            sw.Write(line);
            var s = sw.ToString();
            return s;
        }
    }

    public class SyntaxErrorException : Exception
    {
        public SyntaxErrorException(string message) : base(message)
        {
        }

        public SyntaxErrorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class InvalidOptionValueException : Exception
    {
        public InvalidOptionValueException(string message)
            : base(message)
        {
        }
    }
}