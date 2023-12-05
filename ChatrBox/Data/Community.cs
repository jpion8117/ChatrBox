#nullable disable
using ChatrBox.Models.CommunityControls;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatrBox.Data
{
    public class Community : IImageDbReference
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// comma deliminated list of all the tags that apply to 
        /// this community and will help users find it in their searches.
        /// </summary>
        public string Tags { get; set; }
        public string OwnerId { get; set; }
        public virtual Chatr Owner { get; set; }
        public Visibility Visibility { get; set; }
        public ContentFilter ContentFilter { get; set; }
        public string ImageHash { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        public virtual List<Topic> Topics { get; set; }

        [NotMapped]
        public Topic GetDefaultTopic
        {
            get
            {
                var topic = Topics.FirstOrDefault(t => t.DisplayOrder == 0);
                
                if(topic != null)
                    return topic;

                return new Topic();
            }
        }

        [NotMapped]
        public bool IsUnaltered => IntegretyVerifyer.Veryify(ImageUrl, ImageHash);

        public void QuickAssign(IImageDbReference newImageRef)
        {
            ImageUrl = newImageRef.ImageUrl;
            ImageHash = newImageRef.ImageHash;
        }

        public List<string> MissingSystemTopics()
        {
            var missingSystemTopics = new List<string>();
            
            var topicNames = new List<string>();

            foreach (var topic in Topics)
            {
                topicNames.Add(topic.Name);
            }

            foreach (var systemTopic in Topic.SystemTopics) 
            {

                if (!topicNames.Contains(systemTopic))
                {
                    missingSystemTopics.Add(systemTopic);
                } 
            }

            return missingSystemTopics;
        }
    }
}
