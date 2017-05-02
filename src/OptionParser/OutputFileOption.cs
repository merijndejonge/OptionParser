using System;
using System.IO;
using System.Text;

namespace OpenSoftware.OptionParsing
{
    /// <summary>
    ///     Defines an option for an output file (i.e., for writer).
    /// </summary>
    [Option(Name = "--output", ShortName = "-o", Description = "Specifies output file name.")]
    public class OutputFileOption : FileOption
    {
        private Encoding _encoding = Encoding.GetEncoding(0); // Default encoding see (https://msdn.microsoft.com/en-us/library/wzsz3bk3%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396);
        private TextWriter _writer;

        /// <summary>
        ///     Returns the raw value (i.e., the literal string value) passed to this OutputFileOption.
        /// </summary>
        public override string RawValue
        {
            get => base.RawValue;
            internal set
            {
                if(RawValue != value)
                {
                    base.RawValue = value;
                    if (_writer == null) return;
                    _writer.Dispose();
                    _writer = null;
                }
            }
        }

        /// <summary>
        ///     Gets or sets the encoding for the output Writer.
        /// </summary>
        public Encoding Encoding
        {
            get => _encoding;
            set
            {
                if(!Equals(_encoding, value))
                {
                    _encoding = value;
                    if (_writer == null) return;
                    _writer.Dispose();
                    _writer = null;
                }
            }
        }

        /// <summary>
        ///     Returns the TextWriter according to the OutputFileOption or Console.Out if the option wasn't specified.
        /// </summary>
        public TextWriter Writer
        {
            get
            {
                if (_writer != null) return _writer;
                if(FileName == null)
                {
                    return Console.Out;
                }
                var stream = new FileStream(FileName, FileMode.Create);
                _writer = new StreamWriter(stream, Encoding);
                return _writer;
            }
        }
    }
}