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
    public class BeautyServicesItemSrervice : GenericService<BeautyServicesItem>, IBeautyServicesItemService
    {
        private readonly IBeautyServicesItemDal _BeautyserviceitemRepository;

        public BeautyServicesItemSrervice(IBeautyServicesItemDal BeautyserviceitemRepository) : base(BeautyserviceitemRepository)
        {
            _BeautyserviceitemRepository = BeautyserviceitemRepository;
        }
        
    }
}