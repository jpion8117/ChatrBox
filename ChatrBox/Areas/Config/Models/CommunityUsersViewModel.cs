#nullable disable
using ChatrBox.CoreComponents;
using ChatrBox.CoreComponents.API;
using ChatrBox.Data;

namespace ChatrBox.Areas.Config.Models
{
    public enum CommunityUserGroup
    {
        Owner,
        Moderator,
        User
    }

    public class CommunityUsersViewModel
    {
        private Dictionary<CommunityUserGroup, List<CommunityUser>> _users 
            = new Dictionary<CommunityUserGroup, List<CommunityUser>>();

        public string CommunityName { get; set; }

        public CommunityRole CommunityRole { get; set; }

        public void AddUserGroup(CommunityUserGroup group, List<CommunityUser> users)
        {
            _users.Add(group, users);
        }

        public void RemoveUserGroup(CommunityUserGroup group)
        {
            _users.Remove(group);
        }

        public List<CommunityUser> GetUserGroup(CommunityUserGroup group)
        {
            if (_users.ContainsKey(group))
                return _users[group];

            return new List<CommunityUser>();
        }
    }
}
