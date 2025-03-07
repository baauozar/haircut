﻿using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Abstract
{
    public interface IBeautyItemsDal : IGenericRepository<BeautyItem>
    {
        Task<IEnumerable<BeautyItem>> GetByCategoryIdAsync(int categoryId);
        Task<IEnumerable<BeautyItem>> GetAllWithCategoryAsync();
    }
}
