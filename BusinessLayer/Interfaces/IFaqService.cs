using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IFaqService
    {
        Task<Faq?> GetFaqByIdAsync(int id);
        Task<IEnumerable<Faq>> GetAllFaqsAsync();
        Task AddFaqAsync(Faq faq);
        Task UpdateFaqAsync(Faq faq);
        Task<bool> DeleteFaqAsync(int id);

    }
}
