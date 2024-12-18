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
    public class HairCutSupServicesService : IHairCutSupServicesService
    {
        private readonly IHairCutSupServicesDal _dal;

        public HairCutSupServicesService(IHairCutSupServicesDal dal)
        {
            _dal = dal;
        }

        public async Task<HaircutSupService?> GetByIdAsync(int id) => await _dal.GetByIdAsync(id);
        public async Task<IEnumerable<HaircutSupService>> GetAllAsync() => await _dal.GetAllAsync();
        public async Task<IEnumerable<HaircutSupService>> GetByServiceIdAsync(int serviceId)
            => await _dal.GetByServiceIdAsync(serviceId);

        public async Task AddAsync(HaircutSupService supService)
        {
            if (string.IsNullOrWhiteSpace(supService.Name))
                throw new System.ArgumentException("Name required.");
            await _dal.AddAsync(supService);
      
        }

        public async Task UpdateAsync(HaircutSupService supService)
        {
            await _dal.UpdateAsync(supService);
            
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
