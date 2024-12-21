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
    public class BeautymultiItems : GenericService<BeautyServiesItem>, IBeautymultiItemsService
    {
        private readonly IBeautymultiItemsDal _BeautyserviceitemRepository;

        public BeautymultiItems(IBeautymultiItemsDal BeautyserviceitemRepository) : base(BeautyserviceitemRepository)
        {
            _BeautyserviceitemRepository = BeautyserviceitemRepository;
        }
    }
}