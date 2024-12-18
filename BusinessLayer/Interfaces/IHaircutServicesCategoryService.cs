using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IHaircutServicesCategoryService
    {
        Task<HaircutServicesCategory?> GetByIdAsync(int id);
        Task<IEnumerable<HaircutServicesCategory>> GetAllAsync();
        Task<HaircutServicesCategory?> GetCategoryWithServicesAndSubsAsync(int categoryId);

        Task AddAsync(HaircutServicesCategory category);
        Task UpdateAsync(HaircutServicesCategory category);
        Task<bool> DeleteAsync(int id);
    }
}
