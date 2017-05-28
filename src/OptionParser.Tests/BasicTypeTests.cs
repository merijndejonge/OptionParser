using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenSoftware.OptionParsing;

namespace OptionParsing.Tests
{
    internal class BasicTypesOptionParser : OptionParser
    {
        public override string Name => nameof(Name) + "=" + nameof(BasicTypesOptionParser);
        public override string Description => nameof(Description) + "=" + nameof(BasicTypesOptionParser);

        [Option(Description = nameof(IntOption), Name = "-i")]
        public IntOption IntOption { get; set; }

        [Option(Description = nameof(BoolOption), Name = "-b")]
        public BoolOption BoolOption { get; set; }

        [Option(Description = nameof(StringOption), Name = "-s")]
        public StringOption StringOption { get; set; }
    }

    [TestClass]
    public class BasisTypesTests
    {
        [TestMethod]
        public void IntTest()
        {
            var options = new BasicTypesOptionParser();
            const int valueToTest = 5;
            options.Parse("-i", valueToTest.ToString());

            Assert.IsTrue(options.IntOption.IsDefined);
            Assert.IsFalse(options.BoolOption.IsDefined);
            Assert.IsFalse(options.StringOption.IsDefined);

            Assert.IsTrue(options.IntOption.Value == valueToTest);
        }

        [TestMethod]
        public void BoolTest()
        {
            var options = new BasicTypesOptionParser();
            const bool valueToTest = true;
            options.Parse("-b", valueToTest.ToString());

            Assert.IsTrue(options.BoolOption.IsDefined);
            Assert.IsFalse(options.IntOption.IsDefined);
            Assert.IsFalse(options.StringOption.IsDefined);

            Assert.IsTrue(options.BoolOption.Value == valueToTest);
        }

        [TestMethod]
        public void StringTest()
        {
            var options = new BasicTypesOptionParser();
            const string valueToTest = "hello";
            options.Parse("-s", valueToTest);

            Assert.IsTrue(options.StringOption.IsDefined);
            Assert.IsFalse(options.IntOption.IsDefined);
            Assert.IsFalse(options.BoolOption.IsDefined);

            Assert.IsTrue(options.StringOption.Value == valueToTest);
        }
    }
}