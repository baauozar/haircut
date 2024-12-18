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
    public class HairCutTeammemberDal : GenericRepository<HairCutTeammember>, IHairCutTeammemberDal
    {

        public HairCutTeammemberDal(Context context) : base(context)
    {
    }

   
}
}
