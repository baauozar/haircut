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
    public class BeautyItemsDal : GenericRepository<BeautyItem>, IBeautyItemsDal
    {
        public BeautyItemsDal(Context context) : base(context)
        {
        }
        public async Task<IEnumerable<BeautyItem>> GetByCategoryIdAsync(int categoryId)
        {
            return await _dbSet
                .Where(item => item.BeautyCategoryId == categoryId && !item.IsDeleted)
                .ToListAsync();
        }
        public async Task<IEnumerable<BeautyItem>> GetAllWithCategoryAsync()
        {
            return await _context.BeautyItems
                            .Where(h => h.BeautyCategory != null) // Exclude items without a category
                            .ToListAsync();
        }

    }
}
