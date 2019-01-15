using Excellency.Interfaces;
using Excellency.Models;
using Excellency.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Excellency.Services
{
    public class InterpretationService : IInterpretation
    {
        private EASDbContext _dbContext;

        public InterpretationService(EASDbContext context)
        {
            _dbContext = context;
        }
        public IEnumerable<Interpretation> GetAll()
        {
            return _dbContext.Interpretations.Where(a => a.IsDeleted == false);
        }

        public Interpretation GetById(int id)
        {
            var item = _dbContext.Interpretations.FirstOrDefault(a => a.Id == id);
            return item;
        }

        public void RemoveById(int id)
        {
            var item = _dbContext.Interpretations.FirstOrDefault(a => a.Id == id);
            item.IsDeleted = true;
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Save(Interpretation item)
        {
            if(item.Id == 0)
            {
                item.ModifiedBy = string.Empty;
                item.CreationDate = DateTime.Now;
                _dbContext.Add(item);
            }
            else
            {
                item.ModifiedDate = DateTime.Now;
                _dbContext.Entry(item).State = EntityState.Modified;
            }
            _dbContext.SaveChanges();
        }
        
    }
}
