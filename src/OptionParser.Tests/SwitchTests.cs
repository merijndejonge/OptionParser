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

        [Option(Description = "SwitchOptionTest", Name = "-s")]
        public SwitchOption<SwitchValues> SwitchOption { get; set; }
    }

    [TestClass]
    public class SwitchTests
    {
        [TestMethod]
        public void TestSwitchValue()
        {
            var options = new SwitchOptionParser();
            const SwitchOptionParser.SwitchValues valueToTest = SwitchOptionParser.SwitchValues.Value2;

            options.Parse("-s");

            //Assert.IsTrue(options.SwitchOption.IsDefined);
            //Assert.IsTrue(options.SwitchOption.Value == valueToTest);
            //Assert.IsTrue(options.SwitchOption.Enumvalue == valueToTest.ToString());
        }
    }
}