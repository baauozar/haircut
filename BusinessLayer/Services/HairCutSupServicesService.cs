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
    public class HairCutSupServicesService : GenericService<HaircutSupService>, IHairCutSupServicesService
    {

        private readonly IHairCutSupServicesDal _hairCutSupServices;
        public HairCutSupServicesService(IHairCutSupServicesDal hairCutSupServices) : base(hairCutSupServices)
        {

            _hairCutSupServices = hairCutSupServices;
        }

        public async Task<IEnumerable<HaircutSupService>> GetByServiceIdAsync(int serviceId)
            => await _hairCutSupServices.GetByServiceIdAsync(serviceId);

    }
}
