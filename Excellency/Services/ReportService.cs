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
    }
}
