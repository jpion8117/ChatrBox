using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChatrBox.Data;
using ChatrBox.Models.CommunityControls;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace ChatrBox.Areas.Config.Controllers
{
    [Area("Config")]
    public class TopicsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TopicsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Config/Topics
        public async Task<IActionResult> Index(int communityId)
        {
            var community = _context.Communities.Find(communityId);

            if (community == null)
                return NotFound("Community not found.");

            ViewBag.CommunityName = community.Name;

            var applicationDbContext = _context.Topics
                .Where(t => t.CommunityId == communityId)
                .OrderBy(t => t.DisplayOrder)
                .Include(t => t.Community);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Config/Topics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Topics == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics
                .Include(t => t.Community)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topic == null)
            {
                return NotFound();
            }

            return View(topic);
        }

        // GET: Config/Topics/Create
        public IActionResult Create()
        {
            ViewData["Permissions"] = new SelectList(Enum.GetNames(typeof(PostPermission)), Enum.GetValues(typeof(PostPermission)));
            ViewData["CommunityId"] = new SelectList(_context.Communities, "Id", "Name");
            return View();
        }

        // POST: Config/Topics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Topic topic)
        {
            ModelState.Remove("Messages");
            ModelState.Remove("Community");
            if (ModelState.IsValid)
            {
                _context.Add(topic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Permissions"] = new SelectList(Enum.GetNames(typeof(PostPermission)), Enum.GetValues(typeof(PostPermission)));
            ViewData["CommunityId"] = new SelectList(_context.Communities, "Id", "Name", topic.CommunityId);
            return View(topic);
        }

        // GET: Config/Topics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Topics == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics.FindAsync(id);
            if (topic == null)
            {
                return NotFound();
            }
            ViewData["CommunityId"] = new SelectList(_context.Communities, "Id", "Id", topic.CommunityId);
            return View(topic);
        }

        // POST: Config/Topics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,PostPermission,LastPost,CommunityId")] Topic topic)
        {
            if (id != topic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopicExists(topic.Id))
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
            ViewData["CommunityId"] = new SelectList(_context.Communities, "Id", "Id", topic.CommunityId);
            return View(topic);
        }

        // GET: Config/Topics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Topics == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics
                .Include(t => t.Community)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topic == null)
            {
                return NotFound();
            }

            return View(topic);
        }

        // POST: Config/Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Topics == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Topics'  is null.");
            }
            var topic = await _context.Topics.FindAsync(id);
            if (topic != null)
            {
                _context.Topics.Remove(topic);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public bool SaveListOrder(string orderString)
        {
            int[]? topicIds = new int[0];
            try
            {
                topicIds = JsonConvert.DeserializeObject<int[]>(orderString);
            }
            catch
            {
                return false;
            }

            if (topicIds == null)
                return false;

            for (int i = 0; i < topicIds.Length; i++)
            {
                int topicId = topicIds[i];
                var topic = _context.Topics.Find(topicId);
                if(topic == null)
                    return false;
                
                topic.DisplayOrder = i;
                _context.Topics.Update(topic);
            }

            _context.SaveChanges();

            return true;
        }

        private bool TopicExists(int id)
        {
          return (_context.Topics?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
