using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenSoftware.OptionParsing;

namespace OptionParsing.Tests
{
    internal class BasicTypesOptionParser : OptionParser
    {
        public override string Name => nameof(Name) + "=" + nameof(BasicTypesOptionParser);
        public override string Description => nameof(Description) + "=" + nameof(BasicTypesOptionParser);

        [Option(Description = nameof(IntOption), Name = "--int")]
        public IntOption IntOption { get; set; }

        [Option(Description = nameof(BoolOption), Name = "--bool")]
        public BoolOption BoolOption { get; set; }

        [Option(Description = nameof(StringOption), Name = "--string")]
        public StringOption StringOption { get; set; }
        [Option(Description = nameof(GuidOption), Name = "--guid")]
        public GuidOption GuidOption { get; set; }

    }

    [TestClass]
    public class BasisTypesTests
    {
        [TestMethod]
        public void IntTest()
        {
            var options = new BasicTypesOptionParser();
            const int valueToTest = 5;
            options.Parse("--int", valueToTest.ToString());

            Assert.IsTrue(options.IntOption.IsDefined);
            Assert.IsFalse(options.BoolOption.IsDefined);
            Assert.IsFalse(options.StringOption.IsDefined);
            Assert.IsFalse(options.GuidOption.IsDefined);

            Assert.IsTrue(options.IntOption.Value == valueToTest);
        }

        [TestMethod]
        public void BoolTest()
        {
            var options = new BasicTypesOptionParser();
            const bool valueToTest = true;
            options.Parse("--bool", valueToTest.ToString());

            Assert.IsTrue(options.BoolOption.IsDefined);
            Assert.IsFalse(options.IntOption.IsDefined);
            Assert.IsFalse(options.StringOption.IsDefined);
            Assert.IsFalse(options.GuidOption.IsDefined);

            Assert.IsTrue(options.BoolOption.Value == valueToTest);
        }

        [TestMethod]
        public void BoolRawValueTest()
        {
            var options = new BasicTypesOptionParser();

            options.Parse();
            Assert.IsTrue(options.BoolOption.RawValue == bool.FalseString);
            Assert.IsFalse(options.BoolOption.IsDefined);
            Assert.IsFalse(options.BoolOption.Value);
            Assert.IsFalse(options.GuidOption.IsDefined);

            options.Parse("--bool");
            Assert.IsTrue(options.BoolOption.RawValue == bool.TrueString);
            Assert.IsTrue(options.BoolOption.IsDefined);
            Assert.IsTrue(options.BoolOption.Value);

            options.Parse("--bool", "true");
            Assert.IsTrue(options.BoolOption.RawValue == bool.TrueString);
            Assert.IsTrue(options.BoolOption.IsDefined);
            Assert.IsTrue(options.BoolOption.Value);

            options.Parse("--bool", "false");
            Assert.IsTrue(options.BoolOption.RawValue == bool.FalseString);
            Assert.IsTrue(options.BoolOption.IsDefined);
            Assert.IsFalse(options.BoolOption.Value);
            Assert.IsFalse(options.GuidOption.IsDefined);
        }

        [TestMethod]
        public void StringTest()
        {
            var options = new BasicTypesOptionParser();
            const string valueToTest = "hello";
            options.Parse("--string", valueToTest);

            Assert.IsTrue(options.StringOption.IsDefined);
            Assert.IsFalse(options.IntOption.IsDefined);
            Assert.IsFalse(options.BoolOption.IsDefined);
            Assert.IsFalse(options.GuidOption.IsDefined);

            Assert.IsTrue(options.StringOption.Value == valueToTest);
        }
        [TestMethod]
        public void GuidTest()
        {
            var options = new BasicTypesOptionParser();
            var valueToTest = Guid.NewGuid();
            
            options.Parse("--guid", valueToTest.ToString());

            Assert.IsTrue(options.GuidOption.IsDefined);
            Assert.IsFalse(options.StringOption.IsDefined);
            Assert.IsFalse(options.IntOption.IsDefined);
            Assert.IsFalse(options.BoolOption.IsDefined);

            Assert.IsTrue(options.GuidOption.Value == valueToTest);
        }
    }
}