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
    public class BeautyItemsService : IBeautyItemsService
    {
        private readonly IBeautyItemsDal _dal;
        public BeautyItemsService(IBeautyItemsDal dal)
        {
            _dal = dal;
        }

        public async Task<BeautyItem?> GetByIdAsync(int id) => await _dal.GetByIdAsync(id);
        public async Task<IEnumerable<BeautyItem>> GetAllAsync() => await _dal.GetAllAsync();
     
        public async Task AddAsync(BeautyItem item)
        {
            if (string.IsNullOrWhiteSpace(item.ServiceName))
                throw new System.ArgumentException("ServiceName required.");
            await _dal.AddAsync(item);
          
        }

        public async Task UpdateAsync(BeautyItem item)
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
