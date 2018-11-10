using Excellency.Interfaces;
using Excellency.Models;
using Excellency.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Excellency.Services
{
    public class BranchService : IBranch
    {
        private EASDbContext _dbContext;

        public BranchService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Branch branch)
        {
            _dbContext.Add(branch);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Branch> Branches()
        {
            return _dbContext.Branches.Where(x => x.IsDeleted == false)
                .Include(a => a.Company);
        }

        public IEnumerable<Company> Companies()
        {
            return _dbContext.Companies.Where(x => x.IsDeleted == false);
        }

        public Branch GetBranchById(int id)
        {
            return _dbContext.Branches.FirstOrDefault(x => x.Id == id);
        }

        public Company GetCompanyById(int id)
        {
            return _dbContext.Companies.FirstOrDefault(a => a.Id == id);
        }

        public Company GetCompanyPerBranch(int id)
        {
            var comp = _dbContext.Branches
                .Include(c => c.Company)
                .FirstOrDefault(x => x.Id == id).Company.Id;
            return _dbContext.Companies.FirstOrDefault(x => x.Id == comp);
        }

        public void RemoveById(int Id)
        {
            var branch = _dbContext.Branches.FirstOrDefault(x => x.Id == Id);
            branch.IsDeleted = true;
            _dbContext.Entry(branch).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Update(Branch branch)
        {
            _dbContext.Entry(branch).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
