using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ICompanyService
    {
        Task<Company?> GetByIdAsync(int id);
        Task<IEnumerable<Company>> GetAllAsync();
       

        Task AddAsync(Company company);
        Task UpdateAsync(Company company);
        Task<bool> DeleteAsync(int id);
    }
}
