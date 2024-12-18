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
    public class HairCutSupServicesDal : GenericRepository<HaircutSupService>, IHairCutSupServicesDal
    {
        private new readonly Context _context;

        public HairCutSupServicesDal(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HaircutSupService>> GetByServiceIdAsync(int serviceId)
        {
            return await _context.HairCutSupServices
                .Where(s => s.ServiceId == serviceId)
                .ToListAsync();
        }
    }
}
