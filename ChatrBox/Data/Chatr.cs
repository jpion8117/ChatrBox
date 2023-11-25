using ChatrBox.CoreComponents;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatrBox.Data
{
    public class Chatr : IdentityUser, IImageDbReference
    {
        public DateTime LastActive { get; set; }

        /// <summary>
        /// Gets the current activity status of the user.
        /// </summary>
        [NotMapped]
        public bool ActiveUser
        {
            get 
            {
                return DateTime.UtcNow < LastActive.AddSeconds(ConfigManager.ActivityTimeOut);
            }
        }

        public string ImageHash { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public virtual List<Message> Messages { get; set; }

        [NotMapped]
        public bool IsUnaltered => IntegretyVerifyer.Veryify(ImageUrl, ImageHash);
    }
}
