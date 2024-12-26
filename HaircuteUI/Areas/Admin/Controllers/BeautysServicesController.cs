using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using EntityLayer;
using HaircuteUI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HaircuteUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BeautysServicesController : Controller
    {
        private readonly IBeautyServices _service;
        private readonly IBeautyServicesItemService _serviceItem;

        public BeautysServicesController(IBeautyServices service, IBeautyServicesItemService serviceItem)
        {
            _service = service;
            _serviceItem = serviceItem;
        }

        public async Task<IActionResult> Index(int? serviceid)
        {
            var serviceitemcategories = await _serviceItem.GetAllAsync();
            IEnumerable<BeautyService> beautyservices;
            if (serviceid.HasValue)
            {
                beautyservices = await _service.GetByCategoryIdAsync(serviceid.Value);
            }
            else
            {
                beautyservices = await _service.GetAllAsync();
            }
            var beautyservice = await _service.GetAllAsync();

            // Map beauty items to the view model, including the category name
            var model = beautyservices.Select(item => new BeautyServiceViewModel
            {
                Id = item.Id,
                Heading = item.Heading ?? "",
                Subheading = item.Subheading??"",
                Description = item.Description,
                BeautyServicesItemId = item.BeautyServicesItemId,
                BeautyServicesItem = serviceitemcategories.FirstOrDefault(c => c.Id == item.BeautyServicesItemId)?.Title??"Deleted item",
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
                var categories = await _serviceItem.GetAllAsync();

                // Return the data in the required format
                return Json(categories.Select(c => new
                {
                    id = c.Id,       // ID of the category
                    title = c.Title    // Name of the category
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
            return PartialView("_Create", new BeautyServiceViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BeautyServiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var beautyItem = new BeautyService
                {
                    Heading = model.Heading,
                    Subheading = model.Subheading,
                    Description = model.Description,
                    BeautyServicesItemId = model.BeautyServicesItemId
                };

                await _service.AddAsync(beautyItem);
                TempData["NotificationMessage"] = "Beauty service Has Been Create successfully!";
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Invalid data." });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            var vm = new BeautyServiceViewModel
            {
                Id = item.Id,
                Heading = item.Heading,
                Subheading = item.Subheading,
                Description = item.Description,
                BeautyServicesItemId = item.BeautyServicesItemId
            };
            return PartialView("_Edit", vm);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BeautyServiceViewModel vm)
        {
            if (!ModelState.IsValid)
                return PartialView("_Edit", vm);

            var entity = await _service.GetByIdAsync(vm.Id);
            if (entity == null)
            {
                return NotFound();
            }
            entity.Heading = vm.Heading;
            entity.Subheading = vm.Subheading;
            entity.Description = vm.Description;
            entity.BeautyServicesItemId = vm.BeautyServicesItemId;
            await _service.UpdateAsync(entity);
            TempData["NotificationMessage"] = "Beauty service details Has Been Edit successfully!";
            return Json(new { success = true });

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            await _service.SoftDeleteAsync(id);
            TempData["NotificationMessage"] = "Beauty service Has been deleted successfully!";
            return Json(new { success = true });
        }
    }
}
