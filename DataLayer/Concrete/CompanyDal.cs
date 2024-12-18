using DataLayer.Abstract;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Concrete
{
    public class CompanyDal : GenericRepository<Company>, ICompanyDal
    {
        public CompanyDal(Context context) : base(context)
        {
        }

    }
}
