using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IBeautysServicesService
    {
        Task<BeautysServices?> GetByIdAsync(int id);
        Task<IEnumerable<BeautysServices>> GetAllAsync();
        Task AddAsync(BeautysServices service);
        Task UpdateAsync(BeautysServices service);
        Task<bool> DeleteAsync(int id);
    }
}
