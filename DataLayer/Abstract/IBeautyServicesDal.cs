using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Abstract
{
    public interface IBeautyServicesDal : IGenericRepository<BeautyService>
    {
        Task<IEnumerable<BeautyService>> GetByCategoryIdAsync(int categoryId);
    }
}
