using Excellency.Interfaces;
using Excellency.Persistence;
using Excellency.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Excellency.Services
{
    public class ApprovalService : IApproval
    {
        private EASDbContext _dbContext;

        public string UserConnectionString { get; }

        private string StoredProcedure = "[dbo].[spApproval]";

        public ApprovalService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
            UserConnectionString = _dbContext.Database.GetDbConnection().ConnectionString;
        }
        public void ApproveFirstApproval(int id, string remarks, int userid)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Remarks", remarks);
            cmd.Parameters.AddWithValue("@ApproverId", userid);
            cmd.Parameters.AddWithValue("@QueryType", 1);
            var result = SCObjects.ExecuteNonQuery(cmd, UserConnectionString);
        }

        public void ApproveSecondApproval(int id, string remarks, int userid)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Remarks", remarks);
            cmd.Parameters.AddWithValue("@ApproverId", userid);
            cmd.Parameters.AddWithValue("@QueryType", 3);
            var result = SCObjects.ExecuteNonQuery(cmd, UserConnectionString);
        }

        public void DisapproveFirstApproval(int id, string remarks, int userid)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Remarks", remarks);
            cmd.Parameters.AddWithValue("@ApproverId", userid);
            cmd.Parameters.AddWithValue("@QueryType", 2);
            var result = SCObjects.ExecuteNonQuery(cmd, UserConnectionString);
        }

        public void DisapproveSecondApproval(int id, string remarks, int userid)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Remarks", remarks);
            cmd.Parameters.AddWithValue("@ApproverId", userid);
            cmd.Parameters.AddWithValue("@QueryType",4);
            var result = SCObjects.ExecuteNonQuery(cmd, UserConnectionString);
        }

        public IEnumerable<FirstApprovalItem> GetAllFirstApprovals(int userid)
        {
            List<FirstApprovalItem> items = new List<FirstApprovalItem>();
            string sql = string.Format("EXEC [dbo].[spFirstApprovalPerApprover] @Id = {0}", userid.ToString());
            DataTable dt = SCObjects.LoadDataTable(sql, UserConnectionString);
            if(dt != null)
            {
                if(dt.Rows.Count > 0)
                {
                    foreach(DataRow dr in dt.Rows)
                    {
                        var item = new FirstApprovalItem
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            Type = dr["Type"].ToString(),
                            Name = dr["Name"].ToString(),
                            Title = dr["Title"].ToString(),
                            Description = dr["Description"].ToString(),
                            Weight = int.Parse(dr["Weight"].ToString()),
                            Status = dr["Status"].ToString(),
                            RatedBy = dr["RatedBy"].ToString(),
                            RatedDate = dr["RatedDate"].ToString(),
                            PostedDate = dr["PostedDate"].ToString(),
                            FirstApproverRemarks = dr["FirstApproverRemarks"].ToString(),
                            SecondApproverRemarks = dr["SecondApproverRemarks"].ToString(),
                        };
                        items.Add(item);
                    }
                }
            }
            return items;
        }

        public IEnumerable<SecondApprovalItem> GetAllSecondApprovals(int userid)
        {
            List<SecondApprovalItem> items = new List<SecondApprovalItem>();
            string sql = string.Format("EXEC [dbo].[spSecondApprovalPerApprover] @Id = {0}", userid.ToString());
            DataTable dt = SCObjects.LoadDataTable(sql, UserConnectionString);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        var item = new SecondApprovalItem
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            Type = dr["Type"].ToString(),
                            Name = dr["Name"].ToString(),
                            Title = dr["Title"].ToString(),
                            Description = dr["Description"].ToString(),
                            Weight = int.Parse(dr["Weight"].ToString()),
                            Status = dr["Status"].ToString(),
                            RatedBy = dr["RatedBy"].ToString(),
                            RatedDate = dr["RatedDate"].ToString(),
                            PostedDate = dr["PostedDate"].ToString(),
                            FirstApproverRemarks = dr["FirstApproverRemarks"].ToString(),
                            SecondApproverRemarks = dr["SecondApproverRemarks"].ToString(),
                            FirstApproval = dr["FirstApproval"].ToString(),
                            FirstApprovalDate = dr["FirstApprovalDate"].ToString(),
                        };
                        items.Add(item);
                    }
                }
            }
            return items;
        }

        public bool IsWithFirstApproval(int userid)
        {
            var checkThis = SCObjects.ReturnText(string.Format("SELECT [dbo].[fnIsWithFirstApproval]({0})", userid.ToString()), UserConnectionString);
            return checkThis.Equals("1");
        }

        public bool IsWithSecondApproval(int userid)
        {
            var checkThis = SCObjects.ReturnText(string.Format("SELECT [dbo].[fnIsWithSecondApproval]({0})", userid.ToString()), UserConnectionString);
            return checkThis.Equals("1");
        }
    }
}
