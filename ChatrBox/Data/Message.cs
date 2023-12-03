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

                //stores the new value of the message's plaintext content
                _messagePlain = value;
                
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

                var userIcon = HtmlElement.Create("img")
                    .EnableSelfClose()
                    .AddClass("mx-auto userMsgIcon")
                    .AddAttribute("src", Sender.ImageUrl);

                var username = HtmlElement.Create("div")
                    .SetContent(Sender.UserName)
                    .AddClass("fs-2 fw-bold msgUsername");

                var message = HtmlElement.Create("div")
                    .SetContent(parsedMessage);

                var messagePlain = HtmlElement.Create("textarea")
                    .AddAttribute ("type", "text")
                    .SetContent(MessagePlain)
                    .AddAttribute("hidden")
                    .SetID($"messageId_{Id}_Content");

                var messageContentWrap = HtmlElement.Create("div")
                    .AddClass("col-md-11 row")
                    .AddContent(username, message, messagePlain);

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

    }
}
