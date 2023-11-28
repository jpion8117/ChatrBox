using Castle.Core.Internal;
using ChatrBox.CoreComponents;
using ChatrBox.Data;
using ChatrBox.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            if (HttpContext.User.Identity != null)
            {
                var username = HttpContext.User.Identity.Name;
                var user = _context.Users.FirstOrDefault(u => u.UserName == username) ??
                    throw new ArgumentNullException();

                user.LastActive = DateTime.UtcNow;
                _context.Users.Update(user);
                _context.SaveChanges();
            }

            AssignDefaultIconsAsync();

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

        public IActionResult InitialCheckin(string returnUrl)
        {
            if (HttpContext.User.Identity != null)
            {
                var username = HttpContext.User.Identity.Name;
                var user = _context.Users.FirstOrDefault(u => u.UserName == username) ??
                    throw new ArgumentNullException();

                user.LastActive = DateTime.UtcNow;
                _context.Users.Update(user);
                _context.SaveChanges();
            }

            return LocalRedirect(returnUrl);
        }

        public JsonResult UserCheckin()
        {
            if (HttpContext.User.Identity != null)
            {
                var username = HttpContext.User.Identity.Name;
                var user = _context.Users.FirstOrDefault(u => u.UserName == username) ?? 
                    throw new ArgumentNullException();

                user.LastActive = DateTime.UtcNow;
                _context.Users.Update(user);
                _context.SaveChanges();

                return new JsonResult(new { status = "Recieved: Ok", username = user.UserName, time = DateTime.UtcNow.ToString() });
            }

            return new JsonResult(new { status = "Recieved: Failed to locate user.", time = DateTime.UtcNow.ToString() });
        }

        private void AssignDefaultIconsAsync()
        {

            var usersMissingIcons = _context.Users
                .Where(u => u.ImageUrl == "" || u.ImageUrl == null)
                .ToList();

            var communitiesMissingIcons = _context.Communities
                .Where(c => c.ImageUrl == "" || c.ImageUrl == null)
                .ToList();

            foreach ( var user in usersMissingIcons)
            {
                user.QuickAssign(ImageUploader.AssignDefaultIcon());
                _context.Users.Update(user);
            }

            foreach ( var com in communitiesMissingIcons)
            {
                com.QuickAssign(ImageUploader.AssignDefaultIcon());
                _context.Communities.Update(com);
            }
            
            if(usersMissingIcons.Any() || communitiesMissingIcons.Any())
                _context.SaveChanges();
        }
    }
}