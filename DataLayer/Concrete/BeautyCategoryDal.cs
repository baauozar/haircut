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
    public class BeautyCategoryDal : GenericRepository<BeautyCategory>, IBeautyCategoryDal
    {
       
        public BeautyCategoryDal(Context context) : base(context)
        {
            
        }
     






    }
}
