using BusinessLayer.Interfaces;
using EntityLayer;
using HaircuteUI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HaircuteUI.Controllers
{
    public class HaircutMenuCategoryController : Controller
    {
        private readonly IHaircutMenuCategoryService _menuCategoryService;

        public HaircutMenuCategoryController(IHaircutMenuCategoryService menuCategoryService)
        {
            _menuCategoryService = menuCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _menuCategoryService.GetAllAsync();
            return View(categories);
        }

    /*    public async Task<IActionResult> Details(int id)
        {
         *//*   var category = await _menuCategoryService.GetCategoryWithMenuItemsAsync(id);
            if (category == null) return NotFound();

            return View(category);*//*
        }*/

        [HttpPost]
        public async Task<IActionResult> Create(HaircutMenuCategoryViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

         
            var entity = new HaircutMenuCategory
            {
                Name = viewModel.Name,
                HaircutMenuItems = viewModel.MenuItems?.Select(item => new HaircutMenuItem
                {
                    Id = item.Id,           
                    Name = item.Name,
                    Price = item.Price
                }).ToList() ?? new List<HaircutMenuItem>()
            };

            // Pass the entity to the service
           /* await _menuCategoryService.AddAsync(entity);*/

            return RedirectToAction(nameof(Index));
        }

    }
}
