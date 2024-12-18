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
    public class BeautyCardInfoService : IBeautyCardInfoService
    {
        private readonly IBeautyCardInfoDal _dal;
        public BeautyCardInfoService(IBeautyCardInfoDal dal)
        {
            _dal = dal;
        }

        public async Task<BeautyCardInfo?> GetByIdAsync(int id) => await _dal.GetByIdAsync(id);
        public async Task<IEnumerable<BeautyCardInfo>> GetAllAsync() => await _dal.GetAllAsync();


        public async Task AddAsync(BeautyCardInfo info)
        {
            // Validate
            if (string.IsNullOrWhiteSpace(info.Title))
                throw new System.ArgumentException("Title is required.");
            await _dal.AddAsync(info);
          
        }

        public async Task UpdateAsync(BeautyCardInfo info)
        {
            await _dal.UpdateAsync(info);
           
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
