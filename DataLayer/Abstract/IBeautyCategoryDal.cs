using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Abstract
{
    public interface IBeautyCategoryDal : IGenericRepository<BeautyCategory>
    {
        // Returns a single BeautyCategory with its associated BeautyItems
      

        // Returns a collection of BeautyItems for a specific category
        Task<IEnumerable<BeautyItem>> GetBeautyItemsByCategoryIdAsync(int categoryId);

        // Adds a new BeautyItem to a category
        Task<BeautyItem> AddBeautyItemAsync(BeautyItem item);

    }
}
