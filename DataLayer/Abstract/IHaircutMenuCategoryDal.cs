﻿using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Abstract
{
    public interface IHaircutMenuCategoryDal : IGenericRepository<HaircutMenuCategory>
    {
        Task<IEnumerable<HaircutMenuItem>> GetHaircutMenuItemsByCategoryIdAsync(int categoryId);


        Task<HaircutMenuItem> AddHaircutMenuItemAsync(HaircutMenuItem item);
    }
}