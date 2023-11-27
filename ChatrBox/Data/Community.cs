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

        [NotMapped]
        public bool IsUnaltered => IntegretyVerifyer.Veryify(ImageUrl, ImageHash);
    }
}
