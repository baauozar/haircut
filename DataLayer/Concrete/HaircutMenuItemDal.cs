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
    public class HaircutMenuItemDal : GenericRepository<HaircutMenuItem>, IHaircutMenuItemDal
    {
        public HaircutMenuItemDal(Context context) : base(context)
        {
        }

        public async Task<IEnumerable<HaircutMenuItem>> GetAllWithCategoryAsync()
        {
            return await _context.HaircutMenuItems
                                 .Where(h => h.HaircutMenuCategory != null) // Exclude items without a category
                                 .Include(h => h.HaircutMenuCategory)
                                 .ToListAsync();
        }


      
    }
}
