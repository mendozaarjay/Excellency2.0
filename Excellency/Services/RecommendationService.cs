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
    public class RecommendationService : IRecommendation
    {
        private EASDbContext _dbContext;

        public string UserConnectionString { get; }

        public RecommendationService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
            UserConnectionString = _dbContext.Database.GetDbConnection().ConnectionString;
        }
        public IEnumerable<Account> Accounts(int userid)
        {
            return _dbContext.Accounts.Where(a => a.IsDeleted == false && a.IsDeactivated == false && a.Id != userid);
        }

        public IEnumerable<RatingBehavioralFactor> BehavioralRatingById(int id)
        {
            return _dbContext.RatingBehavioralFactors
                .Include(a => a.RatingHeader)
                .Include(a => a.BehavioralFactor)
                .Include(a => a.BehavioralFactorItem)
                .Where(a => a.RatingHeader.Id == id );
        }

        public IEnumerable<Recommendation> GetAllRecommendations(int id)
        {
            return _dbContext.Recommendations
                .Include(a => a.Employee)
                .Where(a => a.IsDeleted == false && a.IsExpired == false);
        }

        public string GetNameById(int id)
        {
            var item = _dbContext.Accounts.FirstOrDefault(a => a.Id == id);
            return (item.FirstName + " " + item.MiddleName + " " + item.LastName).Replace("  ", " ");
        }

        public IEnumerable<RatingKeySuccessArea> KRARatingById(int id)
        {
            return _dbContext.RatingKeySuccessAreas
                .Include(a => a.RatingHeader)
                .Include(a => a.KeyResultArea)
                .Include(a => a.KeySuccessIndicator)
                .Where(a => a.RatingHeader.Id == id);
        }

        public RatingHeader RatingById(int id)
        {
            return _dbContext.RatingHeader.FirstOrDefault(a => a.Id == id);
        }

        public Recommendation RecommendationById(int id)
        {
            return _dbContext.Recommendations.FirstOrDefault(a => a.Id == id);
        }

        public void RemoveById(int id)
        {
            var item = RecommendationById(id);
            item.IsDeleted = true;
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Save(Recommendation item, int userid)
        {
            if(item.Id == 0)
            {
                item.EvaluationSeason = ActiveSeason();
                item.CreatedBy = userid.ToString();
                item.CreationDate = DateTime.Now;
                _dbContext.Add(item);
            }
            else
            {
                item.EvaluationSeason = ActiveSeason();
                item.ModifiedBy = userid.ToString();
                item.ModifiedDate = DateTime.Now;
                _dbContext.Entry(item).State = EntityState.Modified;
            }
            _dbContext.SaveChanges();
        }

        public RatingHeader BehavioralEvaluationByEmployee(int id)
        {
            return _dbContext.RatingHeader
                .Include(a => a.Ratee)
                .Include(a => a.Rater)
                .Include(a => a.Status)
                .FirstOrDefault(a => a.Ratee.Id == id && a.IsExpired == false && a.IsDeleted == false && a.Type == "behavioral");
        }

        public RatingHeader KRAEvaluationByEmployee(int id)
        {
            return _dbContext.RatingHeader
                .Include(a => a.Ratee)
                .Include(a => a.Rater)
                .Include(a => a.Status)
                .FirstOrDefault(a => a.Ratee.Id == id && a.IsExpired == false && a.IsDeleted == false && a.Type == "kra");
        }
        public bool IsWithRecommendation(int id)
        {
            string sql = string.Format("SELECT 1 FROM [dbo].[Recommendations] [r] WHERE [r].[EmployeeId] = {0} AND [r].[IsExpired] = 0 AND [r].[IsDeleted] = 0", id.ToString());
            string CheckThis = SCObjects.ReturnText(sql, UserConnectionString);
            return CheckThis.Length > 0;
        }

        public Recommendation RecommendationByEmployeeId(int id)
        {
            return _dbContext.Recommendations
                .Include(a => a.Employee)
                .FirstOrDefault(a => a.Employee.Id == id && a.IsDeleted == false && a.IsExpired == false);
        }

        public Account GetAccountById(int id)
        {
            return _dbContext.Accounts.FirstOrDefault(a => a.Id == id);
        }
        public EvaluationSeason ActiveSeason()
        {
            return _dbContext.EvaluationSeasons.FirstOrDefault(a => a.IsActive == true);
        }
        public bool IsWithActiveSeason()
        {
            return _dbContext.EvaluationSeasons.Any(a => a.IsActive == true);
        }
    }
}
