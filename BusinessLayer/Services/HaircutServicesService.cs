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
    public class HaircutServicesService : IHaircutServicesService
    {
        private readonly IHaircutServicesDal _dal;
        public HaircutServicesService(IHaircutServicesDal dal)
        {
            _dal = dal;
        }

        public async Task<HaircutService?> GetByIdAsync(int id) => await _dal.GetByIdAsync(id);
        public async Task<IEnumerable<HaircutService>> GetAllAsync() => await _dal.GetAllAsync();
        public async Task<HaircutService?> GetServiceWithSubServicesAsync(int id)
            => await _dal.GetServiceWithSubServicesAsync(id);
        public async Task<IEnumerable<HaircutService>> GetServicesByCategoryAsync(int categoryId)
            => await _dal.GetServicesByCategoryAsync(categoryId);

        public async Task AddAsync(HaircutService service)
        {
            if (string.IsNullOrWhiteSpace(service.Title))
                throw new System.ArgumentException("Title is required.");
            await _dal.AddAsync(service);
           
        }

        public async Task UpdateAsync(HaircutService service)
        {
            await _dal.UpdateAsync(service);
            
        }

        public async Task DeleteAsync(int id)
        {
            await _dal.GetByIdAsync(id);

            await _dal.DeleteAsync(id);
                
            
        }
    }
}
