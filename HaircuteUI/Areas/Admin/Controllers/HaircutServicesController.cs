using BusinessLayer.Interfaces;
using EntityLayer;
using HaircuteUI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HaircuteUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class HaircutServicesController : Controller
    {
        private readonly IHaircutServicesService _service;
        private readonly IHaircutServicesCategoryService _categoryService;
       

        public HaircutServicesController(IHaircutServicesService service, IHaircutServicesCategoryService categoryService)
        {
            _service = service;
            _categoryService = categoryService;
          
        }

        public async Task<IActionResult> Index(int?categoryid)
        {
            var categories = await _categoryService.GetAllAsync();
            IEnumerable<HaircutService> haircutMenuItems;
            if (categoryid.HasValue)
            {
                haircutMenuItems = await _service.GetServicesByCategoryAsync(categoryid.Value);
            }
            else
            {
                haircutMenuItems = await _service.GetAllAsync();
            }
            var haircutMenuItem = await _service.GetAllAsync();
            var viewModel = haircutMenuItems.Select(item => new HaircutServicesViewModel
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                ImagePath = item.ImagePath,
                ServiceCategoryId = item.ServiceCategoryId,
                ServiceCategory = categories.FirstOrDefault(c => c.Id == item.ServiceCategoryId)?.Name??"deleted category"
            }).ToList();
            return View(viewModel);
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
         
            return PartialView("_Create",new HaircutServicesViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HaircutServicesViewModel vm)
        {
            if (!ModelState.IsValid)
            {
              
                return PartialView("_Create",vm);
            }

            var entity = new HaircutService
            {
                Title = vm.Title,
                Description = vm.Description,
                ImagePath = vm.ImagePath,
                ServiceCategoryId = vm.ServiceCategoryId
            };

            await _service.AddAsync(entity);

            TempData["NotificationMessage"] = "Haircut services Has Been Create successfully!";

            return Json(new {success=true});
        }

        public async Task<IActionResult> Edit(int id)
        {
            var service = await _service.GetByIdAsync(id);
            if (service == null) return NotFound();

            var vm = new HaircutServicesViewModel
            {
                Id = service.Id,
                Title = service.Title ?? "",
                Description = service.Description,
                ImagePath = service.ImagePath ?? "",
                ServiceCategoryId = service.ServiceCategoryId
             
              
            };
            return PartialView("_Edit",vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(HaircutServicesViewModel vm)
        {
            if (!ModelState.IsValid)
            {
               
                return PartialView("_Edit",vm);
            }

            var entity = await _service.GetByIdAsync(vm.Id);
            if (entity == null) return NotFound();

            // Update main entity
            entity.Title = vm.Title;
            entity.Description = vm.Description;
            entity.ImagePath = vm.ImagePath;
            entity.ServiceCategoryId = vm.ServiceCategoryId;
            await _service.UpdateAsync(entity);
            TempData["NotificationMessage"] = "HairCut Services details Has Been Edit successfully!";

            return Json(new { success = true });
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.SoftDeleteAsync(id);
            TempData["NotificationMessage"] = "HairCut Services Has Been Delete successfully!";
            return Json(new { success = true });
        }
    }
}