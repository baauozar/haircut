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
    public class BeautyItemsService : GenericService<BeautyItem>, IBeautyItemsService
    {
        private readonly IBeautyItemsDal _beautyitemRepository;
        private readonly IBeautyCategoryDal _categoryService; // Inject category service

        public BeautyItemsService(IBeautyItemsDal beautyitemRepository, IBeautyCategoryDal categoryService) : base(beautyitemRepository)
        {
            _beautyitemRepository = beautyitemRepository;
            _categoryService = categoryService;
        }
        public async Task<IEnumerable<BeautyItem>> GetByCategoryIdAsync(int categoryId)
        {
            return await _beautyitemRepository.GetByCategoryIdAsync(categoryId);
        }
    }
}
