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
    public class CriteriaSettingService : ICriteriaSetting
    {
        private EASDbContext _dbContext;

        public CriteriaSettingService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public CriteriaHeader CriteriaHeaderById(int id)
        {
            return _dbContext.CriteriaHeaders.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<CriteriaHeader> CriteriaHeaders()
        {
            return _dbContext.CriteriaHeaders.Where(a => a.IsDeleted == false);
        }

        public CriteriaLine CriteriaLineById(int id)
        {
            return _dbContext.CriteriaLines.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<CriteriaLine> LineItemsByHeaderId(int id)
        {
            return _dbContext.CriteriaLines
                 .Include(a => a.CriteriaHeader)
                 .Where(a => a.CriteriaHeader.Id == id && a.IsDeleted == false);
        }

        public void RemoveHeader(int id)
        {
            var item = this.CriteriaHeaderById(id);
            item.IsDeleted = true;
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void RemoveLine(int id)
        {
            var item = this.CriteriaLineById(id);
            item.IsDeleted = true;
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void SaveHeader(CriteriaHeader item, int userid)
        {
            item.CreatedBy = userid.ToString();
            item.CreationDate = DateTime.Now;
            item.ModifiedBy = userid.ToString();
            item.ModifiedDate = DateTime.Now;
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

        public void SaveLine(int headerId, CriteriaLine item)
        {
            var header = CriteriaHeaderById(headerId);
            if(item.Id == 0)
            {
                item.CriteriaHeader = header;
                _dbContext.Add(item);
            }
            else
            {
                item.CriteriaHeader = header;
                _dbContext.Entry(item).State = EntityState.Modified;
            }
            _dbContext.SaveChanges();
        }
    }
}
