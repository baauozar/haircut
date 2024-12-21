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
    public class ContactService : GenericService<Contact>, IContactService
    {
        private readonly IContactDal _contactRepository;

        public ContactService(IContactDal contactRepository) : base(contactRepository)
        {
            _contactRepository = contactRepository;
        }


    }
}
