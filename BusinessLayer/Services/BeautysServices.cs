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
    public class BeautysServices : GenericService<EntityLayer.BeautysServices>, IBeautysServices
    {
        private readonly IBeautysServicesDal _beautyservicesRepository;

        public BeautysServices(IBeautysServicesDal beautyservicesRepository) : base(beautyservicesRepository)
        {
            _beautyservicesRepository = beautyservicesRepository;
        }


    }
}
