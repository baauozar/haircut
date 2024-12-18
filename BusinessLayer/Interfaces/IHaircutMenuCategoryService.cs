using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IHaircutMenuCategoryService
    {
        Task<IEnumerable<HaircutMenuCategory>> GetAllAsync();
        Task<HaircutMenuCategory> GetByIdAsync(int id);
        Task<HaircutMenuCategory> CreateAsync(HaircutMenuCategory category);
        Task<HaircutMenuCategory> UpdateAsync(HaircutMenuCategory category);
        Task<bool> DeleteAsync(int id);
        Task<bool> RestoreAsync(int id);
        Task<IEnumerable<HaircutMenuItem>> GetHaircutMenuItemsByCategoryIdAsync(int categoryId);
        Task<HaircutMenuItem> AddHaircutMenuItemAsync(HaircutMenuItem item);

    }
}
