using BusinessLayer.Interfaces;
using EntityLayer;
using HaircutApi.EntityDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HaircutApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HairCutSupServicesController : ControllerBase
    {
        private readonly IHairCutSupServicesService _service;

        public HairCutSupServicesController(IHairCutSupServicesService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var services = await _service.GetAllAsync();
            var result = services.Select(s => new HairCutSupServicesDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                ServiceId = s.ServiceId
            });
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var service = await _service.GetByIdAsync(id);
            if (service == null) return NotFound();

            var dto = new HairCutSupServicesDto
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                ServiceId = service.ServiceId
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HairCutSupServicesDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var service = new HaircutSupService
            {
                Name = dto.Name,
                Description = dto.Description,
                ServiceId = dto.ServiceId
            };
            await _service.AddAsync(service);

            return CreatedAtAction(nameof(GetById), new { id = service.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] HairCutSupServicesDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Name = dto.Name;
            existing.Description = dto.Description;
            existing.ServiceId = dto.ServiceId;

            await _service.UpdateAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}