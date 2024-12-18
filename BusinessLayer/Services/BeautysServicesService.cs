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
    public class BeautysServicesService : IBeautysServicesService
    {
        private readonly IBeautysServicesDal _dal;

        public BeautysServicesService(IBeautysServicesDal dal)
        {
            _dal = dal;
        }

        public async Task<BeautysServices?> GetByIdAsync(int id) => await _dal.GetByIdAsync(id);
        public async Task<IEnumerable<BeautysServices>> GetAllAsync() => await _dal.GetAllAsync();
       
        public async Task AddAsync(BeautysServices service)
        {
            if (string.IsNullOrWhiteSpace(service.Heading))
                throw new System.ArgumentException("Heading is required.");
            await _dal.AddAsync(service);
          
        }

        public async Task UpdateAsync(BeautysServices service)
        {
            await _dal.UpdateAsync(service);
          
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
