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
    public class HaircutMenuItemService : IHaircutMenuItemService
    {
        private readonly IHaircutMenuItemDal _dal;

        public HaircutMenuItemService(IHaircutMenuItemDal dal)
        {
            _dal = dal;
        }

        public async Task<HaircutMenuItem?> GetByIdAsync(int id) => await _dal.GetByIdAsync(id);
        public async Task<IEnumerable<HaircutMenuItem>> GetAllAsync() => await _dal.GetAllAsync();
      
        public async Task AddAsync(HaircutMenuItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
                throw new System.ArgumentException("Name is required.");
            await _dal.AddAsync(item);
       
        }

        public async Task UpdateAsync(HaircutMenuItem item)
        {
            await _dal.UpdateAsync(item);
          
        }
        public async Task<IEnumerable<HaircutMenuItem>> GetAllWithCategoryAsync()
        {
            return await _dal.GetAllWithCategoryAsync();
        }
        public async Task DeleteAsync(int id)
        {
             await _dal.GetByIdAsync(id);

            await _dal.DeleteAsync(id);
               
            
        }
    }
}
