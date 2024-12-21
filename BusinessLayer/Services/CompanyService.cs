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
    public class CompanyService : GenericService<Company>, ICompanyService
    {
        private readonly ICompanyDal _companyRepository;

        public CompanyService(ICompanyDal companyRepository) : base(companyRepository)
        {
            _companyRepository = companyRepository;
        }


    }
}
