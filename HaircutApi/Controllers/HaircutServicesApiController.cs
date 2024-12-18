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
    public class HaircutServicesApiController : ControllerBase
    {
        private readonly IHaircutServicesService _servicesService;
        private readonly IHairCutSupServicesService _subServicesService;

        public HaircutServicesApiController(IHaircutServicesService servicesService, IHairCutSupServicesService subServicesService)
        {
            _servicesService = servicesService;
            _subServicesService = subServicesService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HaircutService>> Get(int id)
        {
            var hs = await _servicesService.GetServiceWithSubServicesAsync(id);
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

            // Add sub-services from DTO
            if (dto.SubServices != null)
            {
                foreach (var sub in dto.SubServices)
                {
                    var s = new HaircutSupService
                    {
                        Name = sub.Name,
                        Description = sub.Description,
                        ServiceId = entity.Id
                    };
                    await _subServicesService.AddAsync(s);
                }
            }

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

            // Handle sub-services
            var existingSubs = await _subServicesService.GetByServiceIdAsync(id);
            var dtoSubs = dto.SubServices ?? new List<HairCutSupServicesDto>();

            // Delete those not in DTO
            var toDelete = existingSubs.Where(es => !dtoSubs.Any(ds => ds.Id == es.Id)).ToList();
            foreach (var del in toDelete)
                await _subServicesService.DeleteAsync(del.Id);

            // Add or update
            foreach (var ds in dtoSubs)
            {
                if (ds.Id == 0)
                {
                    // New
                    var newSub = new HaircutSupService
                    {
                        Name = ds.Name,
                        Description = ds.Description,
                        ServiceId = id
                    };
                    await _subServicesService.AddAsync(newSub);
                }
                else
                {
                    // Update existing
                    var es = existingSubs.First(x => x.Id == ds.Id);
                    es.Name = ds.Name;
                    es.Description = ds.Description;
                    await _subServicesService.UpdateAsync(es);
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _servicesService.DeleteAsync(id);
            // If no cascade delete, also remove sub-services:
            var subs = await _subServicesService.GetByServiceIdAsync(id);
            foreach (var s in subs) await _subServicesService.DeleteAsync(s.Id);

            return NoContent();
        }
    }
}