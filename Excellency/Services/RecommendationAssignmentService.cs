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
    public class RecommendationAssignmentService : IRecommendationAssignment
    {
        private EASDbContext _dbContext;

        public string UserConnectionString { get; }
        private const string StoredProcedure = "[dbo].[spRecommendationAssignment]";

        public RecommendationAssignmentService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
            UserConnectionString = _dbContext.Database.GetDbConnection().ConnectionString;
        }
        public IEnumerable<Account> Employees(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@QueryType", 1);
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

        public IEnumerable<RecommendationAssignment> RecommendationAssignments(int id)
        {
            return _dbContext.RecommendationAssignments
                .Include(a => a.Employee)
                .Include(a => a.Recommender)
                .Where(a => a.Recommender.Id == id && a.IsDeleted == false);
        }

        public IEnumerable<Account> Recommender()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@QueryType", 0);
            DataTable dt = SCObjects.ExecGetData(cmd, UserConnectionString);
            List<Account> items = new List<Account>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
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
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@QueryType", 4);
            var result = SCObjects.ExecuteNonQuery(cmd, UserConnectionString);
        }

        public void Save(RecommendationAssignment item, int userid)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", item.Id);
            cmd.Parameters.AddWithValue("@EmployeeId", item.Employee.Id);
            cmd.Parameters.AddWithValue("@RecommenderId", item.Recommender.Id);
            cmd.Parameters.AddWithValue("@CreatedBy", userid);
            cmd.Parameters.AddWithValue("@ModifiedBy", userid);
            cmd.Parameters.AddWithValue("@QueryType", item.Id == 0 ? 2 : 3);
            var result = SCObjects.ExecuteNonQuery(cmd, UserConnectionString);
        }

        public Account GetAccountById(int id)
        {
            return _dbContext.Accounts.FirstOrDefault(a => a.Id == id);
        }

        public string GetNameById(int id)
        {
            var item = GetAccountById(id);
            return item.FirstName + " " + item.LastName;
        }
    }
}
