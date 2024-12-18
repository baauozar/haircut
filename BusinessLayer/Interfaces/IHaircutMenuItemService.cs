using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IHaircutMenuItemService
    {
        Task<HaircutMenuItem?> GetByIdAsync(int id);
        Task<IEnumerable<HaircutMenuItem>> GetAllAsync();
        Task<IEnumerable<HaircutMenuItem>> GetAllWithCategoryAsync();

        Task AddAsync(HaircutMenuItem item);
        Task UpdateAsync(HaircutMenuItem item);
        Task<bool> DeleteAsync(int id);
    }
}
