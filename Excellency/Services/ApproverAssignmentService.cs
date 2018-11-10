using Excellency.Interfaces;
using Excellency.Models;
using Excellency.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.Services
{
    public class ApproverAssignmentService : IApproverAssignment
    {
        private EASDbContext _dbContext;

        public ApproverAssignmentService(EASDbContext context)
        {
            _dbContext = context;
        }
        public void AssignAccounts(List<int> Accounts, int ApproverId ,int UserId)
        {
            foreach(var item in Accounts)
            {
                var assignment = new ApproverAssignment
                {
                    User = GetAccountById(item),
                    Approver = GetAccountById(ApproverId),
                    CreatedBy = UserId.ToString(),
                    CreationDate = DateTime.Now
                };
                _dbContext.Add(assignment);
            }
            _dbContext.SaveChanges();
        }

        public Account GetAccountById(int id)
        {
            return _dbContext.Accounts.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Account> GetAccountsById(int id)
        {
            var result = _dbContext.Accounts
                         .Where(a => a.Id != id && a.IsDeleted == false);
            return result;
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            var result = _dbContext.Accounts
             .Include(a => a.Company)
             .Include(a => a.Branch)
             .Include(a => a.Department)
             .Include(a => a.Position)
             .Where(a => a.IsDeleted == false);
            return result;
        }

        public IEnumerable<ApproverAssignment> GetAssignedAccountsById(int id)
        {
            var result = _dbContext.ApproverAssignments
                        .Include(a => a.User)
                        .Include(a => a.Approver)
                        .Include(a => a.User.Company)
                        .Include(a => a.User.Branch)
                        .Include(a => a.User.Department)
                        .Include(a => a.User.Position)
                        .Where(a => a.Approver.Id == id && a.IsDeleted == false);
            return result;
        }

        public IEnumerable<Account> GetAssignmentAccountByApproverId(int id)
        {
            var result = _dbContext.ApproverAssignments
                          .Include(a => a.User)
                          .Include(a => a.Approver)
                          .Include(a => a.User.Company)
                          .Include(a => a.User.Branch)
                          .Include(a => a.User.Department)
                          .Include(a => a.User.Position)
                          .Where(a => a.Approver.Id == id && a.IsDeleted == false)
                          .Select(a => new Account
                          {
                              Id = a.User.Id,
                              FirstName = a.User.FirstName,
                              LastName = a.User.LastName,
                              Company = a.User.Company,
                              Branch = a.User.Branch,
                              Department = a.User.Department,
                              Position = a.User.Position,
                          }).ToList();
            return result;
        }

        public string GetNameById(int id)
        {
            var item = _dbContext.Accounts.FirstOrDefault(a => a.Id == id);
            var name = item.LastName + ", " + item.FirstName;
            return name;
        }

        public void RemoveById(int id)
        {
            var item = _dbContext.ApproverAssignments.FirstOrDefault(a => a.Id == id);
            item.IsDeleted = true;
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
