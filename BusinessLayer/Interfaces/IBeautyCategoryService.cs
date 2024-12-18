using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IBeautyCategoryService
    {
        Task<IEnumerable<BeautyCategory>> GetAllAsync();
        Task<BeautyCategory> GetByIdAsync(int id);
        Task<BeautyCategory> CreateAsync(BeautyCategory category);
        Task<BeautyCategory> UpdateAsync(BeautyCategory category);
        Task<bool> DeleteAsync(int id);
        Task<bool> RestoreAsync(int id);
        Task<IEnumerable<BeautyItem>> GetBeautyItemsByCategoryIdAsync(int categoryId);
        Task<BeautyItem> AddBeautyItemAsync(BeautyItem item);
    }
}
