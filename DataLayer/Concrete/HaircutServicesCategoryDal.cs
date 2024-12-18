using DataLayer.Abstract;
using EntityLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Concrete
{
    public class HaircutServicesCategoryDal : GenericRepository<HaircutServicesCategory>, IHaircutServicesCategoryDal
    {
        private new readonly Context _context;

        public HaircutServicesCategoryDal(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<HaircutServicesCategory?> GetCategoryWithServicesAndSubsAsync(int categoryId)
        {
            return await _context.HaircutServicesCategories
                .Include(hsc => hsc.HaircutServices)
                .ThenInclude(hs => hs.HairCutSupServices)
                .FirstOrDefaultAsync(hsc => hsc.Id == categoryId);
        }
        public async Task<IEnumerable<HaircutService>> GetHaircutServicesByCategoryIdAsync(int categoryId)
        {
            return await _context.HaircutServices
                                 .Where(hs => hs.ServiceCategoryId == categoryId && !hs.IsDeleted)
                                 .ToListAsync();
        }
        public async Task<HaircutService> AddHaircutServiceAsync(HaircutService service)
        {
            await _context.HaircutServices.AddAsync(service);
            await _context.SaveChangesAsync();
            return service;
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.HaircutServicesCategories
                                         .Include(hsc => hsc.HaircutServices)
                                             .ThenInclude(hs => hs.HairCutSupServices)
                                         .FirstOrDefaultAsync(hsc => hsc.Id == id);
            if (category == null || category.IsDeleted)
                return false;

            category.IsDeleted = true;

            // Cascade soft delete to HaircutServices and HaircutSupServices
            foreach (var service in category.HaircutServices)
            {
                if (!service.IsDeleted)
                {
                    service.IsDeleted = true;

                    foreach (var supService in service.HairCutSupServices)
                    {
                        if (!supService.IsDeleted)
                        {
                            supService.IsDeleted = true;
                            _context.HairCutSupServices.Update(supService);
                        }
                    }

                    _context.HaircutServices.Update(service);
                }
            }

            _context.HaircutServicesCategories.Update(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public override async Task<bool> RestoreAsync(int id)
        {
            var category = await _context.HaircutServicesCategories
                                         .Include(hsc => hsc.HaircutServices)
                                             .ThenInclude(hs => hs.HairCutSupServices)
                                         .FirstOrDefaultAsync(hsc => hsc.Id == id);
            if (category == null || !category.IsDeleted)
                return false;

            category.IsDeleted = false;

            // Cascade restore to HaircutServices and HaircutSupServices
            foreach (var service in category.HaircutServices)
            {
                if (service.IsDeleted)
                {
                    service.IsDeleted = false;

                    foreach (var supService in service.HairCutSupServices)
                    {
                        if (supService.IsDeleted)
                        {
                            supService.IsDeleted = false;
                            _context.HairCutSupServices.Update(supService);
                        }
                    }

                    _context.HaircutServices.Update(service);
                }
            }

            _context.HaircutServicesCategories.Update(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
