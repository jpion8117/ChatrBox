using ChatrBox.Models.CommunityControls;

namespace ChatrBox.Data
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PostPermission PostPermission { get; set; }
        public DateTime LastPost {  get; set; }
        public virtual int CommunityId { get; set; }
        public virtual Community Community { get; set; }

        public virtual List<Message> Messages { get; set; }
    }
}
