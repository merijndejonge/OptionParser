using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenSoftware.OptionParsing;

namespace OptionParsing.Tests
{
    internal class EnumOptionParser : OptionParser
    {
        internal enum EnumOptions
        {
            Value1,
            Value2,
            Value3
        };

        public override string Name => nameof(Name) + "=" + nameof(EnumOptionParser);
        public override string Description => nameof(Description) + "=" + nameof(EnumOptionParser);

        [Option(Description = nameof(EnumOption), Name = "-e")]
        public EnumOption<EnumOptions> EnumOption { get; set; }
    }

    [TestClass]
    public class EnumTests
    {
        [TestMethod]
        public void TestEnumValue()
        {
            var options = new EnumOptionParser();

            const EnumOptionParser.EnumOptions valueToTest = EnumOptionParser.EnumOptions.Value2;

            options.Parse("-e", valueToTest.ToString());

            Assert.IsTrue(options.EnumOption.IsDefined);
            Assert.IsTrue(options.EnumOption.Value == valueToTest);
        }

        [TestMethod]
        [ExpectedException(typeof(SyntaxErrorException))]
        public void TestThatEnumOptionOnlyAcceptsEnumValues()
        {
            var options = new EnumOptionParser();

            options.Parse("-e", "some_other_value");
        }
    }
}