using BusinessLayer.Interfaces;
using DataLayer.Abstract;
using DataLayer.Concrete;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class HaircutServicesCategoryService : GenericService<HaircutServicesCategory>, IHaircutServicesCategoryService
    {

        private readonly IHaircutServicesCategoryDal _haircutservicescategoryRepository;
        public HaircutServicesCategoryService(IHaircutServicesCategoryDal haircutservicescategoryRepository) : base(haircutservicescategoryRepository)
        {

            _haircutservicescategoryRepository = haircutservicescategoryRepository;
        }
        public async Task<HaircutServicesCategory?> GetCategoryWithServicesAndSubsAsync(int categoryId)
            => await _haircutservicescategoryRepository.GetCategoryWithServicesAndSubsAsync(categoryId);

      
        

      
    }
}
