using ChatrBox.Data;

namespace ChatrBox.Areas.Config.Models
{
    public class CommunityIndexViewModel
    {
        private Dictionary<string, List<Community>> _communityGroups = new Dictionary<string, List<Community>>();

        public void AddGroup(string key, List<Community> communities)
        {
            _communityGroups[key] = communities;
        }
        public void RemoveGroup(string key)
        {
            _communityGroups.Remove(key);
        }
        public List<Community> GetCommunity(string key) 
        {
            if (_communityGroups.ContainsKey(key))
                return _communityGroups[key];

            return new List<Community>();
        }
    }
}
