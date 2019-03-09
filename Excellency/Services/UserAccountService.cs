using Excellency.Interfaces;
using Excellency.Persistence;
using Excellency.Secured;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.Services
{
    public class UserAccountService : IUserAccountNew
    {
        private EASDbContext _dbContext;

        public string UserConnectionString { get; }
        public const string StoredProcedure = "[dbo].[spUserAccount]";

        public UserAccountService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
            UserConnectionString = _dbContext.Database.GetDbConnection().ConnectionString;
        }
        public DataTable AccountInfo(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@QueryType", 0);
            return SCObjects.ExecGetData(cmd, UserConnectionString);
        }

        public bool IsValidPassword(int id, string password)
        {
            string sql = string.Format("EXEC [dbo].[spUserAccount] @Id = {0},@QueryType = 1", id.ToString());
            var checkthis = SCObjects.ReturnText(sql, UserConnectionString);
            var oldpassword = Security.Decrypt(checkthis);
            return oldpassword.Equals(password);
        }

        public string ChangePassword(int id, string password)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Password", Security.Encrypt(password));
            cmd.Parameters.AddWithValue("@QueryType", 2);
            return SCObjects.ExecuteNonQuery(cmd, UserConnectionString);
        }
    }
}
