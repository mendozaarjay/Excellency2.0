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
    public class ReportService : IReport
    {
        private EASDbContext _dbContext;

        public string UserConnectionString { get; }
        private const string StoredProcedure = "[dbo].[spEvaluationReport]";

        public ReportService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
            UserConnectionString = _dbContext.Database.GetDbConnection().ConnectionString;
        }
        public IEnumerable<EvaluationReport> Evaluations(int periodid, string type)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@EvaluationSeason", periodid);
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@QueryType", 1);
            DataTable dt = SCObjects.ExecGetData(cmd, UserConnectionString);
            List<EvaluationReport> items = new List<EvaluationReport>();
            if(dt != null)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    var item = new EvaluationReport
                    {
                        Id = int.Parse(dr["Id"].ToString()),
                        Name = dr["Name"].ToString(),
                        Rater = dr["Rater"].ToString(),
                        Period = dr["Period"].ToString(),
                        Type = dr["Type"].ToString(),
                        Status = dr["Status"].ToString(),
                        DateCreated = dr["DateCreated"].ToString()
                    };
                    items.Add(item);
                }
            }
            return items;
        }

        public IEnumerable<EvaluationSeason> EvaluationSeasons()
        {
            return _dbContext.EvaluationSeasons.Where(a => a.IsDeleted == false);
        }

        public IEnumerable<EmployeeInformation> Employees(string keyword)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Keyword", keyword);
            cmd.Parameters.AddWithValue("@QueryType", 2);
            DataTable dt = SCObjects.ExecGetData(cmd, UserConnectionString);
            List<EmployeeInformation> items = new List<EmployeeInformation>();
            if(dt != null)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    var item = new EmployeeInformation
                    {
                        Id = int.Parse(dr["Id"].ToString()),
                        Name = dr["Name"].ToString(),
                        EmployeeNo = dr["EmployeeNo"].ToString(),
                        Category = dr["Category"].ToString(),
                        Company = dr["Company"].ToString(),
                        Branch = dr["Branch"].ToString(),
                        Department = dr["Department"].ToString(),
                        Position = dr["Position"].ToString(),
                    };
                    items.Add(item);
                }
            }
            return items;
        }

        public IEnumerable<EmployeePerformance> EmployeePerformances(int period, string keyword)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbo].[spEmployeePerformance]";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@SeasonId", period);
            cmd.Parameters.AddWithValue("@Keyword", keyword);
            DataTable dt = SCObjects.ExecGetData(cmd, UserConnectionString);

            List<EmployeePerformance> items = new List<EmployeePerformance>();
            if(dt != null)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    var item = new EmployeePerformance
                    {
                        Id = int.Parse(dr["Id"].ToString()),
                        Name = dr["EmployeeName"].ToString(),
                        TotalScore = decimal.Parse(dr["TotalScore"].ToString()),
                        TotalWeight = decimal.Parse(dr["TotalWeight"].ToString()),
                        ConvertedScore = decimal.Parse(dr["ConvertedScore"].ToString()),
                        WeightedScore = decimal.Parse(dr["WeightedScore"].ToString()),
                        Percentage = decimal.Parse(dr["Percentage"].ToString()),
                    };
                    items.Add(item);
                }
            }
            return items;
            
        }
    }
}
