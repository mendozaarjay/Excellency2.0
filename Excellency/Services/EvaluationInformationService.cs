using Excellency.Interfaces;
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
    public class EvaluationInformationService : IEvaluationInformation
    {
        private EASDbContext _dbContext;

        public string UserConnectionString { get; }
        public const string StoredProcedure = "[dbo].[spEmployeeEvaluationTracking]";

        public EvaluationInformationService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
            UserConnectionString = _dbContext.Database.GetDbConnection().ConnectionString;
        }
        public DataTable GetAllBehavioralPerEmployee(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@EmployeeId", id);
            cmd.Parameters.AddWithValue("@QueryType", 1);
            return SCObjects.ExecGetData(cmd, UserConnectionString);
        }

        public DataTable GetAllBehavioralRecordPerId(int recordid)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@RecordId", recordid);
            cmd.Parameters.AddWithValue("@QueryType", 3);
            return SCObjects.ExecGetData(cmd, UserConnectionString);
        }

        public DataTable GetAllKRAPerEmployee(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@EmployeeId", id);
            cmd.Parameters.AddWithValue("@QueryType", 0);
            return SCObjects.ExecGetData(cmd, UserConnectionString);
        }

        public DataTable GetAllKRARecordPerId(int recordid)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@RecordId", recordid);
            cmd.Parameters.AddWithValue("@QueryType", 2);
            return SCObjects.ExecGetData(cmd, UserConnectionString);
        }

        public string Name(int id)
        {
            var item = _dbContext.Accounts.FirstOrDefault(a => a.Id == id);
            return item.FirstName + ' ' + item.LastName;
        }

        public DataTable GetApprovalLevel(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@EmployeeId", id);
            cmd.Parameters.AddWithValue("@QueryType", 4);
            return SCObjects.ExecGetData(cmd, UserConnectionString);
        }

        public void Confirm(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@RecordId", id);
            cmd.Parameters.AddWithValue("@QueryType", 5);
            var result = SCObjects.ExecuteNonQuery(cmd, UserConnectionString);
        }
    }
}
