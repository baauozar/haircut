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
    public class HaircutMenuCategoryDal : GenericRepository<HaircutMenuCategory>, IHaircutMenuCategoryDal
    {
        private new readonly Context _context;
        public HaircutMenuCategoryDal(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HaircutMenuItem>> GetHaircutMenuItemsByCategoryIdAsync(int categoryId)
        {
            return await _context.HaircutMenuItems
                                 .Where(hmi => hmi.HaircutMenuCategoryId == categoryId && !hmi.IsDeleted)
                                 .ToListAsync();
        }

        public async Task<HaircutMenuItem> AddHaircutMenuItemAsync(HaircutMenuItem item)
        {
            await _context.HaircutMenuItems.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public override async Task<HaircutMenuCategory> UpdateAsync(HaircutMenuCategory entity)
        {
            _context.HaircutMenuCategories.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
