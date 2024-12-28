using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using EntityLayer;
using HaircuteUI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HaircuteUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
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
            var model = contact.Select(f => new ContactViewModel
            {
                Id = f.Id,
                Name = f.Name ?? "",
                LastName = f.LastName ?? "",
                phonenumber = f.phonenumber ?? "",
                Email = f.Email,
                Message = f.Message,
                IsDeleted = f.IsDeleted
            }).ToList();
            return View(model);
        }

        // GET: Contact/Create
        public IActionResult Create()
        {
            return PartialView("_Create", new ContactViewModel());
        }

        // POST: Contact/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactViewModel vm)
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
            await _service.AddAsync(contact);
            TempData["NotificationMessage"] = "Faq created successfully!";

            return Json(new{ success = true}); 
        }
/*
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
          //  await _service.UpdateAsync(contact);
            TempData["NotificationMessage"] = "Contact Update successfully!";
            return Json(new { success = true});
        }*/

        // POST: Contact/Delete/5 (Soft Delete)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.SoftDeleteAsync(id);
            TempData["NotificationMessage"] = "Contact Deleted successfully!";
            return Json(new{ success = true });
        }
    }
}

