using Castle.Core.Internal;
using ChatrBox.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ChatrBox.Areas.Config.Controllers
{
    [Authorize]
    [Area("Config")]
    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Chatr> _userManager;

        public MessagesController(ApplicationDbContext context, 
                                  UserManager<Chatr> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int topicId, string searchString = "")
        {
            ViewData["topicId"] = topicId;

            var user = await _userManager.GetUserAsync(User) ??
                throw new ArgumentException("User is null.");

            var topic = _context.Topics.FirstOrDefault(t => t.Id == topicId);

            if (topic == null)
            {
                return NotFound($"The topic you requested with id:{topicId} could not be located");
            }

            var comUsers = _context.CommunityUsers
                .Where(cu => cu.ChatrId == user.Id)
                .Where(cu => cu.CommunityId == topic.CommunityId)
                .ToList();

            bool overridePermissions = User.IsInRole("admin") ||
                                       User.IsInRole("superAdmin") ||
                                       User.IsInRole("moderator");

            if (comUsers.Count == 0 && !overridePermissions)
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity"});
            }

            var messages = _context.Messages
                .Where(m => m.Topic.Id == topicId)
                .OrderByDescending(m => m.Timestamp);

            //apply filters and search options
            if (!string.IsNullOrEmpty(searchString))
            {
                var afterFilter = "after:'";
                var beforeFilter = "before:'";
                var senderFilter = "sender:'";

                //normalize the search string to lowercase
                searchString = searchString.ToLower();

                if (searchString.Contains(senderFilter))
                {
                    //extract filter contents
                    var filter = searchString.Substring(
                        searchString.IndexOf(senderFilter) + senderFilter.Length);
                    filter = filter.Substring(0, filter.IndexOf('\''));

                    var users = filter.Split(',');
                    for (int i = 0; i < users.Length; ++i)
                        users[i] = users[i].Trim();

                    messages = (IOrderedQueryable<Message>)messages.Where(m => users.Contains(m.Sender.UserName.ToLower()));

                    //remove filter from search string
                    var purge = $"{senderFilter}{filter}'";
                    searchString = searchString.Remove(
                        searchString.IndexOf(purge),purge.Length);
                }

                if (searchString.Contains(afterFilter))
                {
                    //extract filter contents
                    var filter = searchString.Substring(
                        searchString.IndexOf(afterFilter) + afterFilter.Length);
                    filter = filter.Substring(0, filter.IndexOf('\''));

                    //parse date
                    DateTime.TryParse(filter, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal , out DateTime date);
                    date = date.ToUniversalTime();

                    //filter contents
                    messages = (IOrderedQueryable<Message>)messages.Where(m => m.Timestamp > date);

                    //remove filter from search string
                    var purge = $"{afterFilter}{filter}'";
                    searchString = searchString.Remove(
                        searchString.IndexOf(purge), purge.Length);
                }

                if (searchString.Contains(beforeFilter))
                {
                    //extract filter contents
                    var filter = searchString.Substring(
                        searchString.IndexOf(beforeFilter) + beforeFilter.Length);
                    filter = filter.Substring(0, filter.IndexOf('\''));

                    //parse date
                    DateTime.TryParse(filter, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal , out DateTime date);
                    date = date.ToUniversalTime();

                    //filter contents
                    messages = (IOrderedQueryable<Message>)messages.Where(m => m.Timestamp < date);

                    //remove filter from search string
                    var purge = $"{beforeFilter}{filter}'";
                    searchString = searchString.Remove(
                        searchString.IndexOf(purge), purge.Length);
                }

                if (!searchString.IsNullOrEmpty())
                {
                    messages = (IOrderedQueryable<Message>)messages.Where(m => m.MessagePlain.ToLower().Contains(searchString));
                }
            }

            return View(messages.ToList());
        }
    }
}
