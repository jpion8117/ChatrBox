using ChatrBox.Areas.Config.Models;
using ChatrBox.CoreComponents;
using ChatrBox.CoreComponents.API;
using ChatrBox.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatrBox.Areas.Config.Controllers
{
    [Authorize]
    [Area("Config")]
    public class CommunityUsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommunityUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("Config/Communities/{communityId}/Users")]
        public IActionResult Index(int communityId = 0)
        {
            var user = GetUser();
            var communityUser = CommunityTools.Create(_context).GetCommunityUser(communityId, user.Id, out var role);

            CommunityUsersViewModel viewModel = new CommunityUsersViewModel();

            viewModel.CommunityRole = role;

            if (role < CommunityRole.Moderator)
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });

            var allUsers = _context.CommunityUsers.Where(cu => cu.CommunityId == communityId);

            viewModel.AddUserGroup(CommunityUserGroup.Owner, allUsers
                .Where(cu => cu.Community.OwnerId == cu.ChatrId)
                .ToList());

            if (viewModel.GetUserGroup(CommunityUserGroup.Owner).Any())
            {
                viewModel.CommunityName = viewModel.GetUserGroup(CommunityUserGroup.Owner)[0].Community.Name;
            }
            else
            {
                return NotFound($"Community #{communityId} not found.");
            }

            viewModel.AddUserGroup(CommunityUserGroup.Moderator, allUsers
                .Where(cu => cu.IsModerator)
                .ToList());

            viewModel.AddUserGroup(CommunityUserGroup.User, allUsers
                .Where(cu => !cu.IsModerator)
                .Where(cu => cu.Community.OwnerId != cu.ChatrId)
                .ToList());

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ToggleUserMod(string targetId, int communityId)
        {
            var manager = GetUser();
            var communityTools = CommunityTools.Create(_context);
            communityTools.GetCommunity(communityId, manager.Id, out var role);

            //Only community managers can add/remove moderators
            if (role != CommunityRole.Owner)
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });

            var communityUser = communityTools.GetCommunityUser(communityId, targetId, out role);

            if (role == CommunityRole.Member)
                communityUser.IsModerator = true;
            else
                communityUser.IsModerator = false;

            _context.CommunityUsers.Update(communityUser);
            _context.SaveChanges();

            return RedirectToActionPermanent("Index", new { communityId });
        }

        [HttpPost]
        public IActionResult KickUser(string targetId, int communityId)
        {
            var manager = GetUser();
            var communityTools = CommunityTools.Create(_context);
            communityTools.GetCommunityUser(communityId, manager.Id, out var managerRole);
            var targetUser = communityTools.GetCommunityUser(communityId, targetId, out var targetRole);

            if (managerRole < CommunityRole.Moderator)
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });

            else if (managerRole <= targetRole)
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });

            _context.CommunityUsers.Remove(targetUser); 
            _context.SaveChanges();

            return RedirectToActionPermanent("Index", new { communityId });
        }

        private Chatr GetUser()
        {
            Chatr chatr = new Chatr();

            if (User.Identity != null)
            {
                return _context.Users
                    .FirstOrDefault(u => u.UserName == User.Identity.Name) ??
                        throw new InvalidOperationException("Failed to locate account of currently logged in user");
            }

            return chatr;
        }
    }
}
