using ChatrBox.Areas.Config.Models;
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
            Chatr user = new Chatr();
            if(User.Identity != null)
            {
                user = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name) ??
                    throw new ArgumentNullException("How the actual hell did you manage this!!");
            }

            CommunityUsersViewModel viewModel = new CommunityUsersViewModel();

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
    }
}
