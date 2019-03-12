using Excellency.Interfaces;
using Excellency.Models;
using Excellency.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.Services
{
    public class CriteriaEvaluationService : ICriteriaEvaluation
    {
        private EASDbContext _dbContext;

        public string UserConnectionString { get; }
        private const string StoredProcedure = "[dbo].[spPeerCriteriaEvaluation]";

        public CriteriaEvaluationService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
            UserConnectionString = _dbContext.Database.GetDbConnection().ConnectionString;
        }
        public IEnumerable<Account> Accounts(int userId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", userId);
            cmd.Parameters.AddWithValue("@QueryType", 0);
            DataTable dt = SCObjects.ExecGetData(cmd, UserConnectionString);
            List<Account> items = new List<Account>();
            if(dt != null)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    var item = new Account
                    {
                        Id = int.Parse(dr["Id"].ToString()),
                        FirstName = dr["Firstname"].ToString(),
                        MiddleName = dr["Middlename"].ToString(),
                        LastName = dr["Lastname"].ToString(),
                    };
                    items.Add(item);
                }
            }
            return items;
        }

        public EvaluationSeason ActivePeriod()
        {
            return _dbContext.EvaluationSeasons.FirstOrDefault(a => a.IsDeleted == false && a.IsActive == true);
        }

        public CriteriaEvaluationHeader CriteriaEvaluationPerEmployeeId(int id, int userid)
        {
            return _dbContext.CriteriaEvaluationHeaders
                .Include(a => a.Ratee)
                .Include(a => a.Rater)
                .Include(a => a.Criteria)
                .Include(a => a.Status)
                .Include(a => a.Period)
                .FirstOrDefault(a => a.Rater.Id == userid && a.Ratee.Id == id && a.Period.Id == ActivePeriod().Id);
        }
        public IEnumerable<CriteriaEvaluationLine> EvaluationLinesByHeaderId(int id)
        {
            return _dbContext.CriteriaEvaluationLines
                .Include(a => a.Header)
                .Include(a => a.CriteriaLine)
                .Where(a => a.Header.Id == id);
        }
        public CriteriaHeader CriteriaHeaderById(int id)
        {
            return _dbContext.CriteriaHeaders.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<CriteriaLine> CriteriaLinesByHeaderId(int id)
        {
            return _dbContext.CriteriaLines
                .Include(a => a.CriteriaHeader)
                .Where(a => a.CriteriaHeader.Id == id && a.IsDeleted == false);
        }

        public EmployeeCriteriaAssignment EmployeeCriteriaAssignmentByEmployeeId(int id)
        {
            return _dbContext.EmployeeCriteriaAssignments
                .Include(a => a.Employee)
                .Include(a => a.Criteria)
                .Include(a => a.Period)
                .FirstOrDefault(a => a.Employee.Id == id && a.Period.Id == ActivePeriod().Id);
        }



        public Account GetAccountById(int id)
        {
            return _dbContext.Accounts.FirstOrDefault(a => a.Id == id);
        }

        public string GetName(int id)
        {
            var item = GetAccountById(id);
            return item.FirstName + " " + item.LastName;
        }

        public void Post(int id)
        {
            var item = _dbContext.CriteriaEvaluationHeaders.FirstOrDefault(a => a.Id == id);
            var status = _dbContext.Statuses.FirstOrDefault(a => a.Id == TransactionStatus.Posted.ToInt());
            item.Status = status;
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Save(CriteriaEvaluationHeader header, IEnumerable<CriteriaEvaluationLine> items, int userid)
        {
            var status = _dbContext.Statuses.FirstOrDefault(a => a.Id == TransactionStatus.Save.ToInt());
            header.Status = status;
            header.DateCreated = DateTime.Now;
            header.Period = ActivePeriod();
            _dbContext.Add(header);
            foreach(var item in items)
            {
                item.Header = header;
                _dbContext.Add(item);
            };
            _dbContext.SaveChanges();
        }

        public void Update(CriteriaEvaluationHeader header, IEnumerable<CriteriaEvaluationLine> items, int userid)
        {
            var status = _dbContext.Statuses.FirstOrDefault(a => a.Id == TransactionStatus.Save.ToInt());
            header.Status = status;
            header.DateCreated = DateTime.Now;
            header.Period = ActivePeriod();
            _dbContext.Entry(header).State = EntityState.Modified;
            foreach(var item in items)
            {
                item.Header = header;
                _dbContext.Entry(item).State = EntityState.Modified;
            }
            _dbContext.SaveChanges();
        }

        public CriteriaLine CriteriaLineById(int id)
        {
            return _dbContext.CriteriaLines.FirstOrDefault(a => a.Id == id);
        }

        public CriteriaEvaluationLine CriteriaEvaluationLineById(int id)
        {
            return _dbContext.CriteriaEvaluationLines.FirstOrDefault(a => a.Id == id);
        }

        public bool IsEvaluated(int employeeid, int id,int userid)
        {
            return _dbContext.CriteriaEvaluationHeaders
                .Include(a => a.Criteria)
                .Include(a => a.Ratee)
                .Include(a => a.Rater)
                .Where(a => a.Criteria.Id == id && a.Ratee.Id == employeeid && a.Rater.Id == userid).Any();
        }

        public bool IsPosted(int employeeid, int id, int userid)
        {
            return _dbContext.CriteriaEvaluationHeaders
                .Include(a => a.Criteria)
                .Include(a => a.Ratee)
                .Include(a => a.Rater)
                .Include(a => a.Status)
                .Where(a => a.Criteria.Id == id && a.Ratee.Id == employeeid && a.Rater.Id == userid && a.Status.Id == TransactionStatus.Posted.ToInt()).Any();
        }
    }
}
