using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IHaircutServicesService
    {
        Task<HaircutService?> GetByIdAsync(int id);
        Task<IEnumerable<HaircutService>> GetAllAsync();
        Task<HaircutService?> GetServiceWithSubServicesAsync(int id);
        Task<IEnumerable<HaircutService>> GetServicesByCategoryAsync(int categoryId);
        Task AddAsync(HaircutService service);
        Task UpdateAsync(HaircutService service);
        Task<bool> DeleteAsync(int id);
    }
}
