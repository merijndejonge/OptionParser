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
                int value;
                int.TryParse(RawValue, out value);
                return value;
            }
            internal set => RawValue = value.ToString(CultureInfo.InvariantCulture);
        }
    }
}