using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IBeautyCardInfoService
    {
        Task<BeautyCardInfo?> GetByIdAsync(int id);
        Task<IEnumerable<BeautyCardInfo>> GetAllAsync();
      
        Task AddAsync(BeautyCardInfo info);
        Task UpdateAsync(BeautyCardInfo info);
        Task<bool> DeleteAsync(int id);
    }
}
