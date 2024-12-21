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
    public class HaircutMenuCategoryService:GenericService<HaircutMenuCategory>, IHaircutMenuCategoryService
    {
        private readonly IHaircutMenuCategoryDal _haircutmenucategoryRepository;

        public HaircutMenuCategoryService(IHaircutMenuCategoryDal haircutmenucategoryRepository) : base(haircutmenucategoryRepository)
        {
            _haircutmenucategoryRepository = haircutmenucategoryRepository;
        }


    }
}