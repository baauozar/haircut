using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using EntityLayer;
using HaircuteUI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HaircuteUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    public class BeautyItemsController : Controller
    {
        private readonly IBeautyCategoryService _categoryService;
        private readonly IBeautyItemsService _BeautyitemsService;

        public BeautyItemsController(IBeautyCategoryService categoryService, IBeautyItemsService BeautyitemsService)
        {
            _categoryService = categoryService;
            _BeautyitemsService = BeautyitemsService;
        }
        public async Task<IActionResult> Index(int?categoryid)
        {
            var categories = await _categoryService.GetAllAsync();
            IEnumerable<BeautyItem> beautyItems;
            if (categoryid.HasValue)
            {
                beautyItems = await _BeautyitemsService.GetByCategoryIdAsync(categoryid.Value);
            }
            else
            {
                beautyItems = await _BeautyitemsService.GetAllAsync();
            }
            var beautyitems = await _BeautyitemsService.GetAllAsync();

            // Map beauty items to the view model, including the category name
            var model = beautyItems.Select(item => new BeautyItemsViewModel
            {
                Id = item.Id,
                ServiceName = item.ServiceName ?? "",
                Duration = item.Duration,
                Price = item.Price,
                Description = item.Description,
                BeautyCategoryId = item.BeautyCategoryId,
                CategoryName = categories.FirstOrDefault(c => c.Id == item.BeautyCategoryId)?.Name ?? "Deleted Category",
                IsDeleted = item.IsDeleted
            }).ToList();

            return View(model);
        }
        [HttpGet]
        public async Task<JsonResult> GetCategories()
        {
            try
            {
                // Fetch categories using the service
                var categories = await _categoryService.GetAllAsync();

                // Return the data in the required format
                return Json(categories.Select(c => new
                {
                    id = c.Id,       // ID of the category
                    name = c.Name    // Name of the category
                }));
            }
            catch (Exception ex)
            {
                // Log the error
                return Json(new { error = ex.Message });
            }
        }
        public IActionResult Create()
        {
            return PartialView("_Create",new BeautyItemsViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BeautyItemsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var beautyItem = new BeautyItem
                {
                    ServiceName = model.ServiceName,
                    Duration = model.Duration,
                    Price = model.Price,
                    Description = model.Description,
                    BeautyCategoryId = model.BeautyCategoryId
                };

                await _BeautyitemsService.AddAsync(beautyItem);
                TempData["NotificationMessage"] = "Beauty items Has Been Create successfully!";
                return Json(new { success = true});
            }

            return Json(new { success = false, message = "Invalid data." });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _BeautyitemsService.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            var vm = new BeautyItemsViewModel
            {
                Id = item.Id,
                ServiceName = item.ServiceName,
                Duration = item.Duration,
                Price = item.Price,
                Description = item.Description,
                BeautyCategoryId = item.BeautyCategoryId
            };
            return PartialView("_Edit", vm);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BeautyItemsViewModel vm)
        {
            if (!ModelState.IsValid)
                return PartialView("_Edit", vm);

            var entity = await _BeautyitemsService.GetByIdAsync(vm.Id);
            if (entity == null)
            {
                return NotFound();
            }
            entity.ServiceName = vm.ServiceName;
            entity.Duration = vm.Duration;
            entity.Price = vm.Price;
            entity.Description = vm.Description;
            entity.BeautyCategoryId= vm.BeautyCategoryId;
            await _BeautyitemsService.UpdateAsync(entity);
            TempData["NotificationMessage"] = "Beauty items details Has Been Edit successfully!";
            return Json(new { success = true });

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
          
            await _BeautyitemsService.SoftDeleteAsync(id);
            TempData["NotificationMessage"] = "Beauty item Has been deleted successfully!";
            return Json(new { success = true});
        }
    }
}
