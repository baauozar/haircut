using BusinessLayer.Interfaces;
using DataLayer.Concrete;
using EntityLayer;
using HaircutApi.EntityDto;
using Microsoft.AspNetCore.Http;
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
            var cat = await _categoryService.GetCategoryWithItemsAsync(id);
            if (cat == null) return NotFound();

            var dto = new BeautyCategoryDto
            {
                Id = cat.Id,
                Name = cat.Name ?? "",

                Items = (cat.BeautyItems?? new List<BeautyItem>()).Select(i => new BeautyItemsDto
                {
                    Id = i.Id,
                    ServiceName = i.ServiceName ?? "",
                    Price = i.Price
                    
                }).ToList()
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

            // Add items if any
            if (dto.Items != null)
            {
                foreach (var itm in dto.Items)
                {
                    var newItem = new BeautyItem
                    {
                        ServiceName = itm.ServiceName,
                        Price = itm.Price,

                        BeautyCategoryId = cat.Id
                    };
                    await _itemsService.AddAsync(newItem);
                }
            }

            return CreatedAtAction(nameof(Get), new { id = cat.Id }, cat);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, BeautyCategoryDto dto)
        {
            var existing = await _categoryService.GetCategoryWithItemsAsync(id);
            if (existing == null) return NotFound();

            existing.Name = dto.Name;
           

            await _categoryService.UpdateAsync(existing);

            // Handle items
            var existingItems = (await _itemsService.GetAllAsync()).Where(i => i.BeautyCategoryId == id).ToList();
            var dtoItems = dto.Items ?? new List<BeautyItemsDto>();

            // Delete not in DTO
            var toDelete = existingItems.Where(ei => !dtoItems.Any(di => di.Id == ei.Id)).ToList();
            foreach (var del in toDelete) await _itemsService.SoftDeleteAsync(del.Id);

            // Add or Update
            foreach (var di in dtoItems)
            {
                if (di.Id == 0)
                {
                    var newItem = new BeautyItem
                    {
                        ServiceName = di.ServiceName,
                        Price = di.Price,
                        BeautyCategoryId = id
                    };
                    await _itemsService.AddAsync(newItem);
                }
                else
                {
                    var ei = existingItems.First(x => x.Id == di.Id);
                    ei.ServiceName = di.ServiceName;
                    ei.Price = di.Price;
                    await _itemsService.UpdateAsync(ei);
                }
            }

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
