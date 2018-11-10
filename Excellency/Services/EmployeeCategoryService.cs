using Excellency.Interfaces;
using Excellency.Models;
using Excellency.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.Services
{
    public class EmployeeCategoryService : IEmployeeCategory
    {
        private EASDbContext _dbContext;

        public EmployeeCategoryService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<EmployeeCategory> EmployeeCategories()
        {
            return _dbContext.EmployeeCategories.Where(a => a.IsDeleted == false);
        }

        public EmployeeCategory GetEmployeeCategory(int id)
        {
            return _dbContext.EmployeeCategories.FirstOrDefault(a => a.Id == id);
        }

        public void RemoveCategoryPerId(int id)
        {
            var item = GetEmployeeCategory(id);
            item.IsDeleted = true;
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Save(EmployeeCategory category,string UserId)
        {
            if(category.Id == 0)
            {
                category.CreatedBy = UserId;
                category.CreationDate = DateTime.Now;
                _dbContext.Add(category);
            }
            else
            {
                category.ModifiedBy = UserId;
                category.ModifiedDate = DateTime.Now;
                _dbContext.Entry(category).State = EntityState.Modified;
            }
            _dbContext.SaveChanges();
        }
    }
}
