namespace OpenSoftware.OptionParsing
{
    public interface IEnumOption
    {
    }

    public class EnumOption<T> : Option<T>, IEnumOption
        where T : struct
    {
    }
}