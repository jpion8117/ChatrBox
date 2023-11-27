using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChatrBox.Data;
using Microsoft.AspNetCore.Authorization;

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
            var communities = _context.Communities.ToList();
            var myCommunities = new List<Community>();

            foreach (var community in communities)
            {
                if (CanView(community)) myCommunities.Add(community);
            }
            
            return View(myCommunities);
        }

        [Authorize(Roles = "admin, superAdmin, moderator")]
        public IActionResult IndexAll()
        {
            return _context.Communities.ToList() != null ?
                View("Index", _context.Communities.ToList()) :
                Problem("No communities found!");
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
            return View();
        }

        // POST: Config/Communities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Tags,OwnerId,Visibility,ContentFilter,ImageHash,ImageUrl")] Community community)
        {
            if (ModelState.IsValid)
            {
                _context.Add(community);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(community);
        }

        // GET: Config/Communities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Communities == null)
            {
                return NotFound();
            }

            var community = await _context.Communities.FindAsync(id);
            if (community == null)
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

        private bool CommunityExists(int id)
        {
          return (_context.Communities?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool CanView(Community community)
        {
            if (User.Identity == null) return false;

            return community.Owner.UserName == User.Identity.Name; 
        }
    }
}
