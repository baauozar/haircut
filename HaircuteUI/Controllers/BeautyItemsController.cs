using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using EntityLayer;
using HaircuteUI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HaircuteUI.Controllers
{
    public class BeautyItemsController : Controller
    {
        private readonly IBeautyItemsService _service;

        public BeautyItemsController(IBeautyItemsService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _service.GetAllAsync();
            var model = items.Select(i => new BeautyItemsViewModel
            {
                Id = i.Id,
                ServiceName = i.ServiceName ?? "",
                Price = i.Price
            });
            return View(model);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(BeautyItemsViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var item = new EntityLayer.BeautyItem
            {
                ServiceName = vm.ServiceName,
                Price = vm.Price
                
            };

            await _service.AddAsync(item);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BeautyItemsViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var beatyitem = new BeautyItem { Id = vm.Id, ServiceName = vm.ServiceName };
            await _service.UpdateAsync(beatyitem);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
