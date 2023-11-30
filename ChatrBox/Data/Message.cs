#nullable disable
using Markdig;
using Markdig.Extensions.MediaLinks;
using Markdig.Extensions.AutoLinks;
using System.ComponentModel.DataAnnotations.Schema;

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
                string parsed = Markdown.ToHtml(MessagePlain, MarkdownPipeline);

                string HTML = "" +
                    "<div class=\"msgDisplay\">" +
                        $"<img src=\"{Sender.ImageUrl}\" class=\"my-auto userMsgIcon\" />" +
                        "<div class=\"col-md-11 row\">" +
                            $"<div class=\"fs-2 fw-bold\">{Sender.UserName}</div>" +
                            $"<div class=\"\">{parsed}</div>" +
                        "</div>" +
                    "</div>";

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
