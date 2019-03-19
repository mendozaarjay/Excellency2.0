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
    public class PositionService : IPosition
    {
        private EASDbContext _dbContext;

        public PositionService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Position Position)
        {
            _dbContext.Positions.Add(Position);
            _dbContext.SaveChanges();
        }

        public Position GetPositionById(int id)
        {
            return _dbContext.Positions.FirstOrDefault(a => a.Id == id);
        }

        public PositionLevel PositionLevelById(int id)
        {
            return _dbContext.PositionLevels.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<PositionLevel> PositionLevels()
        {
            return _dbContext.PositionLevels.Where(a => a.IsDeleted == false);
        }

        public IEnumerable<Position> Positions()
        {
            return _dbContext.Positions
                .Include(a => a.PositionLevel)
                .Where(a => a.IsDeleted == false);
        }

        public void RemoveById(int Id)
        {
            var Position = _dbContext.Positions.FirstOrDefault(a => a.Id == Id);
            Position.IsDeleted = true;
            _dbContext.Entry(Position).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Save(Position item, int userid)
        {
            if (item.Id == 0)
            {
                item.CreatedBy = userid.ToString();
                item.CreationDate = DateTime.Now;
                _dbContext.Add(item);
            }
            else
            {
                var entry = _dbContext.Positions.FirstOrDefault(a => a.Id == item.Id);
                entry.Description = item.Description;
                entry.PositionLevel = item.PositionLevel;
                entry.ModifiedBy = userid.ToString();
                entry.ModifiedDate = DateTime.Now;
                _dbContext.Entry(entry).State = EntityState.Modified;
            }
            _dbContext.SaveChanges();
        }

        public void Update(Position Position)
        {
            _dbContext.Entry(Position).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
