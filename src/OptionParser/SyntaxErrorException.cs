using System;

namespace OpenSoftware.OptionParsing
{
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
}