using Excellency.Interfaces;
using Excellency.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.Services
{
    public class EvaluationSettingsService : IEvaluationSettings
    {
        private EASDbContext _dbContext;

        public string UserConnectionString { get; }

        public EvaluationSettingsService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
            UserConnectionString = dbContext.Database.GetDbConnection().ConnectionString;
        }
        public decimal GetBehavioralPercentage()
        {
            var item = SCObjects.ReturnText("EXEC [dbo].[spEvaluationSettings] @QueryType = 0", UserConnectionString);
            return decimal.Parse(item);
        }

        public decimal GetKRAPercentage()
        {
            var item = SCObjects.ReturnText("EXEC [dbo].[spEvaluationSettings] @QueryType = 1", UserConnectionString);
            return decimal.Parse(item);
        }

        public void Save(decimal behavioral, decimal kra)
        {
            SqlCommand cmd = new SqlCommand();
            List<SqlCommand> cmdList = new List<SqlCommand>();
            cmd.CommandText = "[dbo].[spEvaluationSettings]";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@KRA", kra);
            cmd.Parameters.AddWithValue("@QueryType", 2);
            cmdList.Add(cmd);

            SqlCommand cmdNew = new SqlCommand();
            cmdNew.CommandText = "[dbo].[spEvaluationSettings]";
            cmdNew.Parameters.Clear();
            cmdNew.Parameters.AddWithValue("@Behavioral", behavioral);
            cmdNew.Parameters.AddWithValue("@QueryType", 3);
            cmdList.Add(cmd);
            var result = SCObjects.ExecuteNonQuery(cmdList, UserConnectionString);
        }
    }
}
