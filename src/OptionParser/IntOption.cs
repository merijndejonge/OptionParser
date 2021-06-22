using System.Globalization;

namespace OpenSoftware.OptionParsing
{
    /// <summary>
    ///     Defines an int option.
    /// </summary>
    public class IntOption : Option<int>
    {
        /// <summary>
        ///     Gets the value passed to this IntOption or null if it wasn't specified.
        /// </summary>
        public override int Value
        {
            get
            {
                int.TryParse(RawValue, out var value);
                return value;
            }
            protected set => RawValue = value.ToString(CultureInfo.InvariantCulture);
        }
    }
}