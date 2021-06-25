using System;

namespace OpenSoftware.OptionParsing
{
    internal class InvalidOptionValueException : Exception
    {
        public InvalidOptionValueException(string message)
            : base(message)
        {
        }
    }
}