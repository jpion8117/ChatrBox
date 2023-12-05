using Castle.Core.Internal;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using ChatrBox.CoreComponents;
using ChatrBox.CoreComponents.API;
using ChatrBox.Data;
using ChatrBox.Models;
using ChatrBox.Models.CommunityControls;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;

namespace ChatrBox.Areas.API.Controllers
{
    [Authorize]
    [Area("API")]
    public class CommunitiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Chatr> _userManager;

        public CommunitiesController(ApplicationDbContext context, UserManager<Chatr> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<JsonResult> GetTopicList(int communityId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return new JsonResult(new
                {
                    error = Error.MakeReport(ErrorCodes.ContentRestricted, "User not authenticated.")
                }); ;
            }

            var communityUser = _context.CommunityUsers
                .FirstOrDefault(cu => cu.CommunityId == communityId && cu.ChatrId == user.Id) != null;

            if (!communityUser && !OverridePermissionRestriction)
            {
                return new JsonResult(new
                {
                    error = Error.MakeReport(ErrorCodes.ContentRestricted, "You do not have permission to view this community")
                });
            }

            var topics = _context.Topics
                .Where(t => t.CommunityId == communityId && (t.Name != "community-notifications" || t.Community.OwnerId == user.Id))
                .OrderBy(t => t.DisplayOrder)
                .ToList();

            var queryResult = new Dictionary<int, string>();
            string communityName = "";
            foreach (var topic in topics)
            {
                queryResult.Add(topic.Id, topic.Name);
                if (communityName.IsNullOrEmpty())
                    communityName = topic.Community.Name;
            }

            return new JsonResult(new
            {
                error = Error.MakeReport(ErrorCodes.Success, "Topics retrieved successfully."),
                topics = queryResult.ToArray(),
                communityName
            });
        }

