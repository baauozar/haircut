using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using EntityLayer;
using HaircuteUI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HaircuteUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BeautyItemsController : Controller
    {
        private readonly IBeautyCategoryService _categoryService;
        private readonly IBeautyItemsService _BeautyitemsService;

        public BeautyItemsController(IBeautyCategoryService categoryService, IBeautyItemsService BeautyitemsService)
        {
            _categoryService = categoryService;
            _BeautyitemsService = BeautyitemsService;
        }
        public async Task<IActionResult> Index(int?categoryid)
        {
            var categories = await _categoryService.GetAllAsync();
            IEnumerable<BeautyItem> beautyItems;
            if (categoryid.HasValue)
            {
                beautyItems = await _BeautyitemsService.GetByCategoryIdAsync(categoryid.Value);
            }
            else
            {
                beautyItems = await _BeautyitemsService.GetAllAsync();
            }
            var beautyitems = await _BeautyitemsService.GetAllAsync();

            // Map beauty items to the view model, including the category name
            var model = beautyItems.Select(item => new BeautyItemsViewModel
            {
                Id = item.Id,
                ServiceName = item.ServiceName ?? "",
                Duration = item.Duration,
                Price = item.Price,
                Description = item.Description,
                BeautyCategoryId = item.BeautyCategoryId,
                CategoryName = categories.FirstOrDefault(c => c.Id == item.BeautyCategoryId)?.Name ?? "Deleted Category",
                IsDeleted = item.IsDeleted
            }).ToList();

            return View(model);
        }
    }
}
