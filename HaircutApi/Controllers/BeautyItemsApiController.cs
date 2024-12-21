using BusinessLayer.Interfaces;
using EntityLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HaircutApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeautyItemsApiController : ControllerBase
    {
        private readonly IBeautyItemsService _service;

        public BeautyItemsApiController(IBeautyItemsService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BeautyItem>> Get(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return item;
        }

        [HttpGet("all")]
        public async Task<ActionResult> GetAll()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult> Create(BeautyItem item)
        {
            if (string.IsNullOrWhiteSpace(item.ServiceName))
                return BadRequest("ServiceName required.");

            await _service.AddAsync(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, BeautyItem updatedItem)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.ServiceName = updatedItem.ServiceName;
            existing.Price = updatedItem.Price;
           

            await _service.UpdateAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.SoftDeleteAsync(id);
            return NoContent();
        }
    }
}
