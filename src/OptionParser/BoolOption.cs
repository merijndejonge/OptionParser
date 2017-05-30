﻿namespace OpenSoftware.OptionParsing
{
    /// <summary>
    ///     Defines a boolean option.
    /// </summary>
    public class BoolOption : Option<bool>
    {
        /// <summary>
        ///     Gets the value passed to this BoolOption or null if it wasn't specified.
        /// </summary>
        public override bool Value
        {
            get
            {
                bool.TryParse(RawValue, out bool result);
                return result;
            }
            internal set => RawValue = value.ToString();
        }

        /// <summary>
        ///     Returns true if the option is specified on the command line, false otherwise.
        /// </summary>
        public override bool IsDefined => Value;
    }
}