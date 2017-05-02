namespace OpenSoftware.OptionParsing
{
    /// <summary>
    ///     Defines a filename option.
    /// </summary>
    public abstract class FileOption : StringOption
    {
        /// <summary>
        ///     Gets the file name passed to this FileOption or null if it wasn't specified.
        /// </summary>
        public string FileName
        {
            get => Value;
            internal set => Value = value;
        }
    }
}