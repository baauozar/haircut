using BusinessLayer.Interfaces;
using EntityLayer;
using HaircuteUI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HaircuteUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
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
                quastion = f.quastion ?? "",
                Answer = f.Answer ?? "",
                IsDeleted = f.IsDeleted,
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
            // If model is invalid, return a JSON response that indicates an error
            if (!ModelState.IsValid)
            {
                // Collect any errors to show in console (or you can return them to display)
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();

                return Json(new
                {
                    success = false,
                    message = "Validation failed",
                    errors
                });
            }

            // If valid, create the entity
            var faq = new Faq
            {
                quastion = vm.quastion,
                Answer = vm.Answer,
                IsDeleted = false
            };

            await _faqService.AddAsync(faq);
            TempData["NotificationMessage"] = "Faq created successfully!";
        
            return Json(new
            {
                success = true
            });
        }

        // GET: Faq/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var faq = await _faqService.GetByIdAsync(id);
            if (faq == null) return NotFound();

            var vm = new FaqViewModel
            {
                Id = faq.Id,
                quastion = faq.quastion ?? "",
                Answer = faq.Answer ?? "",
                IsDeleted = faq.IsDeleted
            };
            return PartialView("_Edit", vm);
        }

        // POST: Faq/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FaqViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();

                return Json(new
                {
                    success = false,
                    message = "Validation failed",
                    errors
                });
            }
            var faq = await _faqService.GetByIdAsync(vm.Id);
            if (faq == null) return NotFound();

            faq.quastion = vm.quastion;
            faq.Answer = vm.Answer;
            faq.IsDeleted = vm.IsDeleted;
        


            await _faqService.UpdateAsync(faq);

            TempData["NotificationMessage"] = "Faq updated successfully!";
            return Json(new
            {
                success = true
            });
        }


        // POST: Faq/Delete/5 (Soft Delete)
        [HttpPost]
      
        public async Task<IActionResult> Delete(int id)
        {
            await _faqService.SoftDeleteAsync(id);

            TempData["NotificationMessage"] = "Item Deleted successfully!";


            return Json(new
            {
                success = true
               
            });
        }
    }
}

