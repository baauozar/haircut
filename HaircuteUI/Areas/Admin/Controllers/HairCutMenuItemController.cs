using BusinessLayer.Interfaces;
using EntityLayer;
using HaircuteUI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace HaircuteUI.Controllers
{
    [Area("Admin")]
    public class HairCutMenuItemController : Controller
    {
        private readonly IHaircutMenuItemService _menuItemService;
        private readonly IHaircutMenuCategoryService _categoryService;

        public HairCutMenuItemController(IHaircutMenuItemService menuItemService,
                                         IHaircutMenuCategoryService categoryService)
        {
            _menuItemService = menuItemService;
            _categoryService = categoryService;
        }

        // GET: HairCutMenuItem
        public async Task<IActionResult> Index()
        {
            var items = await _menuItemService.GetAllWithCategoryAsync();
            var viewModel = items.Select(item => new HaircutMenuItemViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                HaircutMenuCategoryId = item.HaircutMenuCategoryId,
                CategoryName = item.HaircutMenuCategory?.Name,
                IsDeleted = item.IsDeleted
            }).ToList();

            return View(viewModel);
        }

        // GET: HairCutMenuItem/Create
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllAsync();
            var viewModel = new HaircutMenuItemViewModel
            {
                Categories = categories.Select(c => new HaircutMenuCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
            };

            return PartialView("_Create", viewModel);
        }

        // POST: HairCutMenuItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HaircutMenuItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }

            var entity = new HaircutMenuItem
            {
                Name = model.Name,
                Price = model.Price,
                HaircutMenuCategoryId = model.HaircutMenuCategoryId
            };

            await _menuItemService.AddAsync(entity);
            TempData["NotificationMessage"] = "Menu item created successfully!";
            return Json(new { success = true });
        }

        // GET: HairCutMenuItem/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _menuItemService.GetByIdAsync(id);
            if (item == null) return NotFound();

            var categories = await _categoryService.GetAllAsync();
            var viewModel = new HaircutMenuItemViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                HaircutMenuCategoryId = item.HaircutMenuCategoryId,
                Categories = categories.Select(c => new HaircutMenuCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
            };

            return PartialView("_Edit", viewModel);
        }

        // POST: HairCutMenuItem/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(HaircutMenuItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }

            var existing = await _menuItemService.GetByIdAsync(model.Id);
            if (existing == null) return NotFound();

            existing.Name = model.Name;
            existing.Price = model.Price;
            existing.HaircutMenuCategoryId = model.HaircutMenuCategoryId;

            await _menuItemService.UpdateAsync(existing);
            TempData["NotificationMessage"] = "Menu item edit successfully!";
            return Json(new { success = true});
        }

        // POST: HairCutMenuItem/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _menuItemService.SoftDeleteAsync(id);
            TempData["NotificationMessage"] = "Menu item deleted successfully!";
            return Json(new { success = true});
        }
    }

}
