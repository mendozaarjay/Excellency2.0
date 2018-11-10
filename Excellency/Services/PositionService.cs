using Excellency.Interfaces;
using Excellency.Models;
using Excellency.Persistence;
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

        public IEnumerable<Position> Positions()
        {
            return _dbContext.Positions.Where(a => a.IsDeleted == false);
        }

        public void RemoveById(int Id)
        {
            var Position = _dbContext.Positions.FirstOrDefault(a => a.Id == Id);
            Position.IsDeleted = true;
            _dbContext.Entry(Position).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Update(Position Position)
        {
            _dbContext.Entry(Position).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
