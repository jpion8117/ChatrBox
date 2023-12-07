#nullable disable
using Markdig;
using Markdig.Extensions.MediaLinks;
using Markdig.Extensions.AutoLinks;
using System.ComponentModel.DataAnnotations.Schema;
using Markdig.Renderers;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChatrBox.Models;
using Castle.Core.Internal;
using ChatrBox.CoreComponents.API;

namespace ChatrBox.Data
{

    public class Message
    {
        private string _messagePlain;
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string MessagePlain 
        {
            get => _messagePlain; 
            set
            {
                //blocks null assignments that break entire topics! That was a fun evening!!!!
                if (value.IsNullOrEmpty())
                    return;

                //the message previously had content, it will be marked as edited
                if (!_messagePlain.IsNullOrEmpty())
                    IsEdited = true;

                var lines = value.Split('\n');
                var content = "";
                foreach (var line in lines)
                    content += line + " \n ";

                //stores the new value of the message's plaintext content
                _messagePlain = content;
                
            }
        }

        [NotMapped]
        public string Quote
        {
            get
            {
                var lines = MessagePlain.Split('\n');
                var quote = $"> replying to {Sender.UserName}";
                foreach (var line in lines)
                    quote += "> " + line;

                return quote;
            }
        }

        [NotMapped]
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

                //allow system messages to contain raw html
                if (IsSystem)
                {
                    parsedMessage = parsedMessage
                        .Replace("&gt;", ">")
                        .Replace("&lt;", "<")
                        .Replace("&quot;", "\"");
                }

                var userIcon = HtmlElement.Create("img")
                    .EnableSelfClose()
                    .AddClass("mx-auto userMsgIcon")
                    .AddAttribute("src", Sender.ImageUrl);

                var timeSent = HtmlElement.Create("div")
                    .SetContent(DateTime.UtcNow.Date == Timestamp.Date ? 
                        $"Sent today at {Timestamp.ToLocalTime().ToString("t")}" : 
                        $"Sent {Timestamp.ToLocalTime().ToString("d")} at {Timestamp.ToLocalTime().ToString("t")}")
                    .AddClass("text-muted")
                    .AddStyle("display", "inline!important");

                var username = HtmlElement.Create("div")
                    .SetContent(Sender.UserName)
                    .AddClass("fs-4 fw-bold msgUsername")
                    .AddStyle("display", "inline!important");

                var messageHead = HtmlElement.Create("div")
                    .AddContent(username, timeSent);

                var message = HtmlElement.Create("div")
                    .SetContent(parsedMessage);

                var messagePlain = HtmlElement.Create("textarea")
                    .AddAttribute ("type", "text")
                    .SetContent(MessagePlain)
                    .AddAttribute("hidden")
                    .SetID($"messageId_{Id}_Content");

                var messageContentWrap = HtmlElement.Create("div")
                    .AddClass("col-md-11 row")
                    .AddContent(messageHead, message, messagePlain);

                return userIcon.ToString() + messageContentWrap.ToString();
            }
        }

        public string SenderId { get; set; }
        public virtual Chatr Sender { get; set; }

        public int TopicId { get; set; }
        public virtual Topic Topic { get; set; }

        public bool IsEdited { get; set; }
        public bool IsHidden { get; set; }
        public bool IsFlaged { get; set; }
        public bool IsSticky { get; set; }
        public bool IsSystem {  get; set; }

    }
}
