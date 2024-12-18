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
    
        public class FaqService : IFaqService
        {
            private readonly IFaqDal _faqDal;

            public FaqService(IFaqDal faqDal)
            {
                _faqDal = faqDal;
            }

            public async Task<Faq?> GetFaqByIdAsync(int id)
            {
                return await _faqDal.GetByIdAsync(id);
            }

            public async Task<IEnumerable<Faq>> GetAllFaqsAsync()
            {
                return await _faqDal.GetAllAsync();
            }

            public async Task AddFaqAsync(Faq faq)
            {
                // Business logic: Validate question and answer
                if (string.IsNullOrWhiteSpace(faq.quastion) || string.IsNullOrWhiteSpace(faq.Answer))
                    throw new System.ArgumentException("Question and Answer cannot be empty.");

                await _faqDal.AddAsync(faq);
               
            }

            public async Task UpdateFaqAsync(Faq faq)
            {
                await _faqDal.UpdateAsync(faq);
               
            }

            public async Task DeleteFaqAsync(int id)
            {
                   await _faqDal.GetByIdAsync(id);
                   await _faqDal.DeleteAsync(id);
                   
                
            }

       
        }
    
}
