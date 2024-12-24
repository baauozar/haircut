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
    public class HaircutMenuCategoryDal : GenericRepository<HaircutMenuCategory>, IHaircutMenuCategoryDal
    {
        private new readonly Context _context;
        public HaircutMenuCategoryDal(Context context) : base(context)
        {
            _context = context;
        }

     
    }
}
