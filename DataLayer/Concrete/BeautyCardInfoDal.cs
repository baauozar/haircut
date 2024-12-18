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
    public class BeautyCardInfoDal : GenericRepository<BeautyCardInfo>, IBeautyCardInfoDal
    {
        public BeautyCardInfoDal(Context context) : base(context)
        {
        }
    
    }
}
