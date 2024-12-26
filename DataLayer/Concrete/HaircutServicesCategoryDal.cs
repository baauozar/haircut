using DataLayer.Abstract;
using EntityLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Concrete
{
    public class HaircutServicesCategoryDal : GenericRepository<HaircutServicesCategory>, IHaircutServicesCategoryDal
    {
        private new readonly Context _context;

        public HaircutServicesCategoryDal(Context context) : base(context)
        {
            _context = context;
        }

      
   
   
    }
}
