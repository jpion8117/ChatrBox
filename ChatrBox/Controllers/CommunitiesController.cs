using ChatrBox.CoreComponents.API;
using ChatrBox.Data;
using ChatrBox.Models.CommunityControls;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatrBox.Controllers
{
    [Authorize]
    public class CommunitiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CommunitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult GetTopicList(int communityId)
        {
            var topics = _context.Topics.Where(t => t.CommunityId == communityId).ToList();
            return new JsonResult(topics);
        }

        [HttpGet]
        public JsonResult CheckMessages(int topicId, DateTime? lastPost = null)
        {
            if (HttpContext.User.Identity != null)
            {
                var username = HttpContext.User.Identity.Name;
                var user = _context.Users.FirstOrDefault(u => u.UserName == username) ??
                    throw new ArgumentNullException();

                var topic = _context.Topics.Find(topicId);
                if (topic == null)
                    return new JsonResult(new
                    {
                        error = Error.MakeReport(ErrorCodes.FailedToLocate,
                            $"Topic with id:{topicId} not found")
                    });

                //check if there are any new posts on topic
                if (lastPost != null && topic.LastPost == lastPost)
                {
                    return new JsonResult(new
                    {
                        error = Error.MakeReport(ErrorCodes.SuccessNoAction,
                            "No new messages since last check.")
                    });
                }

                var community = _context.Communities.Find(topic.CommunityId);
                if (community != null)
                {
                    var communityChatrs = _context.CommunityUsers
                        .Where(c => c.CommunityId == community.Id)
                        .ToList();

                    bool canView = false;
                    foreach (var comChatr in communityChatrs)
                    {
                        if (comChatr.ChatrId == user.Id)
                        {
                            canView = true; 
                            break;
                        }
                    }
                    if (!canView && community.Visibility < Visibility.Protected)
                        return new JsonResult(new
                        {
                            error = Error.MakeReport(ErrorCodes.ContentRestricted,
                                "You do not have permission to view this content."),
                            visibilitySetting = community.Visibility
                        });

                    var messages = _context.Messages
                        .Where(m => m.TopicId == topicId)
                        .OrderByDescending(m => m.Timestamp)
                        .Take(250)
                        .OrderBy(m => m.Timestamp)
                        .ToList();

                    //prevents circular references
                    for (var i = 0; i < messages.Count; ++i)
                    {
                        messages[i].Topic = new Topic();
                        messages[i].Sender = new Chatr();
                    }

                    return new JsonResult(new
                    {
                        error = Error.MakeReport(ErrorCodes.Success, 
                            "Messages successfully fetched from server"),
                        messages
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
        /// Send a message to 
        /// </summary>
        /// <param name="topicId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
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

            if (communityUsers.Count > 0)
            {
                //check community content policies and perform content moderation.


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
    }
}