        [HttpGet]
        public async Task<JsonResult> GetCommunityList()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return new JsonResult(new
                {
                    error = Error.MakeReport(ErrorCodes.ContentRestricted, "User not authenticated.")
                }); ;
            }

            var userCommunitiesDbResult = _context.CommunityUsers
                .Where(uc => uc.ChatrId == user.Id)
                .ToList();

            if (!userCommunitiesDbResult.Any())
            {
                return new JsonResult(new
                {
                    error = Error.MakeReport(ErrorCodes.FailedToLocate, "No communitites found")
                });
            }

            var userCommunities = new List<string>();

            for (int i = 0; i < userCommunitiesDbResult.Count; i++)
            {
                CommunityUser? userCommunity = userCommunitiesDbResult[i];
                var communityIcon = HtmlElement.Create("img")
                    .EnableSelfClose()
                    .AddClass("community-list-icon")
                    .AddStyle("width", "100%")
                    .AddAttribute("src", userCommunity.Community.ImageUrl)
                    .AddAttribute("title", userCommunity.Community.Name)
                    .SetID($"communityId_{userCommunity.CommunityId}")
                    .AddClass("community-list-btn");

                var communityListItem = HtmlElement.Create("li")
                    .AddClass("community-list-item");

                //add indicators for active community
                if (i == 0)
                {
                    communityListItem.AddClass("community-list-item-active");
                }

                //pack down and store in community list for return to the server
                communityListItem.AddContent(communityIcon);
                userCommunities.Add(communityListItem.ToString());
            }

            return new JsonResult(new
            {
                error = Error.MakeReport(ErrorCodes.Success, "User communities retrieved"),
                userCommunities,
                communityId = userCommunitiesDbResult[0].CommunityId
            });
        }

        [HttpGet]
        public JsonResult CheckForNewMessages(int topicId, DateTime lastMessagesPulled)
        {
            var topic = _context.Topics.Find(topicId);
            if (topic == null)
                return new JsonResult(new
                {
                    error = Error.MakeReport(ErrorCodes.FailedToLocate, $"Topic#{topicId} not found!")
                });

            if (topic.LastPost > lastMessagesPulled)
                return new JsonResult(new
                {
                    error = Error.MakeReport(ErrorCodes.Success, "New messages available")
                });

            return new JsonResult(new
            {
                error = Error.MakeReport(ErrorCodes.SuccessNoAction, "Up to date, no new messages")
            });
        }

        [HttpGet]
        public int GetFirstTopicId(int communityId)
        {
            var topic = _context.Topics.FirstOrDefault(t => t.CommunityId == communityId);
            if(topic == null) return 0;

            return topic.Id;
        }

        [HttpGet]
        public async Task<JsonResult> GetMessages(int topicId, DateTime? lastPost = null)
        {
            if (HttpContext.User.Identity != null)
            {
                var username = HttpContext.User.Identity.Name;
                var user = _context.Users.FirstOrDefault(u => u.UserName == username) ??
                    throw new ArgumentNullException();

                //admins, superAdmins, and moderators are not bound by read restrictions
                bool overridePermissions = User.IsInRole("admin") ||
                                           User.IsInRole("superAdmin") ||
                                           User.IsInRole("moderator");

                var topic = await _context.Topics.FindAsync(topicId);
                if (topic == null)
                    return new JsonResult(new
                    {
                        error = Error.MakeReport(ErrorCodes.FailedToLocate,
                            $"Topic with id:{topicId} not found")
                    });

                var community = _context.Communities.Find(topic.CommunityId);
                if (community != null)
                {
                    var communityChatrs = _context.CommunityUsers
                        .Where(c => c.CommunityId == community.Id)
                        .ToList();

                    bool moderator = community.OwnerId == user.Id || overridePermissions;
                    bool canView = false;
                    if (overridePermissions)
                        canView = true;
                    else
                    {
                        foreach (var comChatr in communityChatrs)
                        {
                            if (comChatr.ChatrId == user.Id)
                            {
                                canView = true;

                                if (comChatr.IsModerator) moderator = true;

                                break;
                            }
                        }
                    }

                    if (!canView && community.Visibility < Visibility.Protected && topic.Name != "public")
                        return new JsonResult(new
                        {
                            error = Error.MakeReport(ErrorCodes.ContentRestricted,
                                "You do not have permission to view this content."),
                            visibilitySetting = community.Visibility
                        });

                    var messageData = _context.Messages
                        .Where(m => m.TopicId == topicId)
                        .OrderByDescending(m => m.Timestamp)
                        .Take(ConfigManager.MessageCount)
                        .OrderBy (m => m.IsSticky)
                        .ThenBy(m => m.Timestamp)
                        .ToList();

                    var messages = new List<string>();

                    //prevents circular references
                    for (var i = 0; i < messageData.Count; ++i)
                    {
                        HtmlElement msgControls = HtmlElement.Create("span")
                            .AddClass("float-end");

                        if (messageData[i].Sender.UserName == user.UserName)
                        {
                            var editLink = HtmlElement.Create("a")
                                .SetContent("edit")
                                .AddClass("text-muted edit-msg-link")
                                .SetID($"messageId_{messageData[i].Id}")
                                .AddAttribute("href", "");

                            var deleteLink = HtmlElement.Create("a")
                                .SetContent("delete")
                                .AddClass("text-muted delete-msg-link")
                                .SetID($"messageIdDelete_{messageData[i].Id}")
                                .AddAttribute("href", "");

                            msgControls.SetContent($"{editLink} | {deleteLink}");
                        }
                        else if (OverridePermissionRestriction || community.OwnerId == user.Id)
                        {
                            var hideLink = HtmlElement.Create("a")
                                .SetContent("hide")
                                .AddClass("text-muted hide-msg-link")
                                .SetID($"messageIdHide_{messageData[i].Id}")
                                .AddAttribute("href", "");

                            var deleteLink = HtmlElement.Create("a")
                                .SetContent("delete")
                                .AddClass("text-muted delete-msg-link")
                                .SetID($"messageIdDelete_{messageData[i].Id}")
                                .AddAttribute("href", "");

                            msgControls.SetContent($"{hideLink} | {deleteLink}");
                        }
                        else
                        {
                            var flag = HtmlElement.Create("a");
                            if (messageData[i].IsFlaged)
                            {
                                flag.SetContent("flagged for review!")
                                    .AddClass("text-danger")
                                    .ChangeTag("span");
                            }
                            else
                            {
                                flag.SetContent("flag for mods")
                                    .AddClass("text-muted flag-msg-link")
                                    .SetID($"messageIdFlag_{messageData[i].Id}")
                                    .AddAttribute("href", "");
                            }

                            msgControls.SetContent($"{flag}");
                        }

                        var edited = HtmlElement.Create("span")
                            .AddClass("float-start")
                            .SetContent(messageData[i].IsEdited ? "(edited)" : "");

                        var messageTray = HtmlElement.Create("div")
                            .AddClass("text-muted message-tray")
                            .AddStyle("width", "100%")
                            .AddStyle("height", "20px")
                            .AddContent(edited, msgControls);

                        var message = HtmlElement.Create("div")
                            .AddClass("msgDisplay")
                            .SetContent(messageData[i].MessageHTML)
                            .AddContent(messageTray);

                        if (messageData[i].IsHidden)
                        {
                            if (user.Id != messageData[i].SenderId && !moderator)
                                message.AddAttribute("hidden");
                            else
                            {
                                var linkToContentPolicy = HtmlElement.Create("a")
                                    .SetContent("find out why")
                                    .AddAttribute("href", "");

                                var warning = HtmlElement.Create("p")
                                    .SetContent($"Only you, the community manager, or moderators can " +
                                        $"see this message {linkToContentPolicy}")
                                    .AddClass("text-danger");

                                message.AddClass("text-muted")
                                    .AddContent(warning, true);
                            }
                        }

                        messages.Add(message.ToString());
                    }

                    return new JsonResult(new
                    {
                        error = Error.MakeReport(ErrorCodes.Success,
                            "Messages successfully fetched from server"),
                        messages,
                        lastPost = topic.LastPost
                    });
                }
            }

            //This result will be sent if user does not have permissions to view this topic
            return new JsonResult(new
            {
                error = Error.MakeReport(ErrorCodes.ContentRestricted,
                    "You do not have permission to view this content.")
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="communityId"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetUsersOnline(int communityId)
        {
            var communityUsers = _context.CommunityUsers
                .Where(cu => cu.CommunityId == communityId)
                .ToList();

            if (communityUsers.Count == 0)
            {
                return new JsonResult(new
                {
                    error = Error.MakeReport(ErrorCodes.FailedToLocate, "Community not found.")
                });
            }

            var usersOnline = new List<string>();
            var usersOffline = new List<string>();

            foreach (var user in communityUsers)
            {
                var online = user.Chatr.ActiveUser;
                var statusTag = online ? "activeUser" : "inactiveUser";
                var htmlSoup = $"<div class=\"{statusTag}\">{user.Chatr.UserName}</div>";

                if (online) usersOnline.Add(htmlSoup);
                else usersOffline.Add(htmlSoup);
            }

            return new JsonResult(new
            {
                error = Error.MakeReport(ErrorCodes.Success, "User list found"),
                usersOnline,
                usersOffline
            });
        }

        /// <summary>
        /// Posts a message to a specific topic provided the user has permission to do so. Upon 
        /// completion a confirmation response will contain information related to delivery. 
        /// error.code:0 indicates successful post, all other error codes will describe error 
        /// details in error.description.
        /// </summary>
        /// <param name="topicId">Id of the topic the message is intended for</param>
        /// <param name="content">Raw text content of the message</param>
        /// <returns>JSon object containing an error object </returns>
        [HttpPost]
        public JsonResult SendMessage(int topicId, string content)
        {
            Chatr user = new Chatr();
            if (HttpContext.User.Identity != null)
            {
                var username = HttpContext.User.Identity.Name;
                user = _context.Users.FirstOrDefault(u => u.UserName == username) ??
                    throw new ArgumentNullException();
            }

            //allows admins and super admins to override community permissions to send
            //messages in any community. 
            bool overridePermissions = User.IsInRole("admin") ||
                                       User.IsInRole("superAdmin");

            //retrieve the topic
            var topic = _context.Topics.Find(topicId);

            if (topic == null)
            {
                return new JsonResult(new
                {
                    error = Error.MakeReport(ErrorCodes.FailedToLocate,
                        $"Unable to locate the requested topic with Id:{topicId}")
                });
            }

            //verify user has permissions to post
            var communityUsers = _context.CommunityUsers
                .Where(cu => cu.CommunityId == topic.CommunityId && cu.ChatrId == user.Id)
                .ToList();

            //prevent null or empty messages that apparently completely bricks the ENTIRE topic!!!!
            if (content.IsNullOrEmpty()) 
            {
                //I will probably come back to fix this, but this has caused me so much headache this
                //error message is my catharsis!!!!
                return new JsonResult(new
                {
                    error = Error.MakeReport(ErrorCodes.GeneralFailure, "NOT TODAY SATAN!!! Content can not be null or empty")
                });
            }

            if (communityUsers.Count > 0 || overridePermissions)
            {
                //check community content policies and perform content moderation.

                //verify user isn't posting in system topic if they are not allowed to post there
                if (topic.SystemTopic() && (user.Id != topic.Community.OwnerId || communityUsers[0].IsModerator))
                {
                    return new JsonResult(new
                    {
                        error = Error.MakeReport(ErrorCodes.ContentRestricted,
                            "You do not have permission to post. This is a restricted topic that only " +
                            "community managers/moderators can post in")
                    });
                }

                //post message to the server.
                var msg = new Message()
                {
                    SenderId = user.Id,
                    Timestamp = DateTime.UtcNow,
                    TopicId = topic.Id,
                    IsEdited = false,
                    MessagePlain = content
                };

                topic.LastPost = msg.Timestamp;

                _context.Topics.Update(topic);
                _context.Messages.Add(msg);
                _context.SaveChanges();

                return new JsonResult(new
                {
                    error = Error.MakeReport(ErrorCodes.Success,
                        $"Message successfully sent on topic '{topic.Name}'"),
                    messageSummary = new
                    {
                        msg.Id,
                        sender = msg.Sender.UserName,
                        topic = msg.Topic.Name,
                        messageContent = msg.MessagePlain
                    }
                });
            }

            //This result will be sent if user does not have permissions to post to this topic
            return new JsonResult(new
            {
                error = Error.MakeReport(ErrorCodes.ContentRestricted,
                    "You do not have permission to post.")
            });

        }

        [HttpPost]
        public JsonResult EditMessage(int topicId, int messageId, string content)
        {
            Chatr user = new Chatr();
            if (HttpContext.User.Identity != null)
            {
                var username = HttpContext.User.Identity.Name;
                user = _context.Users.FirstOrDefault(u => u.UserName == username) ??
                    throw new ArgumentNullException();
            }

            var message = _context.Messages.Find(messageId);
            if (message == null)
            {
                return new JsonResult(new
                {
                    error = Error.MakeReport(ErrorCodes.FailedToLocate, "Failed to locate message")
                });
            }

            if (user.Id != message.SenderId)
            {
                return new JsonResult(new
                {
                    error = Error.MakeReport(ErrorCodes.ContentRestricted, "You do not have permission to edit this message")
                });
            }
            message.Topic.LastPost = DateTime.Now;
            message.MessagePlain = content;

            _context.Messages.Update(message);
            _context.Topics.Update(message.Topic);
            _context.SaveChanges();

            return new JsonResult(new
            {
                error = Error.MakeReport(ErrorCodes.Success, "Message successfully edited")
            });
        }

        [HttpPost]
        public JsonResult DeleteMessage(int messageId)
        {
            var actionTaken = "";

            //locate message
            var message = _context.Messages.Find(messageId);
            if (message == null)
                return new JsonResult(new
                {
                    error = Error.MakeReport(ErrorCodes.FailedToLocate, $"Message #{messageId} not found.")
                });

            //check identity of user making request
            if (User.Identity == null)
                return new JsonResult(new
                {
                    error = Error.MakeReport(ErrorCodes.FailedToLocate, $"User not authenticated.")
                });

            var user = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            if (user == null)
                return new JsonResult(new
                {
                    error = Error.MakeReport(ErrorCodes.FailedToLocate, "Lord help you if you get this error! It should " +
                        "be theoretically impossible, but just in case here it is. Somehow, User.Identity is valid and " +
                        "User.Identity.Name does not exist in the Users Table in the database... Don't know how that " +
                        "happens and may God have mercy on your forsaken soul!!!!")
                });
            var owner = user.Id == message.SenderId;

            if (!owner && !OverridePermissionRestriction)
                return new JsonResult(new
                {
                    error = Error.MakeReport(ErrorCodes.ContentRestricted, "You do not have permission to delete/hide this message.")
                });

            //message owner deletes
            if(true)
            {
                actionTaken = "deleted";
                _context.Messages.Remove(message);
            }

            //moderator hides
            else
            {
                actionTaken = "hidden";
                message.IsHidden = true;
                _context.Messages.Update(message);
            }

            _context.SaveChanges();

            return new JsonResult(new
            {
                error = Error.MakeReport(ErrorCodes.Success, $"Message #{messageId} {actionTaken}.")
            });
        }

        /// <summary>
        /// Allows API endpoints to check if user can override permissions to view 
        /// content they otherwise would be restricted from viewing for moderation purposes
        /// </summary>
        private bool OverridePermissionRestriction
        {
            get => User.IsInRole("admin") ||
                   User.IsInRole("superAdmin") ||
                   User.IsInRole("moderator");
        }
    }
}
