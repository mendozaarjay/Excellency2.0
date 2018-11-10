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
    public class BehavioralFactorService : IBehavioralFactor
    {
        private EASDbContext _dbContext;

        public BehavioralFactorService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<EmployeeCategory> EmployeeCategories()
        {
            return _dbContext.EmployeeCategories.Where(a => a.IsDeleted == false);
        }

        public BehavioralFactor GetBehavioralFactorById(int id)
        {
            return _dbContext.BehavioralFactors.FirstOrDefault(a => a.Id == id);
        }

        public BehavioralFactorItem GetBehavioralFactorItemById(int id)
        {
            return _dbContext.BehavioralFactorItems.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<BehavioralFactorItem> GetBehavioralFactorItemsByHeaderId(int id)
        {
            return _dbContext.BehavioralFactorItems
                .Include(a => a.BehavioralFactor)
                .Where(a => a.IsDeleted == false && a.BehavioralFactor.Id == id);
        }

        public IEnumerable<BehavioralFactor> GetBehavioralFactors()
        {
            return _dbContext.BehavioralFactors
                .Include(a => a.Category)
                .Where(a => a.IsDeleted == false);
        }

        public EmployeeCategory GetEmployeeCategoryById(int id)
        {
            return _dbContext.EmployeeCategories.FirstOrDefault(a => a.Id == id);
        }

        public void RemoveBehavioralFactorById(int id)
        {
            var item = GetBehavioralFactorById(id);
            item.IsDeleted = true;
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void RemoveFactorItemById(int id)
        {
            var item = GetBehavioralFactorItemById(id);
            item.IsDeleted = true;
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void SaveBehavioralFactor(BehavioralFactor behavioralFactor)
        {
            if(behavioralFactor.Id == 0)
            {
                _dbContext.Add(behavioralFactor);
            }
            else
            {
                _dbContext.Entry(behavioralFactor).State = EntityState.Modified;
            }
            _dbContext.SaveChanges();
        }

        public void SaveFactorItem(BehavioralFactorItem factorItem)
        {
            if(factorItem.Id == 0)
            {
                _dbContext.Add(factorItem);
            }
            else
            {
                _dbContext.Entry(factorItem).State = EntityState.Modified;
            }
            _dbContext.SaveChanges();
        }
    }
}
