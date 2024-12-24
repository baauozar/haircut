using BusinessLayer.Interfaces;
using EntityLayer;
using HaircuteUI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HaircuteUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BeautyCardInfoController : Controller
    {
        private readonly IBeautyCardInfoService _service;

        public BeautyCardInfoController(IBeautyCardInfoService service)
        {
            _service = service;
        }
        // GET: BeautyCardInfo
        // Displays the main page with a list of non-deleted (IsDeleted == false) BeautyCardInfo entries
        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAllAsync();  // By default, your service should filter out soft-deleted entries
            var model = list.Select(c => new BeautyCardInfoViewModel
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                ImagePath = c.ImagePath,
            }).ToList();

            return View(model);  // We'll show how the Index view uses partials for dynamic updates
        }

        // GET: BeautyCardInfo/Create
        // Returns a partial view for creating a new BeautyCardInfo entry
        public IActionResult Create()
        {
            return PartialView("_Create", new BeautyCardInfoViewModel());
        }

        // POST: BeautyCardInfo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BeautyCardInfoViewModel vm)
        {
            if (!ModelState.IsValid)
                return PartialView("_Create", vm); // Return partial with validation errors

            var entity = new BeautyCardInfo
            {
                Title = vm.Title,
                Description = vm.Description,
                ImagePath = vm.ImagePath
            };

            await _service.AddAsync(entity);
            TempData["NotificationMessage"] = "Beauty Card Information Has been added successfully!";

            // Return the updated partial list
            return Json(new {success=true});
        }

        // GET: BeautyCardInfo/Edit/5
        // Returns a partial view for editing an existing BeautyCardInfo entry
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            var vm = new BeautyCardInfoViewModel
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                ImagePath = entity.ImagePath
            };

            return PartialView("_Edit", vm);
        }

        // POST: BeautyCardInfo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BeautyCardInfoViewModel vm)
        {
            if (!ModelState.IsValid)
                return PartialView("_Edit", vm); // Return partial with validation errors

            var entity = await _service.GetByIdAsync(vm.Id);
            if (entity == null)
                return NotFound();

            entity.Title = vm.Title;
            entity.Description = vm.Description;
            entity.ImagePath = vm.ImagePath;

            await _service.UpdateAsync(entity);
            TempData["NotificationMessage"] = "Beauty Card Information Details Has Been Edit successfully!";
            return Json(new {success=true});
        }

        // POST: BeautyCardInfo/Delete/5
        // Soft deletes the card info (IsDeleted = true) and returns updated partial list
        [HttpPost]
      
        public async Task<IActionResult> Delete(int id)
        {
            await _service.SoftDeleteAsync(id);
            TempData["NotificationMessage"] = "Card Information Has been Deleted successfully!";
            return Json(new {success=true});
        }
    }
}