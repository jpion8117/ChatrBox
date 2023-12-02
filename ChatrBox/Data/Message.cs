#nullable disable
using Markdig;
using Markdig.Extensions.MediaLinks;
using Markdig.Extensions.AutoLinks;
using System.ComponentModel.DataAnnotations.Schema;
using Markdig.Renderers;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChatrBox.Models;

namespace ChatrBox.Data
{

    public class Message
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string MessagePlain { get; set; }

        public static MarkdownPipeline MarkdownPipeline { get; set; }
        
        /// <summary>
        /// Takes the plain message text entered by the user and applies HTML formatting
        /// for display on the site.
        /// </summary>
        [NotMapped]
        public string MessageHTML 
        { 
            get
            {
                string parsedMessage = Markdown.ToHtml(MessagePlain, MarkdownPipeline);

                var userIcon = HtmlElement.Create("img")
                    .EnableSelfClose()
                    .AddClass("mx-auto userMsgIcon")
                    .AddAttribute("src", Sender.ImageUrl);

                var username = HtmlElement.Create("div")
                    .SetContent(Sender.UserName)
                    .AddClass("fs-2 fw-bold msgUsername");

                var message = HtmlElement.Create("div")
                    .SetContent(parsedMessage);

                var messageTray = HtmlElement.Create("div")
                    .AddClass("text-muted message-tray")
                    .AddStyle("width", "100%")
                    .AddStyle("height", "20px")
                    .SetContent("");

                var messageContentWrap = HtmlElement.Create("div")
                    .AddClass("col-md-11 row")
                    .SetContent(username, message);

                string HTML = HtmlElement.Create("div")
                    .AddClass("msgDisplay")
                    .SetContent(userIcon, messageContentWrap)
                    .ToString();

                return HTML;
            }
        }

        public string SenderId { get; set; }
        public virtual Chatr Sender { get; set; }

        public int TopicId { get; set; }
        public virtual Topic Topic { get; set; }

        public bool IsEdited { get; set; }

    }
}
