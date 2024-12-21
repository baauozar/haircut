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
    public class BeautyCardInfoService : GenericService<BeautyCardInfo>, IBeautyCardInfoService
    {
        private readonly IBeautyCardInfoDal _beautyCardInfoRepository;

        public BeautyCardInfoService(IBeautyCardInfoDal beautyCardInfoRepository) : base(beautyCardInfoRepository)
        {
            _beautyCardInfoRepository = beautyCardInfoRepository;
        }

       
    }
}
