using ChatrBox.CoreComponents.ChatrBoxCommandsV1;

namespace UnitTests
{
    [TestClass]
    public class CommandParserV1
    {
        [TestMethod]
        public void ParseSingleCommand()
        {
            var parser = CommandParser.Create();
            var stringWithCommand = "some non-command content ~~!command; surrounding a command";
            var commands = parser.Parse(stringWithCommand);

            Assert.IsNotNull(commands);
            Assert.AreEqual(1, commands.Count);
            Assert.AreEqual("command", commands[0]);
        }
    }
}