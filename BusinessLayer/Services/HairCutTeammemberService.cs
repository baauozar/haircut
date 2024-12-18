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
    public class HairCutTeammemberService : IHairCutTeammemberService
    {
        private readonly IHairCutTeammemberDal _dal;

        public HairCutTeammemberService(IHairCutTeammemberDal dal)
        {
            _dal = dal;
        }

        public async Task<HairCutTeammember?> GetByIdAsync(int id) => await _dal.GetByIdAsync(id);
        public async Task<IEnumerable<HairCutTeammember>> GetAllAsync() => await _dal.GetAllAsync();
      
       

        public async Task AddAsync(HairCutTeammember member)
        {
            if (string.IsNullOrWhiteSpace(member.Name))
                throw new System.ArgumentException("Name is required.");
            await _dal.AddAsync(member);
           
        }

        public async Task UpdateAsync(HairCutTeammember member)
        {
            await _dal.UpdateAsync(member);
           
        }

        public async Task DeleteAsync(int id)
        {
               await _dal.GetByIdAsync(id);
           
                await _dal.DeleteAsync(id);
          
            
        }
    }
}
