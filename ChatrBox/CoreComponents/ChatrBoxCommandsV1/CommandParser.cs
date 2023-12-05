namespace ChatrBox.CoreComponents.ChatrBoxCommandsV1
{
    /// <summary>
    /// Future feature to implement - a means of issuing and responding to commands sent through the 
    /// message system.
    /// </summary>
    public class CommandParser
    {
        /// <summary>
        /// This is the character or sequence of characters that will be used to mark the 
        /// beginning of a command. This should be something that is unlikely to appear in
        /// ordinary conversations, but it also shouldn't be overly cumbersome to type. The 
        /// default delimiter is ~~!
        /// </summary>
        public static string CommandStartDelimter { get; set; } = "~~!";

        /// <summary>
        /// This is the character or sequence of characters that will be used to mark the 
        /// end of a command. This should be something that is unlikely to appear in
        /// ordinary conversations, but it also shouldn't be overly cumbersome to type. The 
        /// default delimiter is ;
        /// </summary>
        public static string CommandEndDelimter { get; set; } = ";";

        public static CommandParser Create()
        {
            var parser = new CommandParser();
            return parser;
        }

        public List<string> Parse(string input)
        {
            var result = new List<string>();

            while (input.Contains(CommandStartDelimter))
            {
                var length = input.IndexOf(CommandEndDelimter)- input.IndexOf(CommandStartDelimter) + CommandStartDelimter.Length;
                var command = input.Substring(input.IndexOf(CommandStartDelimter), length);
                result.Add(command);
                input = input.Remove(input.IndexOf(CommandStartDelimter) + CommandStartDelimter.Length, CommandStartDelimter.Length + length);
            }

            return result;
        }
    }
}
