using Excellency.Interfaces;
using Excellency.Models;
using Excellency.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.Services
{
    public class EvaluationReportService : IEvaluationReport
    {
        private EASDbContext _dbContext;

        public EvaluationReportService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<EmployeeEvaluation> GetAllRatingPerUser(int id)
        {
            List<EmployeeEvaluation> employeeEvaluations = new List<EmployeeEvaluation>();

            var UserConnectionString = _dbContext.Database.GetDbConnection().ConnectionString;
            DataTable dt = SCObjects.LoadDataTable(string.Format("EXEC [dbo].[spGetEvaluationScorePerRater] @Rater = {0}", id.ToString()), UserConnectionString);
            if(dt.Rows.Count > 0)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    var item = new EmployeeEvaluation
                    {
                        Id = int.Parse(dr["Id"].ToString()),
                        Name = dr["EmployeeName"].ToString(),
                        Score = decimal.Parse(dr["Total"].ToString()),
                    };
                    employeeEvaluations.Add(item);
                }
            }
            return employeeEvaluations;
        }
        public IEnumerable<EvaluationResultItem> GetResultPerEmployee(int id)
        {
            List<EvaluationResultItem> items = new List<EvaluationResultItem>();
            var UserConnectionString = _dbContext.Database.GetDbConnection().ConnectionString;
            var sql = string.Format("EXEC [dbo].[spGetEvaluationResultPerEmployee] @Id = {0}", id.ToString());
            DataTable dt = SCObjects.LoadDataTable(sql, UserConnectionString);
            if(dt.Rows.Count > 0)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    var item = new EvaluationResultItem
                    {
                        Id = int.Parse(dr["Id"].ToString()),
                        Type = dr["Type"].ToString(),
                        Name = dr["EmployeeName"].ToString(),
                        Weight = decimal.Parse(dr["Weight"].ToString()),
                        Percentage = dr["Percentage"].ToString(),
                        Score = decimal.Parse(dr["Score"].ToString()),
                        Total = decimal.Parse(dr["Total"].ToString())
                    };
                    items.Add(item);
                }
            }

            return items;
        }

        public EvaluationInterpretation InterpretationPerEmployee(int id)
        {
            var UserConnectionString = _dbContext.Database.GetDbConnection().ConnectionString;
            var item = new EvaluationInterpretation();
            string sql = string.Format("EXEC [dbo].[spGetInterpretationPerEmployee] @Id = {0}", id.ToString());
            DataTable dt = SCObjects.LoadDataTable(sql, UserConnectionString);

            if (dt.Rows.Count > 0)
            {
                item.Id = int.Parse(dt.Rows[0]["Id"].ToString());
                item.Name = dt.Rows[0]["Name"].ToString();
                item.Score = decimal.Parse(dt.Rows[0]["Id"].ToString());
                item.Title = dt.Rows[0]["Title"].ToString();
                item.Description = dt.Rows[0]["Description"].ToString();
                item.ScoreFrom = decimal.Parse(dt.Rows[0]["ScoreFrom"].ToString());
                item.ScoreTo = decimal.Parse(dt.Rows[0]["ScoreTo"].ToString());
            }

            return item;
        }
        private IEnumerable<int> AssignedEmployees(int id)
        {
            var result = _dbContext.RaterLine
                .Include(a => a.EmployeeRater)
                .Include(a => a.Employee)
                .Include(a => a.EmployeeRater.Rater)
                .Where(a => a.EmployeeRater.Rater.Id == id && a.IsDeleted == false);
            List<int> _employees = new List<int>();
            foreach(var item in result)
            {
                _employees.Add(item.Employee.Id);
            }
            return _employees;
        }
    }
}
