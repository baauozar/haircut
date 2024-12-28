using BusinessLayer.Interfaces;
using EntityLayer;
using HaircuteUI.UIViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HaircuteUI.Controllers
{

    public class BeautyController : Controller
    {
        private readonly IBeautyCardInfoService _cardinfoService;
        private readonly IBeautyCategoryService _beautyCategoryService;
        private readonly IBeautyServicesItemService _beautyServicesItemService;
        private readonly IBeautyItemsService _beautyItems;

        public BeautyController(IBeautyCardInfoService cardinfoService, IBeautyCategoryService beautyCategoryService, IBeautyServicesItemService beautyServicesItemService, IBeautyItemsService beautyItems)
        {
            _cardinfoService = cardinfoService;
            _beautyCategoryService = beautyCategoryService;
            _beautyServicesItemService = beautyServicesItemService;
            _beautyItems = beautyItems;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BeautyService()
        {
            return View();
        }
        public async Task<IActionResult> GetBeautyCardPartial()
        {
            // Fetch all stories from the service
            var cardinfo = await _cardinfoService.GetAllAsync();

            // Select the first story (or apply any condition to select a specific story)


            // Map the selected story to the view model
            var model = cardinfo
        .Where(f => !f.IsDeleted)
        .Select(f => new BeautyCardInfoUIViewModel
        {
            Title = f.Title,
            Description = f.Description,
            ImagePath = f.ImagePath,

            // etc.
        })
        .ToList();

            // Return the single story to the partial view
            return PartialView("_BeautyCardInfoPartial", model);
        }
        public async Task<IActionResult> GetBeautyCategoryPartial()
        {
            // Fetch all stories from the service
            var beautycategory = await _beautyCategoryService.GetAllAsync();

            // Select the first story (or apply any condition to select a specific story)


            // Map the selected story to the view model
            var model = beautycategory
        .Where(f => !f.IsDeleted)
        .Select(f => new BeautyCategoryUIViewModel
        {
            Name = f.Name,
            IconPath = f.IconPath,
            ImagePath = f.ImagePath

            // etc.
        })
        .ToList();

            // Return the single story to the partial view
            return PartialView("_BeautyCategoryPartial", model);
        }

        public async Task<IActionResult> GetBeautyCategoryItemsPartial()
        {
            var categories = await _beautyCategoryService.GetAllAsync();
            var items = await _beautyItems.GetAllWithCategoryAsync();

            var viewModel = categories
                .Where(cat => !cat.IsDeleted)
                .Select(cat => new BeautyCategoryUIViewModel
                {
                    Id = cat.Id,
                    Name = cat.Name,
                    IconPath = cat.IconPath,
                    ImagePath = cat.ImagePath,
                    BeautyMenuItems = items
                        .Where(i => i.BeautyCategoryId == cat.Id && !i.IsDeleted)
                        .Select(i => new BeautyItemsUIViewModel
                        {
                            ServiceName = i.ServiceName,
                            Duration = i.Duration,
                            Price = i.Price,
                            Description = i.Description
                        }).ToList()
                }).ToList();

            return PartialView("_BeautyCategoryItemsPartial", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetBeautyItemsByCategory(int categoryId)
        {
            try
            {
                var all = await _beautyItems.GetAllWithCategoryAsync();
                var items = all
                    .Where(i => i.BeautyCategoryId == categoryId && !i.IsDeleted)
                    .Select(i => new BeautyItemsUIViewModel
                    {
                        ServiceName = i.ServiceName,
                        Duration = i.Duration,
                        Price = i.Price,
                        Description = i.Description
                    }).ToList();

                return Json(new { success = true, data = items });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }






    }

}