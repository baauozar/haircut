﻿using BusinessLayer.Interfaces;
using EntityLayer;
using HaircuteUI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HaircuteUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class BeautyCategoryController : Controller
    {
        private readonly IBeautyCategoryService _categoryService;
   

        public BeautyCategoryController(IBeautyCategoryService categoryService)
        {
            _categoryService = categoryService;
           
        }

        // GET: BeautyCategory
        // Show main view that can load partials via AJAX
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync(); // excludes IsDeleted
            var model = categories.Select(f => new BeautyCategoryViewModel
            {
                Id = f.Id,
                Name = f.Name,
                IconPath = f.IconPath,
                ImagePath = f.ImagePath,
                IsDeleted = f.IsDeleted
            }).ToList();
                
            

            return View(model);
        }

        // GET: BeautyCategory/Create
        // Returns partial view for creating a new category
        public IActionResult Create()
        {
            return PartialView("_Create", new BeautyCategoryViewModel());
        }

        // POST: BeautyCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BeautyCategoryViewModel vm)
        {
            if (!ModelState.IsValid)
                return PartialView("_CreateCategory", vm);

            var entity = new BeautyCategory
            {
                Name = vm.Name,
                IconPath = vm.IconPath,
                ImagePath = vm.ImagePath
            };

            await _categoryService.AddAsync(entity);
            TempData["NotificationMessage"] = "New Beauty Category has been added successfully!";


            return Json(new {success=true});
        }

        // GET: BeautyCategory/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var cat = await _categoryService.GetByIdAsync(id);
            if (cat == null) return NotFound();

            var vm = new BeautyCategoryViewModel
            {
                Id = cat.Id,
                Name = cat.Name ?? "",
                IconPath = cat.IconPath,
                ImagePath = cat.ImagePath,
                IsDeleted = cat.IsDeleted
            };

            return PartialView("_Edit", vm);
        }

        // POST: BeautyCategory/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BeautyCategoryViewModel vm)
        {
            if (!ModelState.IsValid)
                return PartialView("_Edit", vm);

            var existing = await _categoryService.GetByIdAsync(vm.Id);
            if (existing == null) return NotFound();

            existing.Name = vm.Name;
            existing.IconPath = vm.IconPath;
            existing.ImagePath = vm.ImagePath;

            await _categoryService.UpdateAsync(existing);
            TempData["NotificationMessage"] = "Beauty Category Has been updated successfully!";
            return Json(new {success=true});
        }

        // POST: BeautyCategory/Delete/5 (Soft Delete)
        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.SoftDeleteAsync(id);
            TempData["NotificationMessage"] = "Beauty Category Has been deleted successfully!";


            return Json(new {success=true});
        }

       
    }
}