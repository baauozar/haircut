using BusinessLayer.Interfaces;
using EntityLayer;
using HaircuteUI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HaircuteUI.Controllers
{
    public class FaqController : Controller
    {
        private readonly IFaqService _faqService;

        public FaqController(IFaqService faqService)
        {
            _faqService = faqService;
        }

        public async Task<IActionResult> Index()
        {
            var faqs = await _faqService.GetAllFaqsAsync();
          
            var model = faqs.Select(f => new FaqViewModel
            {
                Id = f.Id,
                Quastion = f.quastion ?? "",
                Answer = f.Answer ?? ""
            });
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FaqViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var faq = new Faq { quastion = vm.Quastion, Answer = vm.Answer };
            await _faqService.AddFaqAsync(faq);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var faq = await _faqService.GetFaqByIdAsync(id);
            if (faq == null) return NotFound();

            var vm = new FaqViewModel
            {
                Id = faq.Id,
                Quastion = faq.quastion ?? "",
                Answer = faq.Answer ?? ""
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FaqViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var faq = new Faq { Id = vm.Id, quastion = vm.Quastion, Answer = vm.Answer };
            await _faqService.UpdateFaqAsync(faq);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _faqService.DeleteFaqAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
