using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IHairCutSupServicesService : IGenericService<HaircutSupService>
    {
      
        Task<IEnumerable<HaircutSupService>> GetByServiceIdAsync(int serviceId);

        

    }
}
