namespace OpenSoftware.OptionParsing
{
    public class FileOption : StringOption
    {
        /// <summary>
        /// Gets the file name passed to this FolderOption or null if it wasn't specified.
        /// </summary>
        public string FileName
        {
            get => Value;
            internal set => RawValue = value;
        }
    }
}