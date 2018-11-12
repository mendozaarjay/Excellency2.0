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
    public class CriteriaService : ICriteria
    {
        private EASDbContext _dbContext;

        public CriteriaService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Criteria GetCriteriaById(int id)
        {
            return _dbContext.Criterias.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Criteria> GetCriterias()
        {
            return _dbContext.Criterias.Where(a => a.IsDeleted == false);
        }

        public void Remove(int id)
        {
            var item = GetCriteriaById(id);
            item.IsDeleted = true;
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Save(Criteria criteria, int UserId)
        {
            if(criteria.Id == 0)
            {
                criteria.CreatedBy = UserId.ToString();
                criteria.CreationDate = DateTime.Now;
                _dbContext.Add(criteria);
            }
            else
            {
                criteria.ModifiedBy = UserId.ToString();
                criteria.ModifiedDate = DateTime.Now;
                _dbContext.Entry(criteria).State = EntityState.Modified;
            }
            _dbContext.SaveChanges();
        }
    }
}
