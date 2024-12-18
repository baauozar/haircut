using BusinessLayer.Interfaces;
using DataLayer.Abstract;
using DataLayer.Concrete;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class HaircutMenuCategoryService : IHaircutMenuCategoryService
    {
        private readonly IHaircutMenuCategoryDal _haircutMenuCategoryDal;

        public HaircutMenuCategoryService(IHaircutMenuCategoryDal haircutMenuCategoryDal)
        {
            _haircutMenuCategoryDal = haircutMenuCategoryDal;
        }

        public async Task<IEnumerable<HaircutMenuCategory>> GetAllAsync()
        {
            return await _haircutMenuCategoryDal.GetAllAsync();
        }

        public async Task<HaircutMenuCategory> GetByIdAsync(int id)
        {
            return await _haircutMenuCategoryDal.GetByIdAsync(id);
        }

        public async Task<HaircutMenuCategory> CreateAsync(HaircutMenuCategory category)
        {
            return await _haircutMenuCategoryDal.AddAsync(category);
        }

        public async Task<HaircutMenuCategory> UpdateAsync(HaircutMenuCategory category)
        {
            var existingCategory = await _haircutMenuCategoryDal.GetByIdAsync(category.Id);
            if (existingCategory == null || existingCategory.IsDeleted)
                return null;

            return await _haircutMenuCategoryDal.UpdateAsync(category);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _haircutMenuCategoryDal.DeleteAsync(id);
        }

        public async Task<bool> RestoreAsync(int id)
        {
            return await _haircutMenuCategoryDal.RestoreAsync(id);
        }

        public async Task<IEnumerable<HaircutMenuItem>> GetHaircutMenuItemsByCategoryIdAsync(int categoryId)
        {
            return await _haircutMenuCategoryDal.GetHaircutMenuItemsByCategoryIdAsync(categoryId);
        }

        public async Task<HaircutMenuItem> AddHaircutMenuItemAsync(HaircutMenuItem item)
        {
            return await _haircutMenuCategoryDal.AddHaircutMenuItemAsync(item);
        }
    }
}