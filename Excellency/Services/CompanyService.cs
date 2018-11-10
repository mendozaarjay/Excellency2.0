using System.Collections.Generic;
using System.Linq;
using Excellency.Interfaces;
using Excellency.Models;
using Excellency.Persistence;

namespace Excellency.Services
{
    public class CompanyService : ICompany
    {
        private EASDbContext _dbContext;

        public CompanyService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Company company)
        {
            _dbContext.Add(company);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Company> Companies()
        {
            return _dbContext.Companies.Where(x => x.IsDeleted == false);
        }

        public Company GetCompanyById(int id)
        {
            return _dbContext.Companies.FirstOrDefault(x => x.Id == id);
        }

        public string GetCompanyLogoUrlById(int id)
        {
            return _dbContext.Companies.FirstOrDefault(x => x.Id == id).LogoUrl;
        }

        public void RemoveById(int Id)
        {
            var company = _dbContext.Companies.FirstOrDefault(x => x.Id == Id);
            company.IsDeleted = true;
            _dbContext.Entry(company).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Update(Company company)
        {
            _dbContext.Entry(company).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
