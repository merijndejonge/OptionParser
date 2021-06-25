using System;
using System.IO;

namespace OpenSoftware.OptionParsing
{
    /// <summary>
    ///     Defines an option for an input file (i.e., for reading).
    /// </summary>
    [Option(Name = "--input", ShortName = "-i", Description = "Specifies input file name.")]
    public class InputFileOption : FileOption
    {
        private TextReader _reader;

        /// <summary>
        ///     Returns the raw value (i.e., the literal string value) passed to this InputFileOption.
        /// </summary>
        public override string RawValue
        {
            get => base.RawValue;
            protected set
            {
                if(RawValue != value)
                {
                    base.RawValue = value;
                    if (_reader == null) return;
                    _reader.Dispose();
                    _reader = null;
                }
            }
        }

        /// <summary>
        ///     Returns the TextReader according to the InputFileOption or Console.In if the option wasn't specified.
        /// </summary>
        public TextReader Reader
        {
            get
            {
                if (_reader != null) return _reader;
                _reader = FileName == null ? new StreamReader(Console.OpenStandardInput(), true) : File.OpenText(FileName);
                return _reader;
            }
        }
    }
}