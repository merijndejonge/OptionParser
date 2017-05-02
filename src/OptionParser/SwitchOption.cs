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