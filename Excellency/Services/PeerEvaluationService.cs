using Excellency.Interfaces;
using Excellency.Models;
using Excellency.Persistence;
using Excellency.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.Services
{
    public class PeerEvaluationService : IPeerEvaluation
    {
        private EASDbContext _dbContext;

        public string UserConnectionString { get; }

        public PeerEvaluationService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
            UserConnectionString = _dbContext.Database.GetDbConnection().ConnectionString;
        }
        public IEnumerable<EmployeeItem> GetAccounts(int userid)
        {
            string sql = string.Format(@"SELECT a.[Id],[a].[FirstName] + ' ' + a.[MiddleName] + ' ' + a.[LastName] AS [Name]
                                        FROM [dbo].[Accounts] [a]
                                        WHERE [a].[Id] NOT IN (
                                                                  SELECT [peh].[EmployeeId]
                                                                  FROM [dbo].[PeerEvaluationHeader] [peh]
                                                                  WHERE [peh].[CreatedById] = {0}
                                                              )
															  AND [a].[Id] <> {0}", userid.ToString());
            DataTable dt = SCObjects.LoadDataTable(sql, UserConnectionString);
            List<EmployeeItem> items = new List<EmployeeItem>();
            if(dt != null)
            {
                if(dt.Rows.Count > 0)
                {
                   foreach(DataRow dr in dt.Rows)
                    {
                        var item = new EmployeeItem
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            Name = dr["Name"].ToString(),
                        };
                        items.Add(item);
                    }
                }
            }

            return items;
        }

        public PeerEvaluationHeader GetHeader(int id)
        {
            var item = _dbContext.PeerEvaluationHeader
                .Include(a => a.Employee)
                .Include(a => a.CreatedBy)
                .FirstOrDefault(a => a.Id == id);
            return item;
        }

        public IEnumerable<PeerEvaluationLine> GetLineItems(int headerid)
        {
            var items = _dbContext.PeerEvaluationLine
                .Include(a => a.PeerEvaluationHeader)
                .Include(a => a.PeerCriteria)
                .Where(a => a.PeerEvaluationHeader.Id == headerid);
            return items;
        }
        public IEnumerable<PeerEvaluationHeader> EvaluationHeaders(int userid)
        {
            var items = _dbContext.PeerEvaluationHeader
                .Include(a => a.Employee)
                .Include(a => a.CreatedBy)
                .Where(a => a.CreatedBy.Id == userid && a.IsDeleted == false && a.IsExpired == false);
            return items;
        }
        public void PostEvaluation(int id)
        {
            var item = _dbContext.PeerEvaluationHeader.FirstOrDefault(a => a.Id == id);
            item.Status = _dbContext.Statuses.FirstOrDefault(a => a.Id == TransactionStatus.Posted.ToInt());
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();

        }
        public void SavePeerEvaluation(PeerEvaluationHeader header, IEnumerable<PeerEvaluationLine> items, int userid)
        {

            if(header.Id == 0)
            {
                header.CreatedBy = _dbContext.Accounts.FirstOrDefault(a => a.Id == userid);
                header.DateCreated = DateTime.Now;
                header.Status = _dbContext.Statuses.FirstOrDefault(a => a.Id == TransactionStatus.Save.ToInt());
                _dbContext.Add(header);
            }
            else
            {
                _dbContext.Entry(header).State = EntityState.Modified;
            }
            foreach(var i in items)
            {
                i.PeerEvaluationHeader = header;
                if(i.Id == 0)
                {
                    _dbContext.Add(i);
                }
                else
                {
                    _dbContext.Entry(i).State = EntityState.Modified;
                }
            }
            _dbContext.SaveChanges();
        }

        public IEnumerable<PeerCriteria> GetCriterias()
        {
            return _dbContext.PeerCriterias.Where(a => a.IsDeleted == false);
        }

        public IEnumerable<PeerEvaluationListingItem> Evaluations(int userid)
        {
            List<PeerEvaluationListingItem> items = new List<PeerEvaluationListingItem>();

            DataTable dt = SCObjects.LoadDataTable(string.Format("EXEC [dbo].[spPeerEvaluationSummaryPerEmployee] @Id = {0}", userid.ToString()), UserConnectionString);
            if(dt != null)
            {
                if(dt.Rows.Count > 0)
                {
                    foreach(DataRow dr in dt.Rows)
                    {
                        var item = new PeerEvaluationListingItem
                        {
                            EvaluationId = int.Parse(dr["EvaluationId"].ToString()),
                            Id = int.Parse(dr["Id"].ToString()),
                            Name = dr["Name"].ToString(),
                            Position = dr["Position"].ToString(),
                            Company = dr["Company"].ToString(),
                            Branch = dr["Branch"].ToString(),
                            Department = dr["Department"].ToString(),
                            EvaluationDate = dr["DateEvaluated"].ToString(),
                            TotalScore = int.Parse(dr["TotalScore"].ToString()),
                            Status = dr["Status"].ToString()
                        };
                        items.Add(item);
                    }
                }
            }

            return items;
        }
        public PeerCriteria GetPeerCriteriaById(int id)
        {
            return _dbContext.PeerCriterias.FirstOrDefault(a => a.Id == id);
        }
        public Account GetAccountById(int id)
        {
            return _dbContext.Accounts.FirstOrDefault(a => a.Id == id);
        }
        public string GetNameById(int id)
        {
            var item = _dbContext.Accounts.FirstOrDefault(a => a.Id == id);
            var name = item.FirstName + " " + item.MiddleName + " " + item.LastName;
            return name;
        }
    }
}
