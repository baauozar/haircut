using BusinessLayer.Interfaces;
using EntityLayer;
using HaircutApi.EntityDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HaircutApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HaircutMenuCategoryApiController : ControllerBase
    {
        private readonly IHaircutMenuCategoryService _menuCategoryService;

        public HaircutMenuCategoryApiController(IHaircutMenuCategoryService menuCategoryService)
        {
            _menuCategoryService = menuCategoryService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryWithItems(int id)
        {
            var dto = await _menuCategoryService.GetHaircutMenuItemsByCategoryIdAsync(id);
            if (dto == null) return NotFound();

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(HaircutMenuCategoryDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Map DTO to Entity
            var haircut = new HaircutMenuCategory
            {
                Name = dto.Name,
                HaircutMenuItems = dto.MenuItems?.Select(i => new HaircutMenuItem
                {
                    Id = i.Id,                // Only if Id is provided
                    Name = i.Name ?? "",
                    Price = i.Price           // Map other fields accordingly
                }).ToList() ?? new List<HaircutMenuItem>()
            };

            // Pass the entity to the service
          /*  await _menuCategoryService.AddHaircutMenuItemAsync(haircut);*/

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _menuCategoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}