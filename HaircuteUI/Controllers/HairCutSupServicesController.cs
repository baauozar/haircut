using BusinessLayer.Interfaces;
using EntityLayer;
using HaircuteUI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HaircuteUI.Controllers
{
    public class HairCutSupServicesController : Controller
    {
        private readonly IHairCutSupServicesService _service;
        private readonly IHaircutServicesService _haircutService; // For related data

        public HairCutSupServicesController(IHairCutSupServicesService service, IHaircutServicesService haircutService)
        {
            _service = service;
            _haircutService = haircutService;
        }

        public async Task<IActionResult> Index()
        {
            var services = await _service.GetAllAsync();
            var model = services.Select(s => new HairCutSupServicesViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                ServiceId = s.ServiceId,
                ServiceName = s.HaircutService?.Title ?? "N/A"
            }).ToList();

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Services = await _haircutService.GetAllAsync(); // Dropdown list
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(HairCutSupServicesViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Services = await _haircutService.GetAllAsync();
                return View(vm);
            }

            var service = new HaircutSupService
            {
                Name = vm.Name,
                Description = vm.Description,
                ServiceId = vm.ServiceId
            };
            await _service.AddAsync(service);
            return RedirectToAction(nameof(Index));
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

            ViewBag.Services = await _haircutService.GetAllAsync();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(HairCutSupServicesViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Services = await _haircutService.GetAllAsync();
                return View(vm);
            }

            var existing = await _service.GetByIdAsync(vm.Id);
            if (existing == null) return NotFound();

            existing.Name = vm.Name;
            existing.Description = vm.Description;
            existing.ServiceId = vm.ServiceId;

            await _service.UpdateAsync(existing);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var service = await _service.GetByIdAsync(id);
            if (service == null) return NotFound();

            return View(new HairCutSupServicesViewModel
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description
            });
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}