using BusinessLayer.Interfaces;
using EntityLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HaircutApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyApiController : ControllerBase
    {
        private readonly ICompanyService _CompanyService;

        public CompanyApiController(ICompanyService CompanyService)
        {
            _CompanyService = CompanyService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> Get(int id)
        {
            var company = await _CompanyService.GetByIdAsync(id);
            if (company == null) return NotFound();
            return company;
        }

        [HttpGet("all")]
        public async Task<ActionResult> GetAll()
        {
            var Compantlist = await _CompanyService.GetAllAsync();
            return Ok(Compantlist);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Company company)
        {
            if (string.IsNullOrWhiteSpace(company.smallTitle) || string.IsNullOrWhiteSpace(company.bigTitle) || string.IsNullOrWhiteSpace(company.BaackgroundTitle) || string.IsNullOrWhiteSpace(company.Section) || string.IsNullOrWhiteSpace(company.Signature))
                return BadRequest("all entities cannot be empty.");

            await _CompanyService.AddAsync(company);
            return CreatedAtAction(nameof(Get), new { id = company.Id }, company);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Company updatedCompany)
        {
            var existing = await _CompanyService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.smallTitle = updatedCompany.smallTitle;
            existing.bigTitle = updatedCompany.bigTitle;
            existing.BaackgroundTitle = updatedCompany.BaackgroundTitle;
            existing.Section = updatedCompany.Section;
            existing.Signature = updatedCompany.Signature;

            await _CompanyService.UpdateAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _CompanyService.DeleteAsync(id);
            return NoContent();
        }
    }
}
