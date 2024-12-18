using BusinessLayer.Interfaces;
using EntityLayer;
using HaircuteUI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HaircuteUI.Controllers
{
    public class BeautyCategoryController : Controller
    {
        private readonly IBeautyCategoryService _categoryService;
        private readonly IBeautyItemsService _itemsService;

        public BeautyCategoryController(IBeautyCategoryService categoryService, IBeautyItemsService itemsService)
        {
            _categoryService = categoryService;
            _itemsService = itemsService;
        }

        /*public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllWithItemsAsync();
            // Map to ViewModel
            var model = categories.Select(c => new BeautyCategoryViewModel
            {
                Id = c.Id,
                Name = c.Name ?? "",
               
                Items = (c.BeautyItems ?? new List<BeautyItems>())
                          .Select(i => new BeautyItemsViewModel
                          {
                              Id = i.Id,
                              ServiceName = i.ServiceName ?? "",
                              Price = i.Price,
                             
                          }).ToList()
            });
            return View(model);
        }

        public IActionResult Create()
        {
            return View(new BeautyCategoryViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(BeautyCategoryViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var entity = new BeautyCategory
            {
                Name = vm.Name
                
            };

            await _categoryService.AddAsync(entity);

            // If you want to add items now or later, handle similarly as done with sub-services:
            // For simplicity, assume no items on create or handle them similarly as we did in HaircutServices.

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var cat = await _categoryService.GetCategoryWithItemsAsync(id);
            if (cat == null) return NotFound();

            var vm = new BeautyCategoryViewModel
            {
                Id = cat.Id,
                Name = cat.Name ?? "",
              
                Items = (cat.BeautyItems ?? new List<BeautyItems>())
                         .Select(i => new BeautyItemsViewModel
                         {
                             Id = i.Id,
                             ServiceName = i.ServiceName ?? "",
                             Price = i.Price,
                             DisplayOrder = i.DisplayOrder
                         }).ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BeautyCategoryViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var entity = await _categoryService.GetByIdAsync(vm.Id);
            if (entity == null) return NotFound();

            entity.Name = vm.Name;
            entity.DisplayOrder = vm.DisplayOrder;

            await _categoryService.UpdateAsync(entity);

            // Handle items:
            var existingItems = await _itemsService.GetAllAsync();
            var categoryItems = existingItems.Where(i => i.BeautyCategoryId == vm.Id).ToList();
            var vmItems = vm.Items ?? new();

            // Delete items not in VM
            var toDelete = categoryItems.Where(ei => !vmItems.Any(vi => vi.Id == ei.Id)).ToList();
            foreach (var del in toDelete) await _itemsService.DeleteAsync(del.Id);

            // Add or update items
            foreach (var vi in vmItems)
            {
                if (vi.Id == 0)
                {
                    // New item
                    var newItem = new BeautyItems
                    {
                        ServiceName = vi.ServiceName,
                        Price = vi.Price,
                        
                        BeautyCategoryId = vm.Id
                    };
                    await _itemsService.AddAsync(newItem);
                }
                else
                {
                    // Update existing
                    var ei = categoryItems.First(x => x.Id == vi.Id);
                    ei.ServiceName = vi.ServiceName;
                    ei.Price = vi.Price;
                 
                    await _itemsService.UpdateAsync(ei);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            // Delete category and related items
            await _categoryService.DeleteAsync(id);
            // If cascade is not in DB, manually delete items:
            var items = (await _itemsService.GetAllAsync()).Where(i => i.BeautyCategoryId == id);
            foreach (var it in items)
                await _itemsService.DeleteAsync(it.Id);

            return RedirectToAction(nameof(Index));
        }*/
    }
}