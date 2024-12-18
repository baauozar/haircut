using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IHairCutSupServicesService
    {
        Task<HaircutSupService?> GetByIdAsync(int id);
        Task<IEnumerable<HaircutSupService>> GetAllAsync();
        Task<IEnumerable<HaircutSupService>> GetByServiceIdAsync(int serviceId);

        Task AddAsync(HaircutSupService supService);
        Task UpdateAsync(HaircutSupService supService);
        Task<bool> DeleteAsync(int id);
    }
}
