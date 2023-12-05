#nullable disable
using Markdig;

namespace ChatrBox.CoreComponents.API
{
    public class Cheddar
    {
        public static string SystemUserId { get; set; }

        public static bool IsCheddar(string Id)
        {
            return SystemUserId == Id;
        }

        public static MarkdownPipeline CheddarsPipeline { get; set; }
    }
}
