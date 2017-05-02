namespace OpenSoftware.OptionParsing
{
    internal interface ICommandOption : ISwitchOption
    {
    }

    public class CommandOption<T> : SwitchOption<T>, ICommandOption
        where T : struct
    {
    }
}