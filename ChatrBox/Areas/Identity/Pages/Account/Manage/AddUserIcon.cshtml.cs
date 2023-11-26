#nullable disable
using ChatrBox.Data;
using ChatrBox.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChatrBox.Areas.Identity.Pages.Account.Manage
{
    public class AddUserIconModel : PageModel
    {
        private ApplicationDbContext _context;

        [BindProperty]
        public ImageBase IconInfo { get; set; }

        [BindProperty]
        public ImageUploader Uploader { get; set; }

        [BindProperty]
        public string ChatrId { get; set; }

        public AddUserIconModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet(string chatrId)
        {
            ChatrId = chatrId;
            Uploader = new ImageUploader();
            IconInfo = (ImageBase)ImageUploader.AssignDefaultIcon();
        }

        public IActionResult OnPost() 
        {
            var user = _context.Users.First(u => u.Id == ChatrId);
            user.ImageUrl = IconInfo.ImageUrl;
            user.ImageHash = IconInfo.ImageHash;

            Uploader.AssetName = user.UserName;
            Uploader.Type = ImageUploader.AssetType.ChatrIcon;

            _context.Users.Update(user);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult OnPostUpload()
        {
            if (Uploader.Image != null) 
            {
                IconInfo = (ImageBase)Uploader.Upload(out var success);
            }

            return Page();
        }
    }
}
