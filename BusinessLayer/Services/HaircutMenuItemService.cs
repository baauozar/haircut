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
    public class HaircutMenuItemService : GenericService<HaircutMenuItem>, IHaircutMenuItemService
    {

        private readonly IHaircutMenuItemDal _haircutmenuitemRepository;
        public HaircutMenuItemService(IHaircutMenuItemDal haircutmenuitemRepository) : base(haircutmenuitemRepository)
        {

            _haircutmenuitemRepository = haircutmenuitemRepository;
        }



        public async Task<IEnumerable<HaircutMenuItem>> GetAllWithCategoryAsync()
        {
            return await _haircutmenuitemRepository.GetAllWithCategoryAsync();
        }
      
    }
}
