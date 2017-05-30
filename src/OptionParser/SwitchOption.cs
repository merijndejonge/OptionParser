using System;
using System.Collections.Generic;

namespace OpenSoftware.OptionParsing
{
    internal interface ISwitchOption{ }

    /// <summary>
    /// Class that holds a static dictionary of switch values.
    /// </summary>
    internal static class SwitchValues
    {
        internal static readonly Dictionary<Type, string> SwitchValue =
            new Dictionary<Type, string>();

        internal static void Reset()
        {
            SwitchValue.Clear();
        }
    }
    /// <summary>
    /// This class represents an option that selects a value from a collection of enum values defined by <typeparamref name="T"/> .
    /// Multiple instances of this class can be used to reference different values of <typeparamref name="T"/>.
    /// 
    /// To access the selected enum value use the static method <c>SwitchOption&lt;T&gt;</c>.GetValue().
    /// </summary>
    /// <typeparam name="T">Specifies enum type that holds different switch values</typeparam>
    public class SwitchOption<T> : EnumOption<T>, ISwitchOption
        where T : struct
    {
        public override string RawValue
        {
            get
            {
                var thisType = typeof(T);
                return SwitchValues.SwitchValue.ContainsKey(thisType) ? SwitchValues.SwitchValue[thisType] : null;
            }
            internal set
            {
                if(IsDefined)
                {
                    throw new InvalidOptionValueException("You can only select a single value for a switch.");
                }
                var thisType = typeof(T);
                SwitchValues.SwitchValue[thisType] = value;
            }
        }
       
        public static T GetValue()
        {
            var key = typeof(T);
            if(SwitchValues.SwitchValue.ContainsKey(key) == false)
            {
                return default(T);
            }
            var stringValue =  SwitchValues.SwitchValue[key];
            return (T)Enum.Parse(typeof(T), stringValue, true);
        }
    }
}