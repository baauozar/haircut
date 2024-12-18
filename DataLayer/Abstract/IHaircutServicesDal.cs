using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Abstract
{
    public interface IHaircutServicesDal : IGenericRepository<HaircutService>
    {
        Task<HaircutService?> GetServiceWithSubServicesAsync(int id);
        Task<IEnumerable<HaircutService>> GetServicesByCategoryAsync(int categoryId);

    }
}
