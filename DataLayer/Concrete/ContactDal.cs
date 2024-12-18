using DataLayer.Abstract;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Concrete
{
    public class ContactDal : GenericRepository<Contact>, IContactDal
    {
        public ContactDal(Context context) : base(context)
        {
        }

    }
}
