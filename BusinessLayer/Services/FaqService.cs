using BusinessLayer.Interfaces;
using DataLayer.Abstract;
using DataLayer.Concrete;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
          public class FaqService : GenericService<Faq>, IFaqService
          {
            private readonly IFaqDal _faqRepository;

           public FaqService(IFaqDal faqRepository) : base(faqRepository)
           {
            _faqRepository = faqRepository;
           }


          }

}
    

