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
    public class BeautyServiesItemSrervice : GenericService<BeautyServiesItem>, IBeautyServiesItemService
    {
        private readonly IBeautyServiesItemDal _BeautyserviceitemRepository;

        public BeautyServiesItemSrervice(IBeautyServiesItemDal BeautyserviceitemRepository) : base(BeautyserviceitemRepository)
        {
            _BeautyserviceitemRepository = BeautyserviceitemRepository;
        }
        
    }
}