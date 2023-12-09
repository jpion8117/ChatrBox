using ChatrBox.Models.CommunityControls;

namespace ChatrBox.Data
{
    public class Topic
    {
        private string _name;
        private static readonly List<string> _systemTopics = new List<string> 
        { 
            //"community-moderation",         //topic used to notify community managers/moderators of moderation requests
            
            "community-notifications",        //topic used to notify community managers/moderators important information
                                              //such as join requests
            
            //"announcements",                //topic only community managers/moderators can post in, but any member can read.
            
            "public"                          //only community managers/moderators can post here, it is the public landing page
                                              //for your community. Unless your community is set to closed, this topic will be
                                              //visible to any non member who vists your community.
        };
        
        public static List<string> SystemTopics
        {
            get => _systemTopics;
        }

        public int Id { get; set; }
        public string Name 
        {
            get => _name;
            set
            {
                var normalizedName = value.ToLower().Replace(' ', '-');

                //prevents users from using or changing topic names that are reserved for system use.
                if (!_systemTopics.Contains(normalizedName) || !_systemTopics.Contains(_name))
                    _name = normalizedName;
            } 
        }
        public string Description { get; set; }
        public PostPermission PostPermission { get; set; }
        public DateTime LastPost {  get; set; }
        public int DisplayOrder { get; set; }
        public virtual int CommunityId { get; set; }
        public virtual Community Community { get; set; }

        public virtual List<Message> Messages { get; set; }

        /// <summary>
        /// checks if a supplied name is valid
        /// </summary>
        /// <param name="name">new topic name</param>
        /// <param name="reason">outputs the reason why the name was rejected</param>
        /// <returns>true if the name is restricted and a reason why it was rejected</returns>
        public bool RestrictedName(string name, out string reason)
        {
            //checks if the name is reserved for system use
            if (_systemTopics.Contains(name))
            {
                reason = $"The topic name {name} is reserved for system use";
                return true;
            }

            //checks if the name already exists in this topic
            if (Community.Topics.FirstOrDefault(t => t.Name == name) != null)
            {
                reason = $"There is already a topic named {name} in this community";
                return true;
            }

            //if here name is safe to add to the database.
            reason = "";
            return false;
        }

        public bool SetSystemTopic(string name)
        {
            if (!_systemTopics.Contains(name))
                return false;

            _name = name;
            return true;
        }

        public bool SystemTopic()
        {
            return _systemTopics.Contains(Name);
        }
    }
}
