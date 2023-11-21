using ChatrBox.Models.System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatrBox.Data
{
    public class Chatr : IdentityUser
    {
        public DateTime LastActive { get; set; }
        public int? IconId { get; set; }
        public ChatrIcon Icon { get; set; }

        /// <summary>
        /// Gets the current activity status of the user.
        /// </summary>
        [NotMapped]
        public bool ActiveUser
        {
            get 
            {
                return DateTime.UtcNow > LastActive.AddSeconds(ConfigManager.ActivityTimeOut);
            }
        }
    }
}
