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
        public JsonResult GetTopicList(int communityID)
        {
            var topics = _context.Topics.Where(t => t.CommunityId == communityID).ToList();
            return new JsonResult(topics);
        }

        [HttpGet]
        public JsonResult CheckMessages(int topicID)
        {
            if (HttpContext.User.Identity != null)
            {
                var username = HttpContext.User.Identity.Name;
                var user = _context.Users.FirstOrDefault(u => u.UserName == username) ??
                    throw new ArgumentNullException();

                var topic = _context.Topics.Find(topicID);
                if (topic == null)
                    return new JsonResult(new
                    {
                        error = "Topic not found",
                        description = $"Topic with id:{topicID} not found"
                    });

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
                            error = "Content restricted",
                            description = "You do not have permission to view this content.",
                            visibilitySetting = community.Visibility
                        });

                    var messages = _context.Messages
                        .Where(m => m.TopicId == topicID)
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
                        error = "Success OK",
                        description = "Messages successfully fetched from server",
                        messages
                    });
                }
            }

            //This result will be sent if user does not have permissions to view this topic
            return new JsonResult(new
            {
                error = "Content restricted",
                description = "You do not have permission to view this content."
            });
        }
    }
}
