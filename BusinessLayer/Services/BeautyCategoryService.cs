using BusinessLayer.Interfaces;
using DataLayer.Abstract;
using DataLayer.Concrete;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class BeautyCategoryService : GenericService<BeautyCategory>,IBeautyCategoryService
    {
       
        private readonly IBeautyCategoryDal _beautycategoryRepository;
        public BeautyCategoryService(IBeautyCategoryDal categoryRepository) : base(categoryRepository)
        {

            _beautycategoryRepository = categoryRepository;
        }

        public async Task<BeautyCategory?> GetCategoryWithItemsAsync(int id)
        {
            return await _beautycategoryRepository.GetCategoryWithItemsAsync(id);
        }

       
    }
}