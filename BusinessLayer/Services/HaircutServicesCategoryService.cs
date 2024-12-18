using BusinessLayer.Interfaces;
using DataLayer.Abstract;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class HaircutServicesCategoryService : IHaircutServicesCategoryService
    {
        private readonly IHaircutServicesCategoryDal _dal;

        public HaircutServicesCategoryService(IHaircutServicesCategoryDal dal)
        {
            _dal = dal;
        }

        public async Task<HaircutServicesCategory?> GetByIdAsync(int id) => await _dal.GetByIdAsync(id);
        public async Task<IEnumerable<HaircutServicesCategory>> GetAllAsync() => await _dal.GetAllAsync();
        public async Task<HaircutServicesCategory?> GetCategoryWithServicesAndSubsAsync(int categoryId)
            => await _dal.GetCategoryWithServicesAndSubsAsync(categoryId);

        public async Task AddAsync(HaircutServicesCategory category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
                throw new System.ArgumentException("Name is required.");
            await _dal.AddAsync(category);
            
        }

        public async Task UpdateAsync(HaircutServicesCategory category)
        {
            await _dal.UpdateAsync(category);
            
        }

        public async Task DeleteAsync(int id)
        {
             await _dal.GetByIdAsync(id);

            await _dal.DeleteAsync(id);
               
            
        }
    }
}
