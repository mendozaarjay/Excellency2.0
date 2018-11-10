using Excellency.Interfaces;
using Excellency.Models;
using Excellency.Persistence;
using Excellency.Secured;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.Services
{
    public class HomeService : IHome
    {
        private EASDbContext _dbContext;

        public HomeService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Account GetAccountById(int id)
        {
            return _dbContext.Accounts.FirstOrDefault(a => a.Id == id);
        }

        public string GetUserId(Account account)
        {
            var item = _dbContext.Accounts.FirstOrDefault(a => a.Username == account.Username && a.Password == account.Password).Id.ToString();
            return item;
        }

        public string GetUserNameById(int id)
        {
            var item = _dbContext.Accounts.FirstOrDefault(a => a.Id == id);
            return item.FirstName + " " + item.LastName;
        }

        public bool IsAccountLocked(Account account)
        {
            var isLocked = _dbContext.Accounts.Any(a => a.Username == account.Username && a.Password == a.Password && a.IsLocked == true);
            return isLocked;
        }

        public bool IsAdministrator(int UserId)
        {
            var isAdmin = _dbContext.AccountRoleAssignments
                .Include(a => a.Role)
                .Include(a => a.Account)
                .Any(a => a.Account.Id == UserId && a.IsDeleted == false && a.Role.Type == AccountType.Administrator.ToInt() && a.Role.IsDeleted == false);
            return isAdmin;
        }

        public bool IsApprover(int UserId)
        {
            var isApprover = _dbContext.AccountRoleAssignments
           .Include(a => a.Role)
           .Include(a => a.Account)
           .Any(a => a.Account.Id == UserId && a.IsDeleted == false && a.Role.Type == AccountType.Approver.ToInt() && a.Role.IsDeleted == false);
            return isApprover;
        }

        public bool IsAvailableToLogin(Account account)
        {
            return _dbContext.Accounts.Any(a => a.Username == account.Username && a.Password == account.Password);
        }

        public bool IsLoginExpired(Account account)
        {
            return _dbContext.Accounts.Any(a => a.Username == account.Username && a.IsExpired == true);
        }

        public bool IsNotExist(Account account)
        {
            if (IsAvailableToLogin(account))
                return false;
            else
                return true;
        }

        public bool IsRater(int UserId)
        {
            var isRater = _dbContext.AccountRoleAssignments
           .Include(a => a.Role)
           .Include(a => a.Account)
           .Any(a => a.Account.Id == UserId && a.IsDeleted == false && a.Role.Type == AccountType.Rater.ToInt() && a.Role.IsDeleted == false);
            return isRater;
        }

        public bool IsUserAlreadyExist(Account account)
        {
            return _dbContext.Accounts.Any(a => a.Username == account.Username || a.Email == account.Email);
        }
        
        public UserAccess UserAccess(int UserId)
        {
            var _isAdmin = IsAdministrator(UserId);
            var _isRater = IsRater(UserId);
            var _isApprover = IsApprover(UserId);
            var access = new UserAccess
            {
                Dashboard = true,
                //Maitenance
                Company = _isAdmin,
                Branch = _isAdmin,
                Department = _isAdmin,
                Position = _isAdmin,
                EmployeeCategory = _isAdmin,
                //Settings
                Ratings = (_isRater|| _isAdmin),
                RatingTable = (_isRater || _isAdmin),
                KeyResultArea = (_isRater || _isAdmin),
                BehavioralKRA = (_isRater || _isAdmin),
                EmployeeKRAAssignment = (_isRater || _isAdmin),
                //Accounts
                Users = _isAdmin,
                UserRoles = _isAdmin,
                ApproverAssignment = _isAdmin,
                //Employees
                Employee = _isAdmin,
                RaterAssignment = _isAdmin,
                //Evaluation
                CreateEvaluation =_isRater,
                Approval = _isApprover,
            };
            if(access.Company || access.Branch || access.Department || access.Position || access.EmployeeCategory)
            {
                access.Maintenance = true;
            }
            if(access.Ratings || access.RatingTable || access.KeyResultArea || access.BehavioralKRA || access.EmployeeKRAAssignment)
            {
                access.Settings = true;
            }
            if(access.Users || access.UserRoles || access.ApproverAssignment)
            {
                access.Accounts = true;
            }
            if(access.Employee || access.RaterAssignment)
            {
                access.Employees = true;
            }
            if(access.CreateEvaluation || access.Approval)
            {
                access.Evaluation = true;
            }

            return access;
        }
    }
}
