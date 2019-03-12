using Excellency.Interfaces;
using Excellency.Models;
using Excellency.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Excellency.Services
{
    public class EmployeeCriteriaService : IEmployeeCriteria
    {
        private EASDbContext _dbContext;

        public string UserConnectionString { get; }

        public EmployeeCriteriaService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
            UserConnectionString = _dbContext.Database.GetDbConnection().ConnectionString;
        }
        public EvaluationSeason ActivePeriod()
        {
            return _dbContext.EvaluationSeasons.FirstOrDefault(a => a.IsActive == true);
        }

        public IEnumerable<EmployeeCriteriaAssignment> Assignments(int criteriaid)
        {
            return _dbContext.EmployeeCriteriaAssignments
                .Include(a => a.Criteria)
                .Include(a => a.Employee)
                .Where(a => a.Criteria.IsDeleted == false && a.IsDeleted == false && a.Employee.IsDeleted == false && a.Criteria.Id == criteriaid);
        }

        public IEnumerable<CriteriaLine> CriteriaDetails(int id)
        {
            return _dbContext.CriteriaLines
                .Include(a => a.CriteriaHeader)
                .Where(a => a.CriteriaHeader.Id == id && a.IsDeleted == false);
        }

        public CriteriaHeader CriteriaHeaderById(int id)
        {
            return _dbContext.CriteriaHeaders.FirstOrDefault(a => a.Id == id); 
        }

        public IEnumerable<CriteriaHeader> Criterias()
        {
            return _dbContext.CriteriaHeaders.Where(a => a.IsDeleted == false);
        }

        public IEnumerable<Account> Employees(int headerid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Account> Employees(int headerid, int userid)
        {
            var sql = string.Format("SELECT * FROM [dbo].[fnEmployeeLookupForCriteria]({0},{1}) [x]", headerid.ToString(), userid.ToString());
            DataTable dt = SCObjects.LoadDataTable(sql, UserConnectionString);
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

        public void RemoveById(int id)
        {
            var item = _dbContext.EmployeeCriteriaAssignments.FirstOrDefault(a => a.Id == id);
            item.IsDeleted = true;
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Add(int criteriaid, int employeeid, int userid)
        {
            var account = _dbContext.Accounts.FirstOrDefault(a => a.Id == employeeid);
            var criteria = _dbContext.CriteriaHeaders.FirstOrDefault(a => a.Id == criteriaid);
            var item = new EmployeeCriteriaAssignment
            {
                Id = 0,
                Employee = account,
                Period = ActivePeriod(),
                Criteria = criteria,
                IsDeleted = false,
                CreatedBy = userid.ToString(),
                CreationDate = DateTime.Now,
                ModifiedBy = userid.ToString(),
                ModifiedDate = DateTime.Now,
            };
            _dbContext.Add(item);
            _dbContext.SaveChanges();
        }
    }
}
