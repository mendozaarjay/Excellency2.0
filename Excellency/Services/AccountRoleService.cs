using Excellency.Interfaces;
using Excellency.Models;
using Excellency.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Excellency.Services
{
    public class AccountRoleService : IAccountRole
    {
        private EASDbContext _dbContext;

        public AccountRoleService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddAccount(int RoleId, List<int> Accounts)
        {
            var header = _dbContext.AccountRoles.FirstOrDefault(a => a.Id == RoleId);

            foreach (var item in Accounts)
            {
                var account = _dbContext.Accounts.FirstOrDefault(a => a.Id == item);
                var lineitem = new AccountRoleAssignment
                {
                    Role = header,
                    Account = account,
                    IsDeleted = false,
                };
                _dbContext.Add(lineitem);
            }
            _dbContext.SaveChanges();
        }

        public void AddRole(AccountRole role, int UserId)
        {
            role.CreatedBy = UserId.ToString();
            role.CreationDate = DateTime.Now;
            _dbContext.Add(role);
            _dbContext.SaveChanges();
        }

        public AccountRoleAssignment GetAccountAssignmentById(int id)
        {
            var item = _dbContext.AccountRoleAssignments.FirstOrDefault(a => a.Id == id);
            return item;
        }

        public Account GetAccountById(int id)
        {
            var item = _dbContext.Accounts.FirstOrDefault(a => a.Id == id);
            return item;
        }

        public IEnumerable<Account> GetAllAccount()
        {
            var items = _dbContext.Accounts.Where(a => a.IsDeactivated == false && a.IsDeleted == false);
            return items;
        }

        public IEnumerable<AccountRole> GetAllRoles()
        {
            var isAdminExist = _dbContext.AccountRoles.Any(a => a.Type == AccountType.Administrator.ToInt() && a.IsDeleted == false);
            if (!isAdminExist)
            {
                var admin = new AccountRole
                {
                    Description = "Administrator",
                    Type = AccountType.Administrator.ToInt(),
                    CreationDate = DateTime.Now,
                    IsDeleted = false,
                };
                _dbContext.Add(admin);
                _dbContext.SaveChanges();
            }
            var isApproverExist = _dbContext.AccountRoles.Any(a => a.Type == AccountType.Approver.ToInt() && a.IsDeleted == false);
            if (!isApproverExist)
            {
                var approver = new AccountRole
                {
                    Description = "Approver",
                    Type = AccountType.Approver.ToInt(),
                    CreationDate = DateTime.Now,
                    IsDeleted = false,
                };
                _dbContext.Add(approver);
                _dbContext.SaveChanges();
            }
            var isRaterExist = _dbContext.AccountRoles.Any(a => a.Type == AccountType.Rater.ToInt() && a.IsDeleted == false);
            if (!isRaterExist)
            {
                var rater = new AccountRole
                {
                    Description = "Rater",
                    Type = AccountType.Rater.ToInt(),
                    CreationDate = DateTime.Now,
                    IsDeleted = false,
                };
                _dbContext.Add(rater);
                _dbContext.SaveChanges();
            }

            var items = _dbContext.AccountRoles.Where(a => a.IsDeleted == false);
            return items;

        }

        public IEnumerable<AccountRoleAssignment> GetAssignmentsByHeaderId(int id)
        {
            var items = _dbContext.AccountRoleAssignments
            .Include(a => a.Role)
            .Include(a => a.Account)
            .Where(a => a.Role.Id == id && a.IsDeleted == false);
            return items;
        }

        public AccountRole GetRoleById(int id)
        {
            var item = _dbContext.AccountRoles.FirstOrDefault(a => a.Id == id);
            return item;
        }

        public void RemoveAccount(int id)
        {
            var item = _dbContext.AccountRoles.FirstOrDefault(a => a.Id == id);
            item.IsDeleted = false;
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void RemoveAccountAssignment(int id)
        {
            var item = _dbContext.AccountRoleAssignments.FirstOrDefault(a => a.Id == id);
            item.IsDeleted = false;
            _dbContext.SaveChanges();
        }

        public void UpdateRole(AccountRole role, int UserId)
        {
            role.ModifiedBy = UserId.ToString();
            role.ModifiedDate = DateTime.Now;
            _dbContext.Entry(role).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
