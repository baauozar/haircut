using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Abstract
{
    public interface IHaircutServicesCategoryDal : IGenericRepository<HaircutServicesCategory>
    {
        Task<HaircutServicesCategory?> GetCategoryWithServicesAndSubsAsync(int categoryId);
        Task<IEnumerable<HaircutService>> GetHaircutServicesByCategoryIdAsync(int categoryId);
        Task<HaircutService> AddHaircutServiceAsync(HaircutService service);
    }
}
