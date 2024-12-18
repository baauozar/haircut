using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IHairCutTeammemberService
    {
        Task<HairCutTeammember?> GetByIdAsync(int id);
        Task<IEnumerable<HairCutTeammember>> GetAllAsync();
     
    

        Task AddAsync(HairCutTeammember member);
        Task UpdateAsync(HairCutTeammember member);
        Task<bool> DeleteAsync(int id);
    }
}
