using Microsoft.AspNetCore.Mvc;

namespace ChatrBox.Areas.Config.Controllers
{
    [Area("Config")]
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
