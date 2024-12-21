using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using EntityLayer;
using HaircuteUI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HaircuteUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly IContactService _service;

        public ContactController(IContactService service)
        {
            _service = service;
        }

        // GET: Contact
        public async Task<IActionResult> Index()
        {
            var contact = await _service.GetAllAsync();
            var model = contact.Select(f => new ContactViewModal
            {
                Id = f.Id,
                Name = f.Name ?? "",
                LastName = f.LastName ?? "",
                phonenumber = f.phonenumber ?? "",
                Email = f.Email,
                Message = f.Message
            }).ToList();
            return View(model);
        }

        // GET: Contact/Create
        public IActionResult Create()
        {
            return PartialView("_Create", new ContactViewModal());
        }

        // POST: Contact/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactViewModal vm)
        {
            if (!ModelState.IsValid)
                return PartialView("_Create", vm);

            var contact = new Contact
            {
                Name = vm.Name,
                LastName = vm.LastName,
                phonenumber = vm.phonenumber,
                Email = vm.Email,
                Message = vm.Message

            };
            var list = await _service.GetAllAsync();
            var updatedModel = list.Select(f => new ContactViewModal
            {
                Id = f.Id,
                Name = f.Name ?? "",
                LastName = f.LastName ?? "",
                phonenumber = f.phonenumber ?? "",
                Email = f.Email ?? "",
                Message = f.Message ?? ""
            }).ToList();

            // Return updated partial

            return PartialView("_ContactList", updatedModel);
        }

        // GET: Contact/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var contact = await _service.GetByIdAsync(id);
            if (contact == null) return NotFound();
            var vm = new ContactViewModal
            {
                Id = contact.Id,
                Name = contact.Name ?? "",
                LastName = contact.LastName ?? "",
                phonenumber = contact.phonenumber ?? "",
                Email = contact.Email ?? "",
                Message = contact.Message ?? ""
            };

            return PartialView("_Edit", vm);
        }

        // POST: Contact/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ContactViewModal vm)
        {
            if (!ModelState.IsValid)
                return PartialView("_Edit", vm);

            var contact = await _service.GetByIdAsync(vm.Id);
            if (contact == null) return NotFound();

            // Update fields
            contact.Name = vm.Name;
            contact.LastName = vm.LastName;
            contact.phonenumber = vm.phonenumber;
            contact.Email = vm.Email;
            contact.Message = vm.Message;
            await _service.UpdateAsync(contact);

            // Return updated partial
            var list = await _service.GetAllAsync();
            var updateModel = list.Select(f => new ContactViewModal
            {
                Id = f.Id,
                Name = f.Name,
                LastName = f.LastName,
                phonenumber = f.phonenumber,
                Email = f.Email,
                Message = f.Message
            }).ToList();
            return PartialView("_ContactList", updateModel);
        }

        // POST: Contact/Delete/5 (Soft Delete)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.SoftDeleteAsync(id);

            var list = await _service.GetAllAsync();
            var updateModel = list.Select(f => new ContactViewModal
            {
                Id = f.Id,
                Name = f.Name,
                LastName = f.LastName,
                phonenumber = f.phonenumber,
                Email = f.Email,
                Message = f.Message
            }).ToList();
            return PartialView("_ContactList", updateModel);
        }
    }
}

