using DataLayer.Abstract;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Concrete
{
    public class FaqDal : GenericRepository<Faq>, IFaqDal
    {
        public FaqDal(Context context) : base(context)
        {
        }

    }
}
