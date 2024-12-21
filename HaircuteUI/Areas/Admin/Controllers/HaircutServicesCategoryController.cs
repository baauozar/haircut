using Microsoft.AspNetCore.Mvc;

namespace HaircuteUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HaircutServicesCategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
