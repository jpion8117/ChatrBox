using ChatrBox.CoreComponents;
using System.ComponentModel.DataAnnotations;

namespace ChatrBox.Areas.Config.Models
{
    public class ClientSettingsViewModel
    {
        private bool _saveSuccessful = false;
        public bool SaveSuccessful 
        {
            get
            {
                var value = _saveSuccessful;
                _saveSuccessful = false;
                return value;
            }

            set 
            {
                _saveSuccessful = value;
            } 
        }

        [Required]
        [Range(3000, 60000)]
        [Display(Name = "Message Update Speed")]
        public double MessageUpdateSpeed { get; set; }

        [Required]
        [Range(20000, 180000)]
        [Display(Name = "User Status Update Speed")]
        public double StatusUpdateSpeed { get; set; }

        [Required]
        [Range(30, 180)]
        [Display(Name = "Time until user is considered offline")]
        public int TimeUntilOffline { get; set; }

        [Required]
        [Range(50, 500)]
        [Display(Name = "Message Download Quantity")]
        public int MessageDownloadQty { get; set; }
        public void Save() 
        {
            ConfigManager.ActivityTimeOut = TimeUntilOffline;
            ConfigManager.StatusUpdateRate = StatusUpdateSpeed;
            ConfigManager.MessageUpdateRate = MessageUpdateSpeed;
            ConfigManager.MessageCount = MessageDownloadQty;
        }
    }
}
