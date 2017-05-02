using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenSoftware.OptionParsing;

namespace OptionParsing.Tests
{
    public class BasicOptionParser: OptionParser
    {
        public override string Name => "Name="+nameof(BasicOptionParser);
        public override string Description => "Description="+nameof(BasicOptionParser);

        [Option(Description = "MyIntOption", Name = "--int-option", ShortName = "-i", DefaultValue = "3" )]
        public IntOption MyIntOption { get; set; }

        [Option(Description = "RequiredOption", Name = "--required-option", ShortName = "-r", IsRequired = true)]
        public BoolOption MyRequiredOption{ get; set; }
    }

    [TestClass]
    public class BasisTests
    {
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

            Assert.IsTrue(options.MyRequiredOption.IsDefined );
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

            Assert.IsTrue(options.MyIntOption.DefaultValue == "3" );
        }
    }
}
