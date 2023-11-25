using ChatrBox.Data;

namespace ChatrBox.CoreComponents
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

        /// <summary>
        /// Defines the number of messages that will be pulled from the server and sent to the 
        /// client machine. (does not effect archive searches)
        /// </summary>
        public int MessageCount { get; set; }
    }
}
