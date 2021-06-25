namespace OpenSoftware.OptionParsing
{
    public class PathOption : StringOption
    {
        /// <summary>
        /// Gets the path name passed to this PathOption or null if it wasn't specified.
        /// </summary>
        public string Path
        {
            get => Value;
            internal set => RawValue = value;
        }
    }
}