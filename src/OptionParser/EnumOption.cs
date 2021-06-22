using System;

namespace OpenSoftware.OptionParsing
{
    public class EnumOption<T> : Option<T>
        where T : struct
    {
        #region Overrides of Option<T>
        public override T Value
        {
            get
            {
                if(Enum.TryParse(RawValue, true, out T result) == false)
                {
                    return default(T);
                }
                return result;
            }
            protected set => RawValue = value.ToString();
        }

        #endregion

        public override string RawValue
        {
            get => base.RawValue;
            internal set
            {
                if (Enum.TryParse(value, true, out T _) == false)
                {
                    var acceptedValues = string.Join(@""", """, Enum.GetNames(typeof(T)));
                    throw new InvalidOptionValueException($@"Accepted values are: ""{acceptedValues}"".");
                }
                base.RawValue = value;
            }
        }
    }
}