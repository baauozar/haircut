using BusinessLayer.Interfaces;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;

namespace HaircuteUI.Controllers
{
    public class BeautysServicesController : Controller
    {
        private readonly IBeautysServicesService _service;

        public BeautysServicesController(IBeautysServicesService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAllAsync();
            return View(list);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(BeautysServices model)
        {
            if (!ModelState.IsValid) return View(model);
            await _service.AddAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BeautysServices model)
        {
            if (!ModelState.IsValid) return View(model);
            await _service.UpdateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
