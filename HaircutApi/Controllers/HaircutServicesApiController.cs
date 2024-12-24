using BusinessLayer.Interfaces;
using EntityLayer;
using HaircutApi.EntityDto;
using Microsoft.AspNetCore.Mvc;

namespace HaircutApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HaircutServicesApiController : ControllerBase
    {
        private readonly IHaircutServicesService _servicesService;
      

        public HaircutServicesApiController(IHaircutServicesService servicesService)
        {
            _servicesService = servicesService;
       
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HaircutService>> Get(int id)
        {
            var hs = await _servicesService.GetByIdAsync(id);
            if (hs == null) return NotFound();
            return hs;
        }

        [HttpPost]
        public async Task<ActionResult> Create(HaircutServicesDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return BadRequest("Title is required.");

            var entity = new HaircutService
            {
                Title = dto.Title,
                Description = dto.Description,
                ImagePath = dto.ImagePath,
                ServiceCategoryId = dto.ServiceCategoryId
            };

            await _servicesService.AddAsync(entity);


            return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, HaircutServicesDto dto)
        {
            var existing = await _servicesService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Title = dto.Title;
            existing.Description = dto.Description;
            existing.ImagePath = dto.ImagePath;
            existing.ServiceCategoryId = dto.ServiceCategoryId;

            await _servicesService.UpdateAsync(existing);

         // New
                    

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _servicesService.SoftDeleteAsync(id);
            // If no cascade delete, also remove sub-services:

            return NoContent();
        }
    }
}