using BusinessLayer.Interfaces;
using EntityLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HaircutApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeautyCardInfoApiController : ControllerBase
    {
        private readonly IBeautyCardInfoService _BeautyCardInfoService;

        public BeautyCardInfoApiController(IBeautyCardInfoService BeautyCardInfoService)
        {
            _BeautyCardInfoService = BeautyCardInfoService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BeautyCardInfo>> Get(int id)
        {
            var beauty = await _BeautyCardInfoService.GetByIdAsync(id);
            if (beauty == null) return NotFound();
            return beauty;
        }

        [HttpGet("all")]
        public async Task<ActionResult> GetAll()
        {
            var Compantlist = await _BeautyCardInfoService.GetAllAsync();
            return Ok(Compantlist);
        }

        [HttpPost]
        public async Task<ActionResult> Create(BeautyCardInfo updatedBeautyCardInfo)
        {
            if (string.IsNullOrWhiteSpace(updatedBeautyCardInfo.Title) || string.IsNullOrWhiteSpace(updatedBeautyCardInfo.Description) || string.IsNullOrWhiteSpace(updatedBeautyCardInfo.ImagePath))
                return BadRequest("all entities cannot be empty.");

            await _BeautyCardInfoService.AddAsync(updatedBeautyCardInfo);
            return CreatedAtAction(nameof(Get), new { id = updatedBeautyCardInfo.Id }, updatedBeautyCardInfo);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, BeautyCardInfo updatedBeautyCardInfo)
        {
            var existing = await _BeautyCardInfoService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Title = updatedBeautyCardInfo.Title;
            existing.Description = updatedBeautyCardInfo.Description;
            existing.ImagePath = updatedBeautyCardInfo.ImagePath;
           
            await _BeautyCardInfoService.UpdateAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool result = await _BeautyCardInfoService.SoftDeleteAsync(id);
            if (result)
                return NoContent(); // 204 No Content
            else
                return NotFound();    // 404 Not Found
        }
    }
}
