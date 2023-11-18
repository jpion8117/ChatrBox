using System.ComponentModel.DataAnnotations.Schema;

namespace ChatrBox.Data
{
    public class Message
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string MessagePlain { get; set; }
        
        /// <summary>
        /// Takes the plain message text entered by the user and applies HTML formatting
        /// for display on the site.
        /// </summary>
        [NotMapped]
        public string MessageHTML 
        { 
            get
            {
                string HTML = "" +
                    "<div>" +
                        MessagePlain +
                    "</div>";

                return HTML;
            }
        }

        public virtual string SenderId { get; set; }
        public virtual Chatr Sender { get; set; }

        public virtual int TopicId { get; set; }
        public virtual Topic Topic { get; set; }

        public bool IsEdited { get; set; }

    }
}
