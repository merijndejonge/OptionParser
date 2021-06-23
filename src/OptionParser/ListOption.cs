using System.Collections.Generic;
using System.Linq;
using Dawn;

namespace OpenSoftware.OptionParsing
{
    public class ListOption<T> : Option<List<T>>
    {
        public override List<T> Value { get; protected set; }


        public override string RawValue
        {
            get
            {
                var stringArray = Value.Select(x => x.ToString());

                return string.Join(",", stringArray);

            }
            protected set
            {
                var list = new List<T>();
                var stringValues = value.Split(",");
                foreach (var stringValue in stringValues)
                {
                    if (TryParseValue(stringValue, out var result) == false)
                    {
                        throw new SyntaxErrorException($"Invalid value {stringValue} for type {typeof(T).Name}");
                    }

                    list.Add(result);
                }

                Value = list;
            }
        }
        private static bool TryParseValue(string stringValue, out T result) =>
            ValueString.Of(stringValue).Is<T>(out result);
    }
}