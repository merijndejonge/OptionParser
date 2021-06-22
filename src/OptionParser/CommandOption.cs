namespace OpenSoftware.OptionParsing
{
    internal interface ICommandOption : ISwitchOption
    {
    }
    /// <summary>
    /// This class is a special form of <see cref="SwitchOption{T}"/>. It stops processing arguments 
    /// once a command has been detected. The remaining arguments can be accessed through the 
    /// <c>Arguments</c> property of the containing <see cref="OptionParser"/> class. This enables 
    /// commanding by chaining different options parser, one for each command.
    /// </summary>
    /// <typeparam name="T">Specifies enum type that holds different command values</typeparam>
    public class CommandOption<T> : SwitchOption<T>, ICommandOption
        where T : struct
    {
    }
}