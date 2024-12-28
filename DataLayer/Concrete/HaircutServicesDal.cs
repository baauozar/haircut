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
    public class HaircutServicesDal : GenericRepository<HaircutService>, IHaircutServicesDal
    {
        private new readonly Context _context;

        public HaircutServicesDal(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HaircutService>> GetAllCategories()
        {
            return await _context.HaircutServices
                                .Where(h => h.HaircutServicesCategory != null) // Exclude items without a category
                                .Include(h => h.HaircutServicesCategory)
                                .ToListAsync();
        }

        public async Task<IEnumerable<HaircutService>> GetServicesByCategoryAsync(int categoryId)
        {
            return await _context.HaircutServices
                .Where(h => h.ServiceCategoryId == categoryId)
                .ToListAsync();
        }

    }
}
