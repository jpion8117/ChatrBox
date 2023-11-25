using ChatrBox.CoreComponents;
using ChatrBox.Data;
using ChatrBox.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChatrBox.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            foreach (var user in users)
            {
                if (string.IsNullOrEmpty(user.ImageUrl))
                {
                    var defIcon = (ImageBase)ImageUploader.AssignDefaultIcon();
                    user.ImageUrl = defIcon.ImageUrl;
                    user.ImageHash = defIcon.ImageHash;

                    _context.Users.Update(user);
                    _context.SaveChanges();
                }
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult TestJs() 
        {
            return View();
        }

        public JsonResult UserCheckin(int topicId = -1)
        {
            if (HttpContext.User.Identity != null)
            {
                var username = HttpContext.User.Identity.Name;
                var user = _context.Users.FirstOrDefault(u => u.UserName == username) ?? 
                    throw new ArgumentNullException();

                //check for new messages in topic
                bool newMessages = false;
                if (topicId != -1)
                {
                    Topic? topic = null;
                    DateTime lastPost;
                    topic = _context.Topics.FirstOrDefault(t => t.Id == topicId);

                    if (topic != null)
                        lastPost = topic.LastPost;
                    else
                        lastPost = DateTime.UtcNow;

                    if (user.LastActive < lastPost)
                        newMessages = true;
                }

                user.LastActive = DateTime.UtcNow;
                _context.Users.Update(user);
                _context.SaveChanges();

                return new JsonResult(new { status = "Recieved: Ok", newMessages, username = user.UserName, time = DateTime.UtcNow.ToString() });
            }

            return new JsonResult(new { status = "Recieved: Failed to locate user.", time = DateTime.UtcNow.ToString() });
        }
    }
}