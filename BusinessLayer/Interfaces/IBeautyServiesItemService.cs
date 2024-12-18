using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IBeautyServiesItemService
    {
        Task<BeautyServiesItem?> GetByIdAsync(int id);
        Task<IEnumerable<BeautyServiesItem>> GetAllAsync();
      
        Task AddAsync(BeautyServiesItem item);
        Task UpdateAsync(BeautyServiesItem item);
        Task<bool> DeleteAsync(int id);
    }
}
