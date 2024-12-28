using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using EntityLayer;
using HaircuteUI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HaircuteUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class HairCutSupServicesController : Controller
    {
        private readonly IHairCutSupServicesService _service;
        private readonly IHaircutServicesService _haircutService; // For related data

        public HairCutSupServicesController(IHairCutSupServicesService service, IHaircutServicesService haircutService)
        {
            _service = service;
            _haircutService = haircutService;
        }

        public async Task<IActionResult> Index(int? categoryid)
        {
            var categories=await _haircutService.GetAllAsync();
            IEnumerable<HaircutSupService> haircut;
            if (categoryid.HasValue)
            {
                haircut=await _service.GetByServiceIdAsync(categoryid.Value);
            }
            else
            {
                haircut=await _service.GetAllAsync();
            }
  
            var model = haircut.Select(s => new HairCutSupServicesViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                ServiceId = s.ServiceId,
                ServiceName = categories.FirstOrDefault(c=>c.Id==s.ServiceId)?.Title ?? "N/A"
            }).ToList();

            return View(model);
        }
        [HttpGet]
        public async Task<JsonResult> GetCategories()
        {
            try
            {
                // Fetch categories using the service
                var categories = await _haircutService.GetAllAsync();

                // Return the data in the required format
                return Json(categories.Select(c => new
                {
                    id = c.Id,       // ID of the category
                    name = c.Title    // Name of the category
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
         
            return PartialView("_Create", new HairCutSupServicesViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HairCutSupServicesViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message ="Invalid Data"});
            }

            var service = new HaircutSupService
            {
                Name = vm.Name,
                Description = vm.Description,
                ServiceId = vm.ServiceId
            };
            await _service.AddAsync(service);
            TempData["NotificationMessage"] = "Sub-Service Has Been Create successfully!";
            return Json(new { success = true });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var service = await _service.GetByIdAsync(id);
            if (service == null) return NotFound();

            var vm = new HairCutSupServicesViewModel
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                ServiceId = service.ServiceId
            };

            return PartialView("_Edit",vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(HairCutSupServicesViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Edit", vm);
            }

            var existing = await _service.GetByIdAsync(vm.Id);
            if (existing == null) return NotFound();

            existing.Name = vm.Name;
            existing.Description = vm.Description;
            existing.ServiceId = vm.ServiceId;

            await _service.UpdateAsync(existing);
            TempData["NotificationMessage"] = "Sub-Service details Has Been Edit successfully!";
            return Json(new { success = true });
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.SoftDeleteAsync(id);
            TempData["NotificationMessage"] = "Sub-Service Has been deleted successfully!";
            return Json(new { success = true });
        }

      
    }
}