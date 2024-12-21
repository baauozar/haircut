using BusinessLayer.Interfaces;
using EntityLayer;
using HaircuteUI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HaircuteUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BeautyCategoryController : Controller
    {
        private readonly IBeautyCategoryService _categoryService;
        private readonly IBeautyItemsService _itemsService;

        public BeautyCategoryController(IBeautyCategoryService categoryService, IBeautyItemsService itemsService)
        {
            _categoryService = categoryService;
            _itemsService = itemsService;
        }

        // GET: BeautyCategory
        // Show main view that can load partials via AJAX
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync(); // excludes IsDeleted
            var model = categories.Select(cat => new BeautyCategoryViewModel
            {
                Id = cat.Id,
                Name = cat.Name ?? "",
                IconPath = cat.IconPath,
                IsDeleted = cat.IsDeleted,
                Items = (cat.BeautyItems ?? new List<BeautyItem>())
                        .Where(i => !i.IsDeleted)
                        .Select(i => new BeautyItemsViewModel
                        {
                            Id = i.Id,
                            BeautyCategoryId = i.BeautyCategoryId,
                            ServiceName = i.ServiceName ?? "",
                            Duration = i.Duration ?? "",
                            Price = i.Price,
                            Description = i.Description ?? "",
                            IsDeleted = i.IsDeleted
                        }).ToList()
            }).ToList();

            return View(model);
        }

        // GET: BeautyCategory/Create
        // Returns partial view for creating a new category
        public IActionResult Create()
        {
            return PartialView("_CreateCategory", new BeautyCategoryViewModel());
        }

        // POST: BeautyCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BeautyCategoryViewModel vm)
        {
            if (!ModelState.IsValid)
                return PartialView("_CreateCategory", vm);

            var entity = new BeautyCategory
            {
                Name = vm.Name,
                IconPath = vm.IconPath
            };

            await _categoryService.AddAsync(entity);

            // Return updated partial list
            var updatedCategories = await _categoryService.GetAllAsync();
            var updatedModel = updatedCategories.Select(cat => new BeautyCategoryViewModel
            {
                Id = cat.Id,
                Name = cat.Name ?? "",
                IconPath = cat.IconPath,
                IsDeleted = cat.IsDeleted
            }).ToList();

            return PartialView("_CategoryList", updatedModel);
        }

        // GET: BeautyCategory/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var cat = await _categoryService.GetByIdAsync(id);
            if (cat == null) return NotFound();

            var vm = new BeautyCategoryViewModel
            {
                Id = cat.Id,
                Name = cat.Name ?? "",
                IconPath = cat.IconPath,
                IsDeleted = cat.IsDeleted
            };

            return PartialView("_EditCategory", vm);
        }

        // POST: BeautyCategory/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BeautyCategoryViewModel vm)
        {
            if (!ModelState.IsValid)
                return PartialView("_EditCategory", vm);

            var existing = await _categoryService.GetByIdAsync(vm.Id);
            if (existing == null) return NotFound();

            existing.Name = vm.Name;
            existing.IconPath = vm.IconPath;

            await _categoryService.UpdateAsync(existing);

            // Return updated partial list
            var updatedCategories = await _categoryService.GetAllAsync();
            var updatedModel = updatedCategories.Select(cat => new BeautyCategoryViewModel
            {
                Id = cat.Id,
                Name = cat.Name ?? "",
                IconPath = cat.IconPath,
                IsDeleted = cat.IsDeleted
            }).ToList();

            return PartialView("_CategoryList", updatedModel);
        }

        // POST: BeautyCategory/Delete/5 (Soft Delete)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.SoftDeleteAsync(id);

            // Return updated partial list
            var updatedCategories = await _categoryService.GetAllAsync();
            var updatedModel = updatedCategories.Select(cat => new BeautyCategoryViewModel
            {
                Id = cat.Id,
                Name = cat.Name ?? "",
                IconPath = cat.IconPath
            }).ToList();

            return PartialView("_CategoryList", updatedModel);
        }

        //-------------------------------------
        // AJAX: Manage Items within a Category
        //-------------------------------------

        // POST: BeautyCategory/AddItem
        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] BeautyItemsViewModel vm)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Invalid data." });

            var newItem = new BeautyItem
            {
                ServiceName = vm.ServiceName,
                Duration = vm.Duration,
                Price = vm.Price,
                Description = vm.Description,
                BeautyCategoryId = vm.BeautyCategoryId
            };

            await _itemsService.AddAsync(newItem);

            // Return success + newly created item data
            var createdItem = new BeautyItemsViewModel
            {
                Id = newItem.Id,
                ServiceName = newItem.ServiceName ?? "",
                Duration = newItem.Duration ?? "",
                Price = newItem.Price,
                Description = newItem.Description ?? "",
                BeautyCategoryId = newItem.BeautyCategoryId,
                IsDeleted = newItem.IsDeleted
            };

            return Json(new { success = true, item = createdItem });
        }

        // POST: BeautyCategory/EditItem
        [HttpPost]
        public async Task<IActionResult> EditItem([FromBody] BeautyItemsViewModel vm)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Invalid data." });

            var existingItem = await _itemsService.GetByIdAsync(vm.Id);
            if (existingItem == null)
                return Json(new { success = false, message = "Item not found." });

            existingItem.ServiceName = vm.ServiceName;
            existingItem.Duration = vm.Duration;
            existingItem.Price = vm.Price;
            existingItem.Description = vm.Description;

            await _itemsService.UpdateAsync(existingItem);

            return Json(new { success = true });
        }

        // POST: BeautyCategory/DeleteItem (Soft Delete item)
        [HttpPost]
        public async Task<IActionResult> DeleteItem(int id)
        {

            if (id <= 0)
                return Json(new { success = false, message = "Invalid ID." });

            var success = await _itemsService.SoftDeleteAsync(id);
            if (!success)
                return Json(new { success = false, message = "Failed to delete item." });

            return Json(new { success = true });
        }
    }
}