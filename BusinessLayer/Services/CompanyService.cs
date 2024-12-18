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
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyDal _dal;

        public CompanyService(ICompanyDal dal)
        {
            _dal = dal;
        }

        public async Task<Company?> GetByIdAsync(int id) => await _dal.GetByIdAsync(id);
        public async Task<IEnumerable<Company>> GetAllAsync() => await _dal.GetAllAsync();
      
        public async Task AddAsync(Company company)
        {
            if (string.IsNullOrWhiteSpace(company.bigTitle))
                throw new System.ArgumentException("big Title is required.");
            await _dal.AddAsync(company);
           
        }

        public async Task UpdateAsync(Company company)
        {
            await _dal.UpdateAsync(company);
            
        }

        public async Task DeleteAsync(int id)
        {
            await _dal.GetByIdAsync(id);
            await _dal.DeleteAsync(id);
            
            
        }
    }
}
