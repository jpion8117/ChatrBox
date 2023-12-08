using ChatrBox.Areas.API.Controllers;
using ChatrBox.CoreComponents.API;
using ChatrBox.Models;
using ChatrBox.Models.CommunityControls;

namespace ChatrBox.Data
{
    public class DbHousekeeping
    {
        private readonly ApplicationDbContext _context;
        private readonly Dictionary<string, List<string>> _housekeepingLog = new Dictionary<string, List<string>>();

        public Dictionary<string, List<string>> HouseKeepingLog { get => _housekeepingLog; }

        public DbHousekeeping(ApplicationDbContext context)
        {
            _context = context;
        }

        public static DbHousekeeping Create(ApplicationDbContext context)
        {
            return new DbHousekeeping(context);
        }

        public DbHousekeeping RunAll()
        {
            var usersMissingIcons = _context.Users
                .Where(u => u.ImageUrl == "" || u.ImageUrl == null)
                .ToList();

            foreach (var user in usersMissingIcons)
            {
                user.QuickAssign(ImageUploader.AssignDefaultIcon());
                _context.Users.Update(user);
            }

            var communitiesMissingIcons = _context.Communities
                .Where(c => c.ImageUrl == "" || c.ImageUrl == null)
                .ToList();

            foreach (var community in communitiesMissingIcons)
            {
                community.QuickAssign(ImageUploader.AssignDefaultIcon());
                _context.Communities.Update(community);
            }

            _context.SaveChanges();

            var communitiesMissingSystemTopics = _context.Communities
                .ToList()
                .Where(c => !c.IsMissingSystemTopics)
                .ToList();

            if (communitiesMissingSystemTopics.Any())
                AddSystemTopics(communitiesMissingSystemTopics);

            return this;
        }

        public DbHousekeeping InitializeNewCommunity(Community community, Chatr owner)
        {
            //add user to his own community. Added with Visibility.Open so user
            //can post in their own community, the community is set to Private
            //by default.
            var comUser = CommunityUser.Create(community.Id, owner.Id, Visibility.Open);

            _context.CommunityUsers.Add(comUser);

            var topic = new Topic
            {
                Name = "general",
                Description = "AUTO GENERATED",
                CommunityId = community.Id,
                LastPost = DateTime.UtcNow,
                PostPermission = PostPermission.Open
            };

            _context.Topics.Add(topic);
            _context.SaveChanges();

            var cheddar = _context.Users.FirstOrDefault(c => c.UserName == "Cheddar_Chatr")
                ?? throw new InvalidOperationException("System account not found! Consider " +
                "updating database.");

            var welcomeMsg = new Message
            {
                SenderId = cheddar.Id,
                MessagePlain = File.ReadAllText(Path.Combine(AdminController.HomePath, "AutomatedMessages", "welcome.txt")),
                Timestamp = DateTime.UtcNow,
                TopicId = topic.Id,
                IsEdited = false
            };

            _context.Messages.Add(welcomeMsg);

            AddSystemTopics(new List<Community> { community });

            _context.SaveChanges();

            return this;
        }

        private List<string> AddSystemTopics(List<Community> communities)
        {
            var actionsTaken = new List<string>();

            foreach (var community in communities)
            {
                var missingSystemTopics = community.MissingSystemTopics();

                foreach (var missingSystemTopic in missingSystemTopics)
                {
                    switch (missingSystemTopic)
                    {
                        case "public":
                            var publicTopic = new Topic
                            {
                                CommunityId = community.Id,
                                DisplayOrder = -4,
                                Description = "Anything only community managers and their appointed " +
                                    "moderators may post to this topic. Any Chat'r may view this topic " +
                                    "unless the community is set to closed. This topic is auto generated " +
                                    "and cannot be removed or modified.",
                                PostPermission = PostPermission.Closed,
                                LastPost = DateTime.Now
                            };

                            publicTopic.SetSystemTopic("public");
                            _context.Topics.Add(publicTopic);
                            _context.SaveChanges();
                            
                            var welcomeMessage = new Message
                            {
                                SenderId = Cheddar.SystemUserId,
                                Timestamp = DateTime.Now,
                                TopicId = publicTopic.Id,
                                IsSystem = true,
                                MessagePlain = $"# Welcome to {community.Name} \n ### Please read any community " +
                                    $"rules below before joining"
                            };

                            var mgrMessage = new Message
                            {
                                SenderId = Cheddar.SystemUserId,
                                Timestamp = DateTime.Now,
                                TopicId = publicTopic.Id,
                                IsSystem = true,
                                IsHidden = true,
                                MessagePlain = $"As the community manager or moderator you are the only one who can see this " +
                                    $"message. This topic is visible to any registerd Chat'r unless your community is set to " +
                                    $"closed. Think of this as the front page of your Chat'r community! Tell visitors why they " +
                                    $"may want to join your community, lay down some ground rules, what ever you choose! The " +
                                    $"messages above and below this are like the header and footer of the page. Anything you " +
                                    $"post in here will show up between these two messages"
                            };

                            var joinMessage = new Message
                            {
                                SenderId = Cheddar.SystemUserId,
                                Timestamp = DateTime.Now,
                                TopicId = publicTopic.Id,
                                IsSystem = true,
                                IsSticky = true,
                                MessagePlain = $"<button class=\"btn btn-primary btn-join-community w-100\" id=\"JoinCommunity_{community.Id}\">Join {community.Name}</button>"
                            };

                            _context.Messages.Add(welcomeMessage);
                            _context.Messages.Add(mgrMessage);
                            _context.Messages.Add(joinMessage);
                            _context.SaveChanges();

                            break;

                        case "community-notifications":

                            var notificationsTopic = new Topic
                            {
                                CommunityId = community.Id,
                                DisplayOrder = -3,
                                Description = "Anything only community managers and their appointed " +
                                    "moderators may post to this topic. Any Chat'r may view this topic " +
                                    "unless the community is set to closed. This topic is auto generated " +
                                    "and cannot be removed or modified.",
                                PostPermission = PostPermission.Closed,
                                LastPost = DateTime.Now
                            };

                            notificationsTopic.SetSystemTopic("community-notifications");
                            _context.Topics.Add(notificationsTopic);
                            _context.SaveChanges();

                            var notificationExplainer = new Message
                            {
                                SenderId = Cheddar.SystemUserId,
                                Timestamp = DateTime.Now,
                                TopicId = notificationsTopic.Id,
                                IsSystem = true,
                                MessagePlain = "## Welcome to your notification center! \n " +
                                    "* Here you will find requests from users to join your community! You if you " +
                                    "click accept, they will be allowed as full members of the community and recieve " +
                                    "all the benefits that come with that. \n * Think of this topic as the central " +
                                    "notification center of your community. \n * only you and those you appoint as " +
                                    "moderators will see this topic and any messages in here. \n * You cannot post " +
                                    "anything here!"
                            };

                            _context.Messages.Add(notificationExplainer);
                            _context.SaveChanges();

                            break;
                    }
                }
            }

            return actionsTaken;
        }
    }
}
