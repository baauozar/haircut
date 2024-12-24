using BusinessLayer.Interfaces;
using EntityLayer;
using HaircutApi.EntityDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HaircutApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeautyItemsApiController : ControllerBase
    {
        private readonly IBeautyItemsService _beautyItemsService;
        private readonly IBeautyCategoryService _beautyCategory;

        public BeautyItemsApiController(IBeautyItemsService beautyItemsService, IBeautyCategoryService beautyCategory)
        {
            _beautyItemsService = beautyItemsService;
            _beautyCategory = beautyCategory;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BeautyItem>> Get(int id)
        {
            var item = await _beautyItemsService.GetByIdAsync(id);
            if (item == null) return NotFound();
            return item;
        }

        [HttpGet("all")]
        public async Task<ActionResult> GetAll()
        {
            var items = await _beautyItemsService.GetAllAsync();
            return Ok(items);
        }
        public async Task<IActionResult> GetCategoryWithServices(int id)
        {
            var beautyItems = await _beautyItemsService.GetByIdAsync(id);
            var beautyitembycategory=await _beautyItemsService.GetByCategoryIdAsync(id);

            var dto = new BeautyItemsDto
            {
                Id = beautyItems.Id,
                ServiceName = beautyItems.ServiceName,
                Duration = beautyItems.Duration,
                Price = beautyItems.Price,

                beautyCategories = beautyitembycategory.Select(s => new BeautyCategoryDto
                {
                    Id = s.Id,
                    Name = s.BeautyCategory!.Name
                  
                }).ToList()
            };

            return Ok(dto);
        }
        [HttpPost]
        public async Task<ActionResult> Create(BeautyItem item)
        {
            if (string.IsNullOrWhiteSpace(item.ServiceName))
                return BadRequest("ServiceName required.");

            await _beautyItemsService.AddAsync(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, BeautyItem updatedItem)
        {
            var existing = await _beautyItemsService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.ServiceName = updatedItem.ServiceName;
            existing.Price = updatedItem.Price;
           

            await _beautyItemsService.UpdateAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _beautyItemsService.SoftDeleteAsync(id);
            return NoContent();
        }
    }
}
