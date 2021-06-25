using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenSoftware.OptionParsing;

namespace OptionParsing.Tests
{
    internal class ListOptionParser : OptionParser
    {

        public override string Name => nameof(Name) + "=" + nameof(ListOptionParser);
        public override string Description => nameof(Description) + "=" + nameof(ListOptionParser);

        [Option(Description = nameof(ListOptionParser), Name = "--int")]
        public ListOption<int> IntListOption { get; set; }
        [Option(Description = nameof(ListOptionParser), Name = "--guid")]
        public ListOption<Guid> GuidListOption { get; set; }

    }

    [TestClass]
    public class ListOptionTests
    {
        [TestMethod]
        public void TestIntListOptionValue()
        {
            var options = new ListOptionParser();

            const int int1 = 1;
            const int int2 = 2;
            const int int3 = 3;
            
            var args = string.Join(",", int1, int2, int3);
            options.Parse("--int", args);
            var values = options.IntListOption.Value;
            
            Assert.IsTrue(values.Count == 3);
            Assert.IsTrue(values[0] == int1);
            Assert.IsTrue(values[1] == int2);
            Assert.IsTrue(values[2] == int3);
        }
        [TestMethod]
        public void TestGuidListOptionValue()
        {
            var options = new ListOptionParser();

            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var guid3 = Guid.NewGuid();

            var args = string.Join(",", guid1, guid2, guid3);
            options.Parse("--guid", args);
            var values = options.GuidListOption.Value;
            
            Assert.IsTrue(values.Count == 3);
            Assert.IsTrue(values[0] == guid1);
            Assert.IsTrue(values[1] == guid2);
            Assert.IsTrue(values[2] == guid3);
        }
    }
}