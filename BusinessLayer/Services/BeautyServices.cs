using BusinessLayer.Interfaces;
using DataLayer.Abstract;
using DataLayer.Concrete;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class BeautyServices : GenericService<BeautyService>, IBeautyServices
    {
        private readonly IBeautyServicesDal _beautyservicesRepository;

        public BeautyServices(IBeautyServicesDal beautyservicesRepository) : base(beautyservicesRepository)
        {
            _beautyservicesRepository = beautyservicesRepository;
        }
        public async Task<IEnumerable<BeautyService>> GetByCategoryIdAsync(int categoryId)
        {
            return await _beautyservicesRepository.GetByCategoryIdAsync(categoryId);
        }

    }
}
