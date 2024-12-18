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
    public class BeautyServiesItemService : IBeautyServiesItemService
    {
        private readonly IBeautyServiesItemDal _dal;

        public BeautyServiesItemService(IBeautyServiesItemDal dal)
        {
            _dal = dal;
        }

        public async Task<BeautyServiesItem?> GetByIdAsync(int id)
            => await _dal.GetByIdAsync(id);

        public async Task<IEnumerable<BeautyServiesItem>> GetAllAsync()
            => await _dal.GetAllAsync();

     
        public async Task AddAsync(BeautyServiesItem item)
        {
            if (string.IsNullOrWhiteSpace(item.NumberText))
                throw new System.ArgumentException("NumberText is required.");

            await _dal.AddAsync(item);
           
        }

        public async Task UpdateAsync(BeautyServiesItem item)
        {
            await _dal.UpdateAsync(item);
           
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dal.GetByIdAsync(id);
            if (entity == null)
            {
                // Optionally, log the attempt to delete a non-existent entity
                return false;
            }

            return await _dal.DeleteAsync(id);


        }
    }
}
