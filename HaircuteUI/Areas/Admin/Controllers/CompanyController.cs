using BusinessLayer.Interfaces;
using EntityLayer;
using HaircuteUI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HaircuteUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _service;

        public CompanyController(ICompanyService service)
        {
            _service = service;
        }

        // GET: Company
        public async Task<IActionResult> Index()
        {
            // Fetch non-deleted companies
            var company = await _service.GetAllAsync();
            var model = company.Select(f => new CompanyViewModal
            {
                Id = f.Id,
                smallTitle = f.smallTitle,
                bigTitle = f.bigTitle,
                BaackgroundTitle = f.BaackgroundTitle,
                Section = f.Section,
                Signature = f.Signature


            }).ToList();
            return View(model);

        }

        // GET: Company/Create
        public IActionResult Create()
        {
            // Return partial view for the 'Create' form
            return PartialView("_Create", new CompanyViewModal());
        }

        // POST: Company/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyViewModal vm)
        {
            if (!ModelState.IsValid)
                return PartialView("_Create", vm); // Return partial with validation errors
            var company = new Company
            {
                smallTitle = vm.smallTitle,
                bigTitle = vm.bigTitle,
                BaackgroundTitle = vm.BaackgroundTitle,
                Section = vm.Section,
                Signature = vm.Signature
            };
            await _service.AddAsync(company);

            // Fetch updated list
            var list = await _service.GetAllAsync();
            var updateModel = list.Select(f => new CompanyViewModal
            {
                Id = f.Id,
                smallTitle = f.smallTitle,
                bigTitle = f.bigTitle,
                BaackgroundTitle = f.BaackgroundTitle,
                Section = f.Section,
                Signature = f.Signature

            }).ToList();
            // Return partial with updated data
            return PartialView("_CompanyList", updateModel);
        }

        // GET: Company/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var company = await _service.GetByIdAsync(id);
            if (company == null) return NotFound();
            var vm = new CompanyViewModal
            {
                Id = company.Id,
                smallTitle = company.smallTitle,
                bigTitle = company.bigTitle,
                BaackgroundTitle = company.BaackgroundTitle,
                Section = company.Section,
                Signature = company.Signature

            };
            // Return partial view for edit form
            return PartialView("_Edit", vm);
        }

        // POST: Company/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CompanyViewModal vm)
        {
            if (!ModelState.IsValid)
                return PartialView("_Edit", vm);

            var company = await _service.GetByIdAsync(vm.Id);
            if (company == null) return NotFound();

            // Update fields as needed
            company.smallTitle = vm.smallTitle;
            company.bigTitle = vm.bigTitle;
            company.BaackgroundTitle = vm.BaackgroundTitle;
            company.Section = vm.Section;
            company.Signature = vm.Signature;

            await _service.UpdateAsync(company);

            // Fetch updated list
            var list = await _service.GetAllAsync();
            var updateModel = list.Select(f => new CompanyViewModal
            {
                Id = f.Id,
                smallTitle = f.smallTitle,
                bigTitle = f.bigTitle,
                BaackgroundTitle = f.BaackgroundTitle,
                Section = f.Section,
                Signature = f.Signature
            }).ToList();
            return PartialView("_CompanyList", updateModel);
        }

        // POST: Company/Delete/5 (Soft Delete)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.SoftDeleteAsync(id); // Soft delete (IsDeleted = true)

            // Fetch updated list
            var list = await _service.GetAllAsync();
            var udpateModel = list.Select(f => new CompanyViewModal
            {
                Id = f.Id,
                smallTitle = f.smallTitle,
                bigTitle = f.bigTitle,
                BaackgroundTitle = f.BaackgroundTitle,
                Section = f.Section,
                Signature = f.Signature
            }).ToList();
            return PartialView("_CompanyList", udpateModel);
        }
    }
}