namespace OpenSoftware.OptionParsing
{
    /// <summary>
    ///     Defines a string option.
    /// </summary>
    public class StringOption : Option<string>
    {
        /// <summary>
        ///     Gets the value passed to this StringOption or null if it wasn't specified.
        /// </summary>
        public override string Value
        {
            get => RawValue;
            protected set => RawValue = value;
        }
    }
}