using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IBeautyItemsService
    {
        Task<BeautyItem?> GetByIdAsync(int id);
        Task<IEnumerable<BeautyItem>> GetAllAsync();
        Task AddAsync(BeautyItem item);
        Task UpdateAsync(BeautyItem item);
        Task<bool> DeleteAsync(int id);
    }
}
