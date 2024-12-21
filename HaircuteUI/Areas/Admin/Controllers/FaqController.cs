using BusinessLayer.Interfaces;
using EntityLayer;
using HaircuteUI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HaircuteUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FaqController : Controller
    {
        private readonly IFaqService _faqService;

        public FaqController(IFaqService faqService)
        {
            _faqService = faqService;
        }

        // GET: Faq
        public async Task<IActionResult> Index()
        {
            var faqs = await _faqService.GetAllAsync();
            var model = faqs.Select(f => new FaqViewModel
            {
                Id = f.Id,
                Quastion = f.quastion ?? "",
                Answer = f.Answer ?? ""
            }).ToList();

            return View(model);
        }

        // GET: Faq/Create
        public IActionResult Create()
        {
            return PartialView("_Create", new FaqViewModel());
        }

        // POST: Faq/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FaqViewModel vm)
        {
            if (!ModelState.IsValid)
                return PartialView("_Create", vm);

            var faq = new Faq
            {
                quastion = vm.Quastion,
                Answer = vm.Answer
            };

            await _faqService.AddAsync(faq);

            // Return updated partial
            var list = await _faqService.GetAllAsync();
            var updatedModel = list.Select(f => new FaqViewModel
            {
                Id = f.Id,
                Quastion = f.quastion ?? "",
                Answer = f.Answer ?? ""
            }).ToList();

            return PartialView("_FaqList", updatedModel);
        }

        // GET: Faq/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var faq = await _faqService.GetByIdAsync(id);
            if (faq == null) return NotFound();

            var vm = new FaqViewModel
            {
                Id = faq.Id,
                Quastion = faq.quastion ?? "",
                Answer = faq.Answer ?? ""
            };
            return PartialView("_Edit", vm);
        }

        // POST: Faq/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FaqViewModel vm)
        {
            if (!ModelState.IsValid)
                return PartialView("_Edit", vm);

            var faq = await _faqService.GetByIdAsync(vm.Id);
            if (faq == null) return NotFound();

            faq.quastion = vm.Quastion;
            faq.Answer = vm.Answer;

            await _faqService.UpdateAsync(faq);

            // Return updated partial
            var list = await _faqService.GetAllAsync();
            var updatedModel = list.Select(f => new FaqViewModel
            {
                Id = f.Id,
                Quastion = f.quastion ?? "",
                Answer = f.Answer ?? ""
            }).ToList();

            return PartialView("_FaqList", updatedModel);
        }

        // POST: Faq/Delete/5 (Soft Delete)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _faqService.SoftDeleteAsync(id);

            var list = await _faqService.GetAllAsync();
            var updatedModel = list.Select(f => new FaqViewModel
            {
                Id = f.Id,
                Quastion = f.quastion ?? "",
                Answer = f.Answer ?? ""
            }).ToList();

            return PartialView("_FaqList", updatedModel);
        }
    }
}

