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
    public class EvaluationSeasonService : IEvaluationSeason
    {
        private EASDbContext _dbContext;

        public string UserConnectionString { get; }

        public EvaluationSeasonService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
            UserConnectionString = _dbContext.Database.GetDbConnection().ConnectionString;
        }

        public void CloseAllExisting()
        {
            throw new NotImplementedException();
        }

        public EvaluationSeason EvaluationSeasonById(int id)
        {
            return _dbContext.EvaluationSeasons.FirstOrDefault(a => a.Id == id);
        }

        public void RemoveById(int id)
        {
            var item = EvaluationSeasonById(id);
            item.IsDeleted = true;
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Save(EvaluationSeason season, int userid)
        {
            if(season.Id == 0)
            {
                season.CreatedBy = userid.ToString();
                season.CreationDate = DateTime.Now;
                _dbContext.Add(season);
            }
            else
            {
                season.ModifiedBy = userid.ToString();
                season.ModifiedDate = DateTime.Now;
                _dbContext.Entry(season).State = EntityState.Modified;
            }
            _dbContext.SaveChanges();
        }

        public IEnumerable<EvaluationSeason> Seasons()
        {
            return _dbContext.EvaluationSeasons.Where(a => a.IsDeleted == false);
        }

        public void SetActive(int id)
        {
            foreach(var i in _dbContext.EvaluationSeasons.Where(a => a.Id != id))
            {
                i.IsActive = false;
                _dbContext.Entry(i).State = EntityState.Modified;
            }
            var item = EvaluationSeasonById(id);
            item.IsActive = true;
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
