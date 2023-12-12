using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChatrBox.Data;
using Microsoft.AspNetCore.Authorization;
using ChatrBox.Areas.Config.Models;
using ChatrBox.Models.CommunityControls;
using ChatrBox.Models;
using ChatrBox.CoreComponents;
using System.Runtime.CompilerServices;

namespace ChatrBox.Areas.Config.Controllers
{
    [Area("Config")]
    [Authorize]
    public class CommunitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommunitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Config/Communities
        public IActionResult Index()
        {
            Chatr user = new Chatr();
            if (User.Identity != null)
            {
                user = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name) ??
                    throw new ArgumentNullException();
            }

            var viewModel = new CommunityIndexViewModel();
            
            var myCommunities = _context.Communities
                .Where(c => c.OwnerId == user.Id)
                .ToList();

            var communitiesImIn = _context.CommunityUsers
                .Where(cu => cu.ChatrId == user.Id && cu.Community.OwnerId != user.Id)
                .ToList();

            var joinedCommunities = new List<Community>();
            var modCommunitiess = new List<Community>();

            foreach (var comUser in communitiesImIn)
            {
                joinedCommunities.Add(comUser.Community);

                if (comUser.IsModerator)
                    modCommunitiess.Add(comUser.Community);
            }

            viewModel.AddGroup("user", myCommunities);
            viewModel.AddGroup("joined", joinedCommunities);
            viewModel.AddGroup("mod", modCommunitiess);

            return View(viewModel);
        }

        [Authorize(Roles = "admin, superAdmin, moderator")]
        public IActionResult IndexAll()
        {
            ViewData["ModView"] = true;

            var viewModel = new CommunityIndexViewModel();

            viewModel.AddGroup("all", _context.Communities.ToList());

            return View("Index", viewModel);
        }

        // GET: Config/Communities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Communities == null)
            {
                return NotFound();
            }

            var community = await _context.Communities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (community == null)
            {
                return NotFound();
            }

            return View(community);
        }

        // GET: Config/Communities/Create
        public IActionResult Create()
        {
            Chatr user = new Chatr();
            if (User.Identity == null)
            {
                return NotFound("User not logged in");
            }

            user = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name) ??
                throw new InvalidOperationException();

            var community = new Community()
            {
                OwnerId = user.Id,
            };

            community.QuickAssign(ImageUploader.AssignDefaultIcon());

            return View(community);
        }

        // POST: Config/Communities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Tags,OwnerId,Visibility,ContentFilter,ImageHash,ImageUrl")] Community community)
        {
            Chatr user = new Chatr();
            if (User.Identity == null)
            {
                return NotFound("User not logged in");
            }

            user = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name) ??
                throw new InvalidOperationException();

            ModelState.Remove("GetDefaultTopic.Name");
            ModelState.Remove("GetDefaultTopic.Messages");
            ModelState.Remove("GetDefaultTopic.Community");
            ModelState.Remove("GetDefaultTopic.Description");

            if (ModelState.IsValid)
            {
                _context.Add(community);
                await _context.SaveChangesAsync();

                DbHousekeeping.Create(_context).InitializeNewCommunity(community, user);

                return RedirectToAction(nameof(Index));
            }



            return View(community);
        }

        // GET: Config/Communities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Chatr user = new Chatr();
            if (User.Identity == null)
            {
                return NotFound();
            }
            user = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name) ??
                throw new InvalidOperationException();

            if (id == null || _context.Communities == null)
            {
                return NotFound();
            }

            var community = await _context.Communities.FindAsync(id);            
            if (community == null)
            {
                return NotFound();
            }

            if (community.OwnerId != user.Id)
            {
                return NotFound();
            }

            return View(community);
        }

        // POST: Config/Communities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Tags,OwnerId,Visibility,ContentFilter,ImageHash,ImageUrl")] Community community)
        {
            if (id != community.Id)
            {
                return NotFound();
            }

            ModelState.Remove("GetDefaultTopic.Name");
            ModelState.Remove("GetDefaultTopic.Messages");
            ModelState.Remove("GetDefaultTopic.Community");
            ModelState.Remove("GetDefaultTopic.Description");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(community);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommunityExists(community.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(community);
        }

        // GET: Config/Communities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Communities == null)
            {
                return NotFound();
            }

            var community = await _context.Communities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (community == null)
            {
                return NotFound();
            }

            return View(community);
        }

        // POST: Config/Communities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Communities == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Communities'  is null.");
            }
            var community = await _context.Communities.FindAsync(id);
            if (community != null)
            {
                _context.Communities.Remove(community);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Yeah... That's all fine and well, but Imma head out now. Good luck with whatever... That is...
        /// 
        /// Lisa: If you happen to notice this one comment, for this one little method, in the sea of code here let me know. Lol
        /// </summary>
        /// <param name="communityId">Id of the community you are peacin out from.</param>
        /// <returns>Some kinda voodoo magic that makes a webpage display.</returns>
        [HttpPost]
        public IActionResult LeaveCommunity(int communityId)
        {
            var user = GetUser();
            var comTools = CommunityTools.Create(_context);
            var community = comTools.GetCommunity(communityId, user.Id, out var role);

            if (role == CommunityRole.Owner)
                return Problem("You are the community owner and may not leave the community. You may delete the community if you wish.");
            else if (role == CommunityRole.Nonmember)
                return Problem($"You are not currently a member of community #{community.Id} '{community.Name}'.");

            //user is either a member or a moderator of the community and can now exit
            var comUser = comTools.GetCommunityUser(communityId, user.Id);

            if (comUser.Id == -1)
                return NotFound("could not locate community user");
            
            _context.CommunityUsers.Remove(comUser);
            _context.SaveChanges();

            return RedirectToActionPermanent("Index");
        }

        private bool CommunityExists(int id)
        {
          return (_context.Communities?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool CanView(Community community)
        {
            if (User.Identity == null) return false;

            return community.Owner.UserName == User.Identity.Name; 
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
