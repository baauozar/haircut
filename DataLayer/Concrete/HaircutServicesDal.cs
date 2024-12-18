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

        public async Task<HaircutService?> GetServiceWithSubServicesAsync(int id)
        {
            return await _context.HaircutServices
                .Include(h => h.HairCutSupServices)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<IEnumerable<HaircutService>> GetServicesByCategoryAsync(int categoryId)
        {
            return await _context.HaircutServices
                .Where(h => h.ServiceCategoryId == categoryId)
                .ToListAsync();
        }
    }
}
