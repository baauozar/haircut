using BusinessLayer.Interfaces;
using EntityLayer;
using HaircuteUI.Models;
using HaircuteUI.UIViewModel;
using HaircuteUI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HaircuteUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHaircutMenuCategoryService _categoryService; // Service for categories
        private readonly IHaircutMenuItemService _menuItemService; // Service for menu items
        private readonly ICompanyService _storyService; // Service for menu items
        private readonly IHaircutServicesService _hairservice; // Service for menu items
        private readonly IHaircutMenuCategoryService _haircategory; // Service for menu items
        private readonly IFaqService _faqservice; // Service for menu items


        public HomeController(IHaircutMenuCategoryService categoryService, IHaircutMenuItemService menuItemService, ICompanyService storyService, IHaircutServicesService hairservice, IHaircutMenuCategoryService haircategory, IFaqService faqservice)
        {
            _categoryService = categoryService;
            _menuItemService = menuItemService;
            _storyService = storyService;
            _hairservice = hairservice;
            _haircategory = haircategory;
            _faqservice = faqservice;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync(); // Fetch all categories

            var viewModel = categories
                .Where(c => !c.IsDeleted)
                .Select(category => new HaircutMenuUIViewModel
                {
                    CategoryName = category.Name,
                    MenuItems = category.HaircutMenuItems
                        .Where(item => !item.IsDeleted)
                        .Select(item => new MenuItemUIViewModel
                        {
                            Name = item.Name,
                            Price = item.Price,
                            Time = item.Time

                        }).ToList()
                }).ToList(); // Convert to List

            return View(viewModel); // Pass collection to the view
        }

        public async Task<IActionResult> GetServiceMenuPartial()
        {
            // Fetch all categories
            var categories = await _categoryService.GetAllAsync();

            // Fetch all menu items with their categories
            var menuItems = await _menuItemService.GetAllWithCategoryAsync();

            // Prepare the grouped view model
            var viewModel = categories
                .Where(category => !category.IsDeleted) // Filter out deleted categories
                .Select(category => new HaircutMenuUIViewModel
                {
                    CategoryName = category.Name,
                    MenuItems = menuItems
                        .Where(item => item.HaircutMenuCategoryId == category.Id && !item.IsDeleted) // Match category and filter deleted items
                        .Select(item => new MenuItemUIViewModel
                        {
                            Name = item.Name,
                            Price = item.Price,
                            Time = item.Time,
                        }).ToList() // Ensure it is a List
                }).ToList();

            // Return the partial view
            return PartialView("_ServiceMenuPartial", viewModel);
        }

        public async Task<IActionResult> GetStoryPartial()
        {
            // Fetch all stories from the service
            var stories = await _storyService.GetAllAsync();

            // Select the first story (or apply any condition to select a specific story)
            var story = stories.FirstOrDefault();

            // If no story exists, return a default empty story
            if (story == null)
            {
                return PartialView("_StoryPartial", new StoryUIViewModel
                {
                    smallTitle = "No story available",
                    bigTitle = "",
                    BaackgroundTitle = "",
                    Section = "",
                    Signature = ""
                });
            }

            // Map the selected story to the view model
            var model = new StoryUIViewModel
            {
                smallTitle = story.smallTitle,
                bigTitle = story.bigTitle,
                BaackgroundTitle = story.BaackgroundTitle,
                Section = story.Section,
                Signature = story.Signature
            };

            // Return the single story to the partial view
            return PartialView("_StoryPartial", model);
        }

        public async Task<IActionResult> GetFaqPartial()
        {
            // Fetch all stories from the service
            var faqs = await _faqservice.GetAllAsync();

            // Select the first story (or apply any condition to select a specific story)


            // Map the selected story to the view model
            var model = faqs
        .Where(f => !f.IsDeleted)
        .Select(f => new FaqUIViewModel
        {
            quastion = f.quastion,
            Answer = f.Answer
            // etc.
        })
        .ToList();

            // Return the single story to the partial view
            return PartialView("_FaqPartial", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoriesPartial()
        {
            var categories = await _haircategory.GetAllAsync();  // or however you fetch
            var viewModel = categories
                .Where(cat => !cat.IsDeleted)
                .Select(cat => new HaircutServicesCategoryUIViewModel
                {
                    Id = cat.Id,
                    Name = cat.Name
                })
                .ToList();

            return PartialView("_CategoriesPartial", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ServicesPartial(int categoryId)
        {
            var services = await _hairservice.GetAllCategories(); // or your method
            var filtered = services
                .Where(s => !s.IsDeleted && s.ServiceCategoryId == categoryId)
                .Select(s => new HaircutServicesUIViewModel
                {
                    Title = s.Title,
                    Description = s.Description,
                    ImagePath = s.ImagePath,
                    ServiceCategoryId = s.ServiceCategoryId
                })
                .ToList();

            return PartialView("_ServicesPartial", filtered);
        }



        public IActionResult Story()
        {
       // Convert to List

            return View(); // Pass collection to the view
        }

    }

}