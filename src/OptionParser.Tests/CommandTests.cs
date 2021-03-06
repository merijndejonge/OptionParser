﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenSoftware.OptionParsing;

namespace OptionParsing.Tests
{
    internal class CommandOptionParser : OptionParser
    {
        internal enum CommandValues
        {
            Command1,
            Command2,
            Command3
        };

        public override string Name => nameof(Name) + "=" + nameof(CommandOptionParser);
        public override string Description => nameof(Description) + "=" + nameof(CommandOptionParser);

        [Option(Description = nameof(IntOption), Name = "-i")]
        public IntOption IntOption { get; set; }

        [Option(Description = nameof(Command2), Name = nameof(CommandValues.Command2),
            EnumValue = nameof(CommandValues.Command2))]
        public CommandOption<CommandValues> Command2 { get; set; }

        public CommandValues CommandValue => SwitchOption<CommandValues>.GetValue();
    }

    [TestClass]
    public class CommandTests
    {
        [TestMethod]
        public void TestCommand()
        {
            var options = new CommandOptionParser();
            const CommandOptionParser.CommandValues commandToTest = CommandOptionParser.CommandValues.Command2;

            options.Parse(commandToTest.ToString());

            Assert.IsTrue(options.Command2.IsDefined);
            Assert.IsTrue(options.CommandValue == commandToTest);
        }

        [TestMethod]
        public void TestThatCommandOptionLeavesRemainingSwitchesUnchanged()
        {
            var options = new CommandOptionParser();
            const CommandOptionParser.CommandValues commandToTest = CommandOptionParser.CommandValues.Command2;

            options.Parse(commandToTest.ToString(), "-i", "10");

            Assert.IsTrue(options.Command2.IsDefined);
            Assert.IsTrue(options.Arguments.Count == 2);
        }
    }
}