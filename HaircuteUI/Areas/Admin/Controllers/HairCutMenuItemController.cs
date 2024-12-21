using Microsoft.AspNetCore.Mvc;

namespace HaircuteUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HairCutMenuItemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
