#nullable disable
using ChatrBox.Models.CommunityControls;

namespace ChatrBox.Data
{
    public class Community
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// comma deliminated list of all the tags that apply to 
        /// this community and will help users find it in their searches.
        /// </summary>
        public string Tags { get; set; }
        public int IconId { get; set; }
        public CommunityIcon Icon { get; set; }
        public string OwnerId { get; set; }
        public Chatr Chatr { get; set; }
        public Visibility Visibility { get; set; }
    }
}
