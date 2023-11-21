using ChatrBox.Data;

namespace ChatrBox.Models.System
{
    public class Configuration
    {

        /// <summary>
        /// Stores the configuration setting defining how often the client system 
        /// checks for new messages from the server for the community/topic they 
        /// are viewing. Message updates will also check in the current user
        /// </summary>
        public double MessageUpdateRate { get; set; }

        /// <summary>
        /// Stores the configuration setting defining how often the client system 
        /// updates online statuses for the community/topic the client Chatr is viewing
        /// </summary>
        public double StatusUpdateRate { get; set; }

        /// <summary>
        /// Stores the time (seconds) between checkins that the system requires to consider 
        /// a Chatr as "offline."
        /// </summary>
        public int ActivityTimeOut { get; set; }
    }
}
