using BusinessLayer.Interfaces;
using EntityLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HaircutApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaqApiController : ControllerBase
    {
        private readonly IFaqService _faqService;

        public FaqApiController(IFaqService faqService)
        {
            _faqService = faqService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Faq>> Get(int id)
        {
            var faq = await _faqService.GetByIdAsync(id);
            if (faq == null) return NotFound();
            return faq;
        }

        [HttpGet("all")]
        public async Task<ActionResult> GetAll()
        {
            var faqs = await _faqService.GetAllAsync();
            return Ok(faqs);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Faq faq)
        {
            if (string.IsNullOrWhiteSpace(faq.quastion) || string.IsNullOrWhiteSpace(faq.Answer))
                return BadRequest("Question and Answer cannot be empty.");

            await _faqService.AddAsync(faq);
            return CreatedAtAction(nameof(Get), new { id = faq.Id }, faq);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Faq updatedFaq)
        {
            var existing = await _faqService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.quastion = updatedFaq.quastion;
            existing.Answer = updatedFaq.Answer;

            await _faqService.UpdateAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _faqService.SoftDeleteAsync(id);
            return NoContent();
        }
    }
}
