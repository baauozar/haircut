using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IContactService
    {
        Task<Contact?> GetByIdAsync(int id);
        Task<IEnumerable<Contact>> GetAllAsync();
        Task AddAsync(Contact contact);
        Task UpdateAsync(Contact contact);
        Task<bool> DeleteAsync(int id);
    }
}
