using BusinessLayer.Interfaces;
using EntityLayer;
using HaircuteUI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HaircuteUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class HaircutServicesCategoryController : Controller
    {
        private readonly IHaircutServicesCategoryService _service;

        public HaircutServicesCategoryController(IHaircutServicesCategoryService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAllAsync();  // By default, your service should filter out soft-deleted entries
            var model = list.Select(c => new HaircutServicesCategoryViewModel
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            return View(model);  // We'll show how the Index view uses partials for dynamic updates
        }

        // GET: BeautyCardInfo/Create
        // Returns a partial view for creating a new BeautyCardInfo entry
        public IActionResult Create()
        {
            return PartialView("_Create", new HaircutServicesCategoryViewModel());
        }

        // POST: BeautyCardInfo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HaircutServicesCategoryViewModel vm)
        {
            if (!ModelState.IsValid)
                return PartialView("_Create", vm); // Return partial with validation errors

            var entity = new HaircutServicesCategory
            {
                Name = vm.Name
            };

            await _service.AddAsync(entity);
            TempData["NotificationMessage"] = "Hair Cut Category Information Has been added successfully!";

            // Return the updated partial list
            return Json(new { success = true });
        }

        // GET: BeautyCardInfo/Edit/5
        // Returns a partial view for editing an existing BeautyCardInfo entry
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            var vm = new HaircutServicesCategoryViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
              
            };

            return PartialView("_Edit", vm);
        }

        // POST: BeautyCardInfo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(HaircutServicesCategoryViewModel vm)
        {
            if (!ModelState.IsValid)
                return PartialView("_Edit", vm); // Return partial with validation errors

            var entity = await _service.GetByIdAsync(vm.Id);
            if (entity == null)
                return NotFound();

            entity.Name = vm.Name;
          

            await _service.UpdateAsync(entity);
            TempData["NotificationMessage"] = "Hair Cut Category Information Details Has Been Edit successfully!";
            return Json(new { success = true });
        }

        // POST: BeautyCardInfo/Delete/5
        // Soft deletes the card info (IsDeleted = true) and returns updated partial list
        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            await _service.SoftDeleteAsync(id);
            TempData["NotificationMessage"] = "Hair Cut Category Has been Deleted successfully!";
            return Json(new { success = true });
        }
    }
}
