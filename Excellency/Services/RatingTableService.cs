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
    public class RatingTableService : IRatingTable
    {
        private EASDbContext _dbContext;

        public RatingTableService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddItem(RatingTableItem item)
        {
            if(item.Id == 0)
            {
                _dbContext.Add(item);
            }
            else
            {
                _dbContext.Entry(item).State = EntityState.Modified;
            }
            _dbContext.SaveChanges();
        }

        public RatingTable GetRatingTableById(int id)
        {
            return _dbContext.RatingTables.FirstOrDefault(a => a.Id == id);
        }

        public RatingTableItem GetTableItemPerId(int id)
        {
            return _dbContext.RatingTableItems.Include(a => a.RatingTable).FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<RatingTable> RatingTables()
        {
            return _dbContext.RatingTables.Where(a => a.IsDeleted == false);
        }

        public void RemoveById(int id)
        {
            var item = GetRatingTableById(id);
            item.IsDeleted = true;
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void RemoveItemPerId(int id)
        {
            var item = GetTableItemPerId(id);
            item.IsDeleted = true;
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Save(RatingTable item,string UserId)
        {
            if (item.Id == 0)
            {
                item.CreatedBy = UserId;
                item.CreationDate = DateTime.Now;
                _dbContext.Add(item);
            }
            else
            {
                item.ModifiedBy = UserId;
                item.ModifiedDate = DateTime.Now;
                _dbContext.Entry(item).State = EntityState.Modified;
            }
            _dbContext.SaveChanges();
        }

        public IEnumerable<RatingTableItem> TableItemsPerId(int RatingTableId)
        {
            return _dbContext.RatingTableItems.Include(a => a.RatingTable)
                .Where(a => a.IsDeleted == false && a.RatingTable.Id == RatingTableId);
        }
    }
}
