using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenSoftware.OptionParsing;

namespace OptionParsing.Tests
{
    internal class SwitchOptionParser : OptionParser
    {
        internal enum SwitchValues
        {
            Value1,
            Value2,
            Value3
        };

        public override string Name => "Name=" + nameof(SwitchOptionParser);
        public override string Description => "Description=" + nameof(SwitchOptionParser);

        [Option(Name = "value1", EnumValue = nameof(SwitchValues.Value1))]
        public SwitchOption<SwitchValues> SwitchOptionValue1 { get; set; }

        [Option(Name = "value2", EnumValue = nameof(SwitchValues.Value2))]
        public SwitchOption<SwitchValues> SwitchOptionValue2 { get; set; }

        public SwitchValues SwitchValue => SwitchOption<SwitchValues>.GetValue();
    }

    [TestClass]
    public class SwitchTests
    {
        [TestMethod]
        public void TestSwitchValue()
        {
            var options = new SwitchOptionParser();
            const SwitchOptionParser.SwitchValues valueToTest = SwitchOptionParser.SwitchValues.Value2;

            options.Parse(valueToTest.ToString().ToLower());

            var value = options.SwitchValue;

            Assert.IsFalse(options.SwitchOptionValue1.IsDefined);
            Assert.IsTrue(options.SwitchOptionValue2.IsDefined);
            Assert.IsTrue(value == valueToTest);
        }

        [TestMethod]
        [ExpectedException(typeof(SyntaxErrorException))]
        public void TestThatOnlyOneSwitchValueCanBeSet()
        {
            var options = new SwitchOptionParser();
            options.Parse("value1", "value2");
        }
    }
}