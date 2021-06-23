using System;

namespace OpenSoftware.OptionParsing
{
    public class GuidOption : Option<Guid>
    {
        public override Guid Value
        {
            get => Guid.TryParse(RawValue, out var result) ? result : Guid.Empty;
            protected set => RawValue = value.ToString();
        }

        public override string RawValue
        {
            get => base.RawValue;
            protected set
            {
                if (Guid.TryParse(value, out var _) == false)
                {
                    throw new InvalidOptionValueException($@"{value} is not a valid GUID.");
                }
                base.RawValue = value;
            }
        }
    }
}