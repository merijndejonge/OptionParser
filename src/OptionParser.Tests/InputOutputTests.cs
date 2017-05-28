using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenSoftware.OptionParsing;

namespace OptionParsing.Tests
{
    internal class InputOutputOptionsParser : OptionParser
    {
        public override string Name => nameof(Name) + "=" + nameof(InputOutputOptionsParser);
        public override string Description => nameof(Description) + "=" + nameof(InputOutputOptionsParser);

        [Option(Description = nameof(InputFileOption), Name = "-i")]
        public InputFileOption InputFileOption { get; set; }

        [Option(Description = nameof(OutputFileOption), Name = "-o")]
        public OutputFileOption OutputFileOption { get; set; }

        [Option(Description = nameof(PathOption), Name = "-p")]
        public PathOption PathOption { get; set; }
    }

    [TestClass]
    public class InputOutputTests
    {
        [TestMethod]
        public void TestThatInputFileOptionOpensRightFile()
        {
            var options = new InputOutputOptionsParser();

            const string valueToTest = "./inputFileOptionTestFile.txt";

            options.Parse("-i", valueToTest);

            Assert.IsTrue(options.InputFileOption.IsDefined);
            Assert.IsTrue(options.InputFileOption.FileName == valueToTest);

            var text = options.InputFileOption.Reader.ReadLine();
            Assert.IsTrue(text.Contains("Hello World!"));
        }

        [TestMethod]
        public void TestThatOutputFileOptionOpensRightFile()
        {
            var options = new InputOutputOptionsParser();

            const string valueToTest = "./outputFileOptionTestFile.txt";

            options.Parse("-o", valueToTest);

            Assert.IsTrue(options.OutputFileOption.IsDefined);
            Assert.IsTrue(options.OutputFileOption.FileName == valueToTest);

            const string text = "Hellow Worl!";
            options.OutputFileOption.Writer.WriteLine(text);
        }

        [TestMethod]
        public void TestPathOption()
        {
            var options = new InputOutputOptionsParser();

            const string valueToTest = "foo/bar";

            options.Parse("-p", valueToTest);

            Assert.IsTrue(options.PathOption.IsDefined);
            Assert.IsTrue(options.PathOption.Path == valueToTest);
        }
    }
}