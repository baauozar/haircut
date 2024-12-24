using BusinessLayer.Interfaces;
using EntityLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HaircutApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeautyServiesItemApiController : ControllerBase
    {
        private readonly IBeautyServiesItemService _beautyServiesItemService;

        public BeautyServiesItemApiController(IBeautyServiesItemService beautyServiesItemService)
        {
            _beautyServiesItemService = beautyServiesItemService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BeautyServiesItem>> Get(int id)
        {
            var faq = await _beautyServiesItemService.GetByIdAsync(id);
            if (faq == null) return NotFound();
            return faq;
        }

        [HttpGet("all")]
        public async Task<ActionResult> GetAll()
        {
            var faqs = await _beautyServiesItemService.GetAllAsync();
            return Ok(faqs);
        }

        [HttpPost]
        public async Task<ActionResult> Create(BeautyServiesItem beautyservicesitem)
        {
            if (string.IsNullOrWhiteSpace(beautyservicesitem.Title) || string.IsNullOrWhiteSpace(beautyservicesitem.NumberText) || string.IsNullOrWhiteSpace(beautyservicesitem.ImagePath))
                return BadRequest("Title and NumberText cannot be empty.");

            await _beautyServiesItemService.AddAsync(beautyservicesitem);
            return CreatedAtAction(nameof(Get), new { id = beautyservicesitem.Id }, beautyservicesitem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, BeautyServiesItem updatedbeautyservicesitem)
        {
            var existing = await _beautyServiesItemService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Title = updatedbeautyservicesitem.Title;
            existing.NumberText = updatedbeautyservicesitem.NumberText;
            existing.ImagePath = updatedbeautyservicesitem.ImagePath;
            

            await _beautyServiesItemService.UpdateAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _beautyServiesItemService.SoftDeleteAsync(id);
            return NoContent();
        }
    }
}

