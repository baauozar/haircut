using DataLayer.Abstract;
using EntityLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Concrete
{
    public class BeautyServicesDal : GenericRepository<BeautyService>, IBeautyServicesDal
    {
        public BeautyServicesDal(Context context) : base(context)
        {
        }
        public async Task<IEnumerable<BeautyService>> GetByCategoryIdAsync(int categoryId)
        {
            return await _dbSet
                .Where(item => item.BeautyServicesItemId == categoryId && !item.IsDeleted)
                .ToListAsync();
        }

    }
}
