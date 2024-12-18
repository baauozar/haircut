using BusinessLayer.Interfaces;
using DataLayer.Abstract;
using EntityLayer;
using HaircutApi.EntityDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HaircutApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HaircutServicesCategoryApiController : ControllerBase
    {
        private readonly IHaircutServicesCategoryService _categoryRepo;
        private readonly IHaircutServicesService _serviceRepo;
        

        public HaircutServicesCategoryApiController(IHaircutServicesCategoryService categoryRepo, IHaircutServicesService serviceRepo)
        {
            _categoryRepo = categoryRepo;
            _serviceRepo = serviceRepo;


        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryWithServices(int id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            var services = await _serviceRepo.GetServicesByCategoryAsync(id);

            var dto = new HaircutServicesCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Services = services.Select(s => new HaircutServicesDto
                {
                    Id = s.Id,
                    Title = s.Title,
                    Description = s.Description
                }).ToList()
            };

            return Ok(dto);
        }
    }

}
