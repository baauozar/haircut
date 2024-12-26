using BusinessLayer.Interfaces;
using EntityLayer;
using HaircuteUI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HaircuteUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HaircutServicesController : Controller
    {
        private readonly IHaircutServicesService _service;
        private readonly IHaircutServicesCategoryService _categoryService;
        private readonly IHairCutSupServicesService _subService;

        public HaircutServicesController(IHaircutServicesService service, IHaircutServicesCategoryService categoryService, IHairCutSupServicesService subService)
        {
            _service = service;
            _categoryService = categoryService;
            _subService = subService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var vm = new HaircutServicesViewModel
            {
                Categories = await _categoryService.GetAllAsync()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(HaircutServicesViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Categories = await _categoryService.GetAllAsync();
                return View(vm);
            }

            var entity = new HaircutService
            {
                Title = vm.Title,
                Description = vm.Description,
                ImagePath = vm.ImagePath,
                ServiceCategoryId = vm.ServiceCategoryId
            };

            await _service.AddAsync(entity);

            // entity.Id is now available if needed after Add + SaveChanges (done inside AddAsync)
            // If you need sub-services creation at creation time:
            foreach (var s in vm.SubServices)
            {
                var subEntity = new HaircutSupService
                {
                    Name = s.Name,
                    Description = s.Description,
                    ServiceId = entity.Id
                };
                await _subService.AddAsync(subEntity);
            }

            return RedirectToAction(nameof(Index));
        }

     /*   public async Task<IActionResult> Edit(int id)
        {
            var service = await _service.GetServiceWithSubServicesAsync(id);
            if (service == null) return NotFound();

            var vm = new HaircutServicesViewModel
            {
                Id = service.Id,
                Title = service.Title ?? "",
                Description = service.Description,
                ImagePath = service.ImagePath ?? "",
                ServiceCategoryId = service.ServiceCategoryId,
                Categories = await _categoryService.GetAllAsync(),
                SubServices = service.HairCutSupServices?
                              .Select(x => new HairCutSupServicesViewModel
                              {
                                  Id = x.Id,
                                  Name = x.Name ?? "",
                                  Description = x.Description
                              }).ToList() ?? new()
            };
            return View(vm);
        }*/

        [HttpPost]
        public async Task<IActionResult> Edit(HaircutServicesViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Categories = await _categoryService.GetAllAsync();
                return View(vm);
            }

            var entity = await _service.GetByIdAsync(vm.Id);
            if (entity == null) return NotFound();

            // Update main entity
            entity.Title = vm.Title;
            entity.Description = vm.Description;
            entity.ImagePath = vm.ImagePath;
            entity.ServiceCategoryId = vm.ServiceCategoryId;
            await _service.UpdateAsync(entity);

            // Handle sub-services
            var existingSubs = await _subService.GetByServiceIdAsync(entity.Id);
            var vmSubs = vm.SubServices ?? new();

            // Delete sub-services not in VM
            var toDelete = existingSubs.Where(es => !vmSubs.Any(vs => vs.Id == es.Id)).ToList();
            foreach (var del in toDelete)
                await _subService.SoftDeleteAsync(del.Id);

            // Add or update subs from VM
            foreach (var vs in vmSubs)
            {
                if (vs.Id == 0)
                {
                    // New sub-service
                    var newSub = new HaircutSupService
                    {
                        Name = vs.Name,
                        Description = vs.Description,
                        ServiceId = entity.Id
                    };
                    await _subService.AddAsync(newSub);
                }
                else
                {
                    // Update existing
                    var es = existingSubs.First(x => x.Id == vs.Id);
                    es.Name = vs.Name;
                    es.Description = vs.Description;
                    await _subService.UpdateAsync(es);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.SoftDeleteAsync(id);
            // If cascade delete isn't set, also delete sub-services manually:
            var subs = await _subService.GetByServiceIdAsync(id);
            foreach (var s in subs) await _subService.SoftDeleteAsync(s.Id);

            return RedirectToAction(nameof(Index));
        }
    }
}