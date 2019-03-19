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
    public class PositionLevelService : IPositionLevel
    {
        private EASDbContext _dbContext;

        public PositionLevelService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Save(PositionLevel item, int userId)
        {
            if(item.Id == 0)
            {
                item.CreatedBy = userId.ToString();
                item.CreationDate = DateTime.Now;
                _dbContext.Add(item);
            }
            else
            {
                var entry = _dbContext.PositionLevels.FirstOrDefault(a => a.Id == item.Id);
                entry.Description = item.Description;
                entry.Level = item.Level;
                item.ModifiedBy = userId.ToString();
                item.ModifiedDate = DateTime.Now;
                _dbContext.Entry(entry).State = EntityState.Modified;
            }
            _dbContext.SaveChanges();
        }

        public PositionLevel PositionLevelById(int id)
        {
            var item = _dbContext.PositionLevels.FirstOrDefault(a => a.Id == id);
            return item;
        }

        public IEnumerable<PositionLevel> PositionLevels()
        {
            return _dbContext.PositionLevels.Where(a => a.IsDeleted == false);
        }

        public void RemoveById(int id)
        {
            var item = _dbContext.PositionLevels.FirstOrDefault(a => a.Id == id);
            item.IsDeleted = true;
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
