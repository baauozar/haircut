using BusinessLayer.Interfaces;
using EntityLayer;
using HaircuteUI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace HaircuteUI.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
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
        public async Task<IActionResult> Index(int?categoryid)
        {
            var categories=await _categoryService.GetAllAsync();
            IEnumerable<HaircutMenuItem> haircutMenuItems;
            if (categoryid.HasValue)
            {
                haircutMenuItems=await _menuItemService.GetAllWithCategoryAsync();
            }
            else
            {
                haircutMenuItems = await _menuItemService.GetAllAsync();
            }
            var haircutMenuItem=await _menuItemService.GetAllAsync();
            var viewModel = haircutMenuItems.Select(item => new HaircutMenuItemViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                Time = item.Time,
                HaircutMenuCategoryId = item.HaircutMenuCategoryId,
                CategoryName = categories.FirstOrDefault(c=>c.Id==item.HaircutMenuCategoryId)?.Name,
                IsDeleted = item.IsDeleted
            }).ToList();

            return View(viewModel);
        }

        // GET: HairCutMenuItem/Create
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
            return PartialView("_Create", new HaircutMenuItemViewModel());
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
                Time = model.Time,
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
            var vm = new HaircutMenuItemViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                Time = item.Time,
                HaircutMenuCategoryId = item.HaircutMenuCategoryId
               
              
            };

            return PartialView("_Edit", vm);
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
            existing.Time = model.Time;
            existing.HaircutMenuCategoryId = model.HaircutMenuCategoryId;

            await _menuItemService.UpdateAsync(existing);
            TempData["NotificationMessage"] = "Menu item edit successfully!";
            return Json(new { success = true});
        }

        // POST: HairCutMenuItem/Delete/5
        [HttpPost]
       
        public async Task<IActionResult> Delete(int id)
        {
            await _menuItemService.SoftDeleteAsync(id);
            TempData["NotificationMessage"] = "Menu item deleted successfully!";
            return Json(new { success = true});
        }
    }

}
