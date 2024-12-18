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
    public class ContactService : IContactService
    {
        private readonly IContactDal _dal;

        public ContactService(IContactDal dal)
        {
            _dal = dal;
        }

        public async Task<Contact?> GetByIdAsync(int id) => await _dal.GetByIdAsync(id);
        public async Task<IEnumerable<Contact>> GetAllAsync() => await _dal.GetAllAsync();
      
        public async Task AddAsync(Contact contact)
        {
            if (string.IsNullOrWhiteSpace(contact.Name))
                throw new System.ArgumentException("Name is required.");
            await _dal.AddAsync(contact);
      
        }

        public async Task UpdateAsync(Contact contact)
        {
            await _dal.UpdateAsync(contact);
            
        }

        public async Task DeleteAsync(int id)
        {
             await _dal.DeleteAsync(id);
          

        }
    }
}
