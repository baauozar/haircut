using BusinessLayer.Interfaces;
using EntityLayer;
using HaircutApi.EntityDto;
using Microsoft.AspNetCore.Mvc;

namespace HaircutApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeautyCategoryApiController : ControllerBase
    {
        private readonly IBeautyCategoryService _categoryService;
        private readonly IBeautyItemsService _itemsService;

        public BeautyCategoryApiController(IBeautyCategoryService categoryService, IBeautyItemsService itemsService)
        {
            _categoryService = categoryService;
            _itemsService = itemsService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BeautyCategoryDto>> Get(int id)
        {
            var cat = await _categoryService.GetByIdAsync(id);
            if (cat == null) return NotFound();

            var dto = new BeautyCategoryDto
            {
                Id = cat.Id,
                Name = cat.Name ?? "",
            };

            return dto;
        }

        [HttpPost]
        public async Task<ActionResult> Create(BeautyCategoryDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Name required.");

            var cat = new BeautyCategory
            {
                Name = dto.Name,

            };

            await _categoryService.AddAsync(cat);

           

            return CreatedAtAction(nameof(Get), new { id = cat.Id }, cat);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, BeautyCategoryDto dto)
        {
            var existing = await _categoryService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Name = dto.Name;
           

            await _categoryService.UpdateAsync(existing);


            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _categoryService.SoftDeleteAsync(id);
            var items = (await _itemsService.GetAllAsync()).Where(i => i.BeautyCategoryId == id);
            foreach (var it in items)
                await _itemsService.SoftDeleteAsync(it.Id);

            return NoContent();
        }
    }



}
