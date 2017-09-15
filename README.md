# OptionParser


## Description
OptionParser is a library that deals with option parsing and option handling for  command-line (CLI) programs.

Options are defined in a class that derives from `OptionParser`, where each option is declared as a property and has a type that derives from `Option`. Meta-information about the options (such as its name) are defined using attributes. The library comes with  multiple pre-defined option types, which will enable you to get started quickly. See below for a complete list of option types.

## Basic Usage
Below we will describe the few necessary steps to get the option parser to run in your application.

### Creating an option parser
The first step in using OptionParser is to create a class that derives from `OptionParser` that defines the different switches of your application. Below is a small example:

```csharp
    public class ExampleOptionParser : OptionParser
    {
        public override string Name => "Option parser example";
        public override string Description => "This application provides a basic example of the OptionParser library";

        [Option(Description = "Specifies the input size in number of bytes", Name = "--input-size", ShortName = "-i")]
        public IntOption InputSize { get; set; }

        [Option(Description = "Specifies whether or not binary mode should be used", Name = "--binary-mode", ShortName = "-b")]
        public BoolOption BinaryMode { get; set; }

        [Option(Description = "Specifies the source location", Name = "--source-location", ShortName = "-s")]
        public StringOption SourceLocation { get; set; }
    }
```
This class defines an option parser with a specific name and description. It specifies three options. There is an `IntOption` to specify the input size, a `BoolOption` to specify binary mode, and there is a `StrngOption` to specify the source location. The attributes for each option specify meta data. In this example, the description of each option and its syntax (as long name and short name).

### Parsing command line arguments
To use this option parser you instantiate the class and use its `Parse` method as follows:

```csharp
    public static void Main(string[] args)
    {
        var options = new ExampleOptionParser();
        options.Parse(args);
    }
```

This example shows how the array of command line parameters `args` is passed to the `Parse` method. For testing purposes, you could also pass your own set of switches to the parser. E.g.:
```csharp
    options.Parse("-i", "10", "--binary-mode");
```
### Generated usage instructions
The meta information defined in your parser class also serves to build default usage instructions. If you run your application with the `--help` or `/?` switch, the parser writes usage instructions of your application to standard error. 

```
Option parser example -- This application provides a basic example of the
                         OptionParser library

Usage:
--binary-mode (-b)        Specifies whether or not binary mode should be used
--input-size (-i)         Specifies the input size in number of bytes
--source-location (-s)    Specifies the source location
--help (/?)               Shows this usage information. (default: false).
```

### Consuming parsed options
After parsing, the different properties of `ExampleOptionParser` are instantiated according to the command line arguments that are passed to the parser.

For instance, to get the input size use:
```csharp
    int input_size = options.InputSize.Value;
```
If this switch wasn't used by the user, `options.InputSize.IsDefined` is false. This is because options are optional by default. To make an option mandatory, specify `IsRequired = true` for the option:
```csharp
        [Option(Description = "Specifies the input size in number of bytes", Name = "--input-size", ShortName = "-i", IsRequired = true)]
        public IntOption InputSize { get; set; }
```
If the user does not specify the switch an exception is which you need to handle in your program.

## Advanced usage
The library supports more advanced option handling than the getting-started example shows. For instance, it supports enum types,file options, as well as sub commands. The source code of `OptionParser` contain unit tests for each feature, demonstrating their usage.

## The OptionParser class

The class `OptionParser` forms the base class of every option parser. It has two constructors:
* `protected OptionParser(IEnumerable<string> args)`
* `protected OptionParser()`

The first constructor automatically parses the list of arguments `args`. The second constructor only performs required initialization, while the parsing has to be done explicitly by calling the `Parse` methods.

The class defines the following abstract properties that must be defined in a base class:
* `Name` Specifies the name of the application. This is used in the generated usage instructions.
* `Description` Provides a short description about the program. This information is used in the generated usage instructions.

The following option can optionally be defined in a base class:
* `Copyright` Provides copyright information about the program. This information is used in the generated usage instruction.

Explicit parsing is done by choosing one of the following parse methods:
* `public void Parse(params string[] args`
* `Parse(IEnumerable<string> args)`

The first is typically used for unit testing where you can pass a sequence of command line arguments to the parse method. The second method is typically used to parse the array of command line argument from your `Main` method.

To display usage information you can choose between two variants:
* `public void DisplayUsage(TextWriter writer)`
* `public void DisplayUsage(TextWriter writer, int width)`

The first uses a default line width based on the console window width, for the second variant you specify the width explicitly.

There are three properties available to further inspect the parser and the parse result:
* `public List<Option> Options` Returns the collection of switched defined in your option parser.
* `public List<Option> ProcessedOptions` Returns the collection of all the options that have successfully been processed.
* `public List<string> Arguments` Returns the collection of remaining (unparsed) command line arguments after parsing.

## Option Metadata
Metadata of options is specified using the `OptionAttribute`. It has the following properties:
* `DefaultValue` Specifies a default value for the option.
* `Description` Specifies a description for the option.
* `EnumValue` For enumeration-based options (e.g., SwitchOption and CommandOption) this attribute specifies the enum value to which this option corresponds.
* `Index` Specifies the index in the help overview where this option should appear. 
* `IsRequired` Indicates whether an option is mandatory or not.
* `Name`  Specifies the name of the option (e.g., '--input-size').
* `ShortName` Specifies the abbreviated name of the option (e.g., '-i').

## Option Types
Below is the list of predefined option types.
* `BoolOption` Defines a boolean switch. It accepts a value of true or false,but specifying this value is required. The evaluated value is accessible through the `Value` property. 
* `CommandOption` This is an advanced feature of `OptionParser` which enables the definition of commands with different sets of switches. Please look at the unit tests for further information.
* `EnumOption` This option type serves to define a fixed set of accepted values.
* `FileOption` Extends `StringOption` with a `FileName` property.
* `InputFileOption` Extends `FileOption` with a `Reader` property of type `TextReader` which allows to easily access the file that was specified on the command line.
* `IntOption` Defines an integer switch. The evaluated value is accessible through the `Value` property.
* `OutputFileOption` Extends `FileOption` with a `Writer` property of type `TextWriter` which allows to easily access the file that was specified on the command line. The property `Encoding` allows you to write a file in the correct encoding.
* `PathOption` Extends `StringOption` with a `Path` property.
* `StringOption` Defines a string switch. The evaluated value is accessible through the `Value` property. 
* `SwitchOption` This is an advanced feature of `OptionParser` that serves to define a group of command line switch one of which may be used at a time. Please look at the unit tests for examples of using this feature.

## More info
Source code of `OptionParser` is available at [GitHub](https://github.com/merijndejonge/OptionParser). Nuget packages are available at [Nuget.org](https://www.nuget.org/packages/OptionParser).

`OptionParser` is distributed under the [Apache 2.0 License](https://github.com/merijndejonge/OptionParser/blob/master/LICENSE).
