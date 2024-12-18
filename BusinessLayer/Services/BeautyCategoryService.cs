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
    public class BeautyCategoryService : IBeautyCategoryService
    {
        private readonly IBeautyCategoryDal _beautyCategoryDal;

        public BeautyCategoryService(IBeautyCategoryDal beautyCategoryDal)
        {
            _beautyCategoryDal = beautyCategoryDal;
        }

        public async Task<IEnumerable<BeautyCategory>> GetAllAsync()
        {
            return await _beautyCategoryDal.GetAllAsync();
        }

        public async Task<BeautyCategory> GetByIdAsync(int id)
        {
            return await _beautyCategoryDal.GetByIdAsync(id);
        }

        public async Task<BeautyCategory> CreateAsync(BeautyCategory category)
        {
            return await _beautyCategoryDal.AddAsync(category);
        }

        public async Task<BeautyCategory> UpdateAsync(BeautyCategory category)
        {
            var existingCategory = await _beautyCategoryDal.GetByIdAsync(category.Id);
            if (existingCategory == null || existingCategory.IsDeleted)
                return null;

            return await _beautyCategoryDal.UpdateAsync(category);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _beautyCategoryDal.DeleteAsync(id);
        }

        public async Task<bool> RestoreAsync(int id)
        {
            return await _beautyCategoryDal.RestoreAsync(id);
        }

        public async Task<IEnumerable<BeautyItem>> GetBeautyItemsByCategoryIdAsync(int categoryId)
        {
            return await _beautyCategoryDal.GetBeautyItemsByCategoryIdAsync(categoryId);
        }

        public async Task<BeautyItem> AddBeautyItemAsync(BeautyItem item)
        {
            return await _beautyCategoryDal.AddBeautyItemAsync(item);
        }
    }
}