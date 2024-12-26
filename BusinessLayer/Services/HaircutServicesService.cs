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
    public class HaircutServicesService : GenericService<HaircutService>, IHaircutServicesService
    {

        private readonly IHaircutServicesDal _haircutServices;
        public HaircutServicesService(IHaircutServicesDal haircutServices) : base(haircutServices)
        {

            _haircutServices = haircutServices;
        }
   
        public async Task<IEnumerable<HaircutService>> GetServicesByCategoryAsync(int categoryId)
            => await _haircutServices.GetServicesByCategoryAsync(categoryId);

        
    }
}
