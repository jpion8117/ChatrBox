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

        public IActionResult Index(int communityId = 0)
        {
            

            return View();
        }
    }
}
