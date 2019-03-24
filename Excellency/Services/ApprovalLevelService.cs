using Excellency.Interfaces;
using Excellency.Models;
using Excellency.Persistence;
using Excellency.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.Services
{
    public class ApprovalLevelService : IApprovalLevel
    {
        private EASDbContext _dbContext;

        public string UserConnectionString { get; }

        public ApprovalLevelService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
            UserConnectionString = _dbContext.Database.GetDbConnection().ConnectionString;
        }
        public ApprovalLevelAssignment ApprovalLevelById(int id)
        {
            return _dbContext.ApprovalLevelAssignment.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<ApprovalLevelAssignment> ApprovalLevels()
        {
            return _dbContext.ApprovalLevelAssignment
                .Include(a => a.Employee)
                .Include(a => a.FirstApproval)
                .Include(a => a.SecondApproval)
                .Where(a => a.IsDeleted == false);
        }
        public IEnumerable<Account> Approvers(int id)
        {
            return _dbContext.Accounts
                .FromSql(@"SELECT *
                    FROM [dbo].[Accounts] [a]
                    WHERE [a].[Id] IN (
                                          SELECT [ara].[AccountId]
                                          FROM [dbo].[AccountRoleAssignments] [ara]
                                          WHERE [ara].[RoleId] = 2
                                      )")
                .Where(a => a.Id != id);
        }

        public IEnumerable<Account> Employees()
        {
            return _dbContext.Accounts
                    .Include(a => a.Company)
                    .Include(a => a.Branch)
                    .Include(a => a.Department)
                    .Include(a => a.Position)
                ;
        }
        public Account GetAccountById(int? id)
        {
            return _dbContext.Accounts.FirstOrDefault(a => a.Id == id);
        }
        public string GetNameById(int id)
        {
            var item = GetAccountById(id);
            var name = item.FirstName + " " + item.MiddleName + " " + item.LastName;
            return name;
        }
        public void Save(ApprovalLevelAssignment approval, int userid)
        {
            //if(approval.Id == 0)
            //{
            //    approval.CreatedBy = userid.ToString();
            //    approval.CreationDate = DateTime.Now;
            //    _dbContext.Add(approval);
            //}
            //else
            //{
            //    approval.ModifiedBy = userid.ToString();
            //    approval.ModifiedDate = DateTime.Now;
            //    _dbContext.Entry(approval).State = EntityState.Modified;
            //}
            //_dbContext.SaveChanges();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbo].[spApprovalLevelAssignment]"; 
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", approval.Id);
            cmd.Parameters.AddWithValue("@EmployeeId", approval.Employee.Id);
            cmd.Parameters.AddWithValue("@FirstApproval", approval.FirstApproval.Id);
            if(approval.IsWithSecondApproval)
            {
                cmd.Parameters.AddWithValue("@SecondApproval", approval.SecondApproval.Id);
            }
            cmd.Parameters.AddWithValue("@CreatedBy", userid);
            cmd.Parameters.AddWithValue("@ModifiedBy", userid);
            cmd.Parameters.AddWithValue("@IsWithSecondApproval", approval.IsWithSecondApproval ? 1 : 0);
            cmd.Parameters.AddWithValue("@QueryType", approval.Id == 0 ? 1 : 2);
            var result = SCObjects.ExecuteNonQuery(cmd, UserConnectionString);
        }
        public void RemoveById(int id)
        {
            var item = _dbContext.ApprovalLevelAssignment
                .Include(a => a.Employee)
                .FirstOrDefault(a => a.Employee.Id == id && a.IsDeleted == false);
            item.IsDeleted = true;
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public ApprovalLevelAssignment ApprovalAssignmentByEmployee(int id)
        {
            return _dbContext.ApprovalLevelAssignment
                .Include(a => a.Employee)
                .Include(a => a.SecondApproval)
                .Include(a => a.FirstApproval)
                .FirstOrDefault(a => a.Employee.Id == id && a.IsDeleted == false);
        }
        public bool IsSet(int id)
        {
            return _dbContext.ApprovalLevelAssignment
                .Include(a => a.Employee)
                .Where(a => a.IsDeleted == false)
                .Any(a => a.Employee.Id == id);
        }

        public IEnumerable<ApprovalLevelAccountItem> ApprovalLevelItems(int page)
        {
            string sql = string.Format(@"EXEC [dbo].[spApprovalLevelIndex] @Page = {0}", page.ToString());
            DataTable dt = SCObjects.LoadDataTable(sql, UserConnectionString);
            List<ApprovalLevelAccountItem> items = new List<ApprovalLevelAccountItem>();
            if(dt != null)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    var item = new ApprovalLevelAccountItem
                    {
                        Id = int.Parse(dr["Id"].ToString()),
                        Name = dr["Name"].ToString(),
                        Company = dr["Company"].ToString(),
                        Branch = dr["Branch"].ToString(),
                        Department = dr["Department"].ToString(),
                        Position = dr["Position"].ToString(),
                        IsSet = dr["IsSet"].ToString().Equals("1") ? true : false,
                    };
                    items.Add(item);
                }
            }

            return items;
        }
    }
}
