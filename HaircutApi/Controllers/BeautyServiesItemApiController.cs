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
        private readonly IBeautymultiItemsService _BeautymultiItemsServiceService;

        public BeautyServiesItemApiController(IBeautymultiItemsService faqService)
        {
            _BeautymultiItemsServiceService = faqService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BeautyServiesItem>> Get(int id)
        {
            var faq = await _BeautymultiItemsServiceService.GetByIdAsync(id);
            if (faq == null) return NotFound();
            return faq;
        }

        [HttpGet("all")]
        public async Task<ActionResult> GetAll()
        {
            var faqs = await _BeautymultiItemsServiceService.GetAllAsync();
            return Ok(faqs);
        }

        [HttpPost]
        public async Task<ActionResult> Create(BeautyServiesItem beautyservicesitem)
        {
            if (string.IsNullOrWhiteSpace(beautyservicesitem.Title) || string.IsNullOrWhiteSpace(beautyservicesitem.NumberText) || string.IsNullOrWhiteSpace(beautyservicesitem.ImagePath))
                return BadRequest("Title and NumberText cannot be empty.");

            await _BeautymultiItemsServiceService.AddAsync(beautyservicesitem);
            return CreatedAtAction(nameof(Get), new { id = beautyservicesitem.Id }, beautyservicesitem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, BeautyServiesItem updatedbeautyservicesitem)
        {
            var existing = await _BeautymultiItemsServiceService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Title = updatedbeautyservicesitem.Title;
            existing.NumberText = updatedbeautyservicesitem.NumberText;
            existing.ImagePath = updatedbeautyservicesitem.ImagePath;
            

            await _BeautymultiItemsServiceService.UpdateAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _BeautymultiItemsServiceService.SoftDeleteAsync(id);
            return NoContent();
        }
    }
}

