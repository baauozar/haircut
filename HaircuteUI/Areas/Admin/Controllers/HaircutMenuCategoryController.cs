using BusinessLayer.Interfaces;
using EntityLayer;
using HaircuteUI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HaircuteUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class HaircutMenuCategoryController : Controller
    {
        private readonly IHaircutMenuCategoryService _menuCategoryService;

        public HaircutMenuCategoryController(IHaircutMenuCategoryService menuCategoryService)
        {
            _menuCategoryService = menuCategoryService;
        }

        // GET: BeautyCategory
        // Show main view that can load partials via AJAX
        public async Task<IActionResult> Index()
        {
            var categories = await _menuCategoryService.GetAllAsync(); // excludes IsDeleted
            var model = categories.Select(f => new HaircutMenuCategoryViewModel
            {
                Id = f.Id,
                Name = f.Name,
            }).ToList();



            return View(model);
        }

        // GET: BeautyCategory/Create
        // Returns partial view for creating a new category
        public IActionResult Create()
        {
            return PartialView("_Create", new BeautyCategoryViewModel());
        }

        // POST: BeautyCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BeautyCategoryViewModel vm)
        {
            if (!ModelState.IsValid)
                return PartialView("_Create", vm);

            var entity = new HaircutMenuCategory
            {
                Name = vm.Name
          
            };

            await _menuCategoryService.AddAsync(entity);
            TempData["NotificationMessage"] = "New Haircut Category has been added successfully!";


            return Json(new { success = true });
        }

        // GET: BeautyCategory/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var cat = await _menuCategoryService.GetByIdAsync(id);
            if (cat == null) return NotFound();

            var vm = new HaircutMenuCategoryViewModel
            {
                Id = cat.Id,
                Name = cat.Name ?? "",
            };

            return PartialView("_Edit", vm);
        }

        // POST: BeautyCategory/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(HaircutMenuCategoryViewModel vm)
        {
            if (!ModelState.IsValid)
                return PartialView("_Edit", vm);

            var existing = await _menuCategoryService.GetByIdAsync(vm.Id);
            if (existing == null) return NotFound();

            existing.Name = vm.Name;
            await _menuCategoryService.UpdateAsync(existing);
            TempData["NotificationMessage"] = "Haircut Category Has been updated successfully!";
            return Json(new { success = true });
        }

        // POST: BeautyCategory/Delete/5 (Soft Delete)
        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            await _menuCategoryService.SoftDeleteAsync(id);
            TempData["NotificationMessage"] = "Haircut Category Has been deleted successfully!";


            return Json(new { success = true });
        }


    }
}
