using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenSoftware.OptionParsing;

namespace OptionParsing.Tests
{
    internal class BasicOptionParser : OptionParser
    {
        public override string Name => nameof(Name) + "=" + nameof(BasicOptionParser);
        public override string Description => nameof(Description) + "=" + nameof(BasicOptionParser);

        [Option(Description = nameof(MyIntOption), Name = "--int-option", ShortName = "-i", DefaultValue = "3")]
        public IntOption MyIntOption { get; set; }

        [Option(Description = nameof(MyRequiredOption), Name = "--required-option", ShortName = "-r",
            IsRequired = true)]
        public BoolOption MyRequiredOption { get; set; }
    }

    [TestClass]
    public class BasisTests
    {
        [TestMethod]
        public void TestThatUsesSwitchIsParsed()
        {
            var options = new BasicOptionParser();

            options.Parse("--required-option");
            Assert.IsFalse(options.Usage.IsDefined);

            options.Parse("--required-option", "--help");
            Assert.IsTrue(options.Usage.IsDefined);
        }
        [TestMethod]
        public void TestThatUsesSwitchBypassesOptionValidation()
        {
            var options = new BasicOptionParser();

            options.Parse("--help");
            Assert.IsTrue(options.Usage.IsDefined);
        }
        [TestMethod]
        public void TestThatUsageContainsNameDescriptionLongNameAndShortName()
        {
            var options = new BasicOptionParser();
            var stringWriter = new StringWriter();
            options.DisplayUsage(stringWriter);

            var result = stringWriter.ToString();

            Assert.IsTrue(result.Contains(options.Name));
            Assert.IsTrue(result.Contains(options.Description));
            Assert.IsTrue(result.Contains("--required-option"));
            Assert.IsTrue(result.Contains("-r"));
        }

        [TestMethod]
        public void TestThatLongNameParsesCorrectly()
        {
            var options = new BasicOptionParser();
            options.Parse("--required-option");

            Assert.IsTrue(options.MyRequiredOption.IsDefined);
        }

        [TestMethod]
        public void TestThatShortNameParsesCorrectly()
        {
            var options = new BasicOptionParser();
            options.Parse("-r");

            Assert.IsTrue(options.MyRequiredOption.IsDefined);
        }

        [TestMethod]
        public void TestForRequiredOptions()
        {
            var options = new BasicOptionParser();
            Assert.ThrowsException<SyntaxErrorException>(() => options.Parse());
        }

        [TestMethod]
        public void TestThatDefaultValueIsUsed()
        {
            var options = new BasicOptionParser();

            Assert.IsTrue(options.MyIntOption.DefaultValue == "3");
            Assert.IsTrue(options.MyIntOption.Value == 3);

            options.Parse("--required-option");
            Assert.IsFalse(options.MyIntOption.IsDefined);
            Assert.IsTrue(options.MyIntOption.Value == 3);
        }

        [TestMethod]
        public void TestThatSwitchOverridesDefaultValue()
        {
            var options = new BasicOptionParser();

            options.Parse("--required-option", "-i", "5");
            Assert.IsTrue(options.MyIntOption.IsDefined);
            Assert.IsTrue(options.MyIntOption.Value == 5);
        }
    }
}