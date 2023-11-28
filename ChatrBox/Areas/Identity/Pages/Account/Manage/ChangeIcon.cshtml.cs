#nullable disable
using ChatrBox.Data;
using ChatrBox.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ChatrBox.Areas.Identity.Pages.Account.Manage
{
    public class ChangeIconModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<Chatr> _userManager;

        public ChangeIconModel(ApplicationDbContext dbContext, UserManager<Chatr> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public string CurrentIconUrl { get; set; }

        public List<string> IconNames { get; set; } = new List<string>();
        public List<string> IconPaths { get; set; } = new List<string>();

        [BindProperty]
        [Required]
        public string URL { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound("User not found. Are you logged in?");

            CurrentIconUrl = user.ImageUrl;

            var directoryInfo = new DirectoryInfo(Path.Combine(ImageUploader.HostPath, "images", "defaultIcons"));
            var icons = directoryInfo.GetFiles();

            foreach (var icon in icons)
            {
                var path = icon.FullName.Remove(0, ImageUploader.HostPath.Length);
                IconPaths.Add(path);

                var name = icon.Name;
                IconNames.Add(name.Remove(name.IndexOf(icon.Extension)));
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) 
                return NotFound("User not found. Are you logged in?");

            if(!ModelState.IsValid)
            {
                CurrentIconUrl = user.ImageUrl;

                var directoryInfo = new DirectoryInfo(Path.Combine(ImageUploader.HostPath, "images", "defaultIcons"));
                var icons = directoryInfo.GetFiles();

                foreach (var icon in icons)
                {
                    var path = icon.FullName.Remove(0, ImageUploader.HostPath.Length);
                    IconPaths.Add(path);

                    var name = icon.Name;
                    IconNames.Add(name.Remove(name.IndexOf(icon.Extension)));
                }

                return Page();
            }

            user.ImageUrl = URL;

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();

            return RedirectToPage("/Account/Manage/Index");
        }
    }
}
