using ChatrBox.Areas.Config.Models;
using ChatrBox.CoreComponents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatrBox.Areas.Config.Controllers
{
    [Area("Config")]
    [Authorize(Roles = "admin,superAdmin")]
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult ClientSettings()
        {
            var viewmodel = new ClientSettingsViewModel();

            viewmodel.StatusUpdateSpeed = ConfigManager.StatusUpdateRate;
            viewmodel.MessageUpdateSpeed = ConfigManager.MessageUpdateRate;
            viewmodel.MessageDownloadQty = ConfigManager.MessageCount;
            viewmodel.TimeUntilOffline = ConfigManager.ActivityTimeOut;

            return View(viewmodel);
        }

        [HttpPost]
        public IActionResult SaveConfig(ClientSettingsViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Save();
                model.SaveSuccessful = true;
            }

            return View("ClientSettings", model);
        }
    }
}
