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
    public class BeautyCategoryDal : GenericRepository<BeautyCategory>, IBeautyCategoryDal
    {
        private new readonly Context _context;
        public BeautyCategoryDal(Context context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BeautyItem>> GetBeautyItemsByCategoryIdAsync(int categoryId)
        {
            return await _context.BeautyItems
                                 .Where(bi => bi.BeautyCategoryId == categoryId && !bi.IsDeleted)
                                 .ToListAsync();
        }

        public async Task<BeautyItem> AddBeautyItemAsync(BeautyItem item)
        {
            await _context.BeautyItems.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public override async Task<BeautyCategory> UpdateAsync(BeautyCategory entity)
        {
            _context.BeautyCategories.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

      
    }
}
