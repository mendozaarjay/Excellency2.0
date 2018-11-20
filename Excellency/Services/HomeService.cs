using Excellency.Dashboard;
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
                PeerCriteria = (_isRater || _isAdmin),
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

        #region Dashboard
        public DashboardAccess DashboardAccessPerUser(int userid)
        {
            var _isAdmin = IsAdministrator(userid);
            var _isRater = IsRater(userid);
            var _isApprover = IsApprover(userid);
            var item = new DashboardAccess
            {
                IsAdministrator = _isAdmin,
                IsRater = _isRater,
                IsApprover = _isApprover
            };

            return item;
        }
        #region Administrator Dashboard
        public IEnumerable<Account> Accounts()
        {
            return _dbContext.Accounts
                .Include(a => a.Company)
                .Include(a => a.Branch)
                .Include(a => a.Department)
                .Include(a => a.Position)
                .Where(a => a.IsDeleted == false && a.IsDeactivated == false);
        }
        public IEnumerable<DataPoint> AccountPerCompany()
        {
            var result = _dbContext.Accounts
                        .Include(a => a.Company)
                        .Where(a => a.IsDeactivated == false && a.IsDeleted == false)
                        .GroupBy(a => a.Company)
                        .Select(x => new { label = x.Key.Description, count = x.Count() });
            var _dataPoints = new List<DataPoint>();
            foreach (var item in result)
            {
                var point = new DataPoint(item.count, item.label);
                _dataPoints.Add(point);
            }
            return _dataPoints;
        }
        public IEnumerable<DataPoint> EmployeePerCompany()
        {
            var result = _dbContext.Employees
                .Include(a => a.Company)
                .Where(a => a.IsDeleted == false)
                .GroupBy(a => a.Company)
                .Select(a => new { label = a.Key.Description, count = a.Count() });
            var _dataPoints = new List<DataPoint>();
            foreach (var item in result)
            {
                var point = new DataPoint(item.count, item.label);
                _dataPoints.Add(point);
            }
            return _dataPoints;
        } 
        #endregion

        //Rater
        #region Rater Dashboard
        public DataPoint RatedEmployees(int userid)
        {
            var header = _dbContext.RaterHeader
                .Include(a => a.Rater)
                .FirstOrDefault(a => a.Rater.Id == userid && a.IsDeleted == false);
            var assigned = _dbContext.RaterLine
                .Include(a => a.EmployeeRater)
                .Include(a => a.Employee)
                .Where(a => a.EmployeeRater.Id == header.Id && a.IsDeleted == false)
                .Select(a => new { employee = a.Employee.Id });
            var total = assigned.Count();
            int count = 0;
            foreach (var item in assigned)
            {
                var isExist = _dbContext.EvaluationHeader
                    .Include(a => a.Ratee)
                    .Include(a => a.Rater)
                    .Any(a => a.Rater.Id == header.Rater.Id && a.Ratee.Id == item.employee && a.IsDeleted == false);
                if (isExist)
                {
                    count++;
                }
            }
            return new DataPoint(total, count, "Rated Employees");
        }
        public IEnumerable<EmployeePerRaterViewModel> EmployeesPerRater(int userid)
        {
            var headerid = _dbContext.RaterHeader
                .Include(a => a.Rater)
                .FirstOrDefault(a => a.Rater.Id == userid && a.IsDeleted == false).Id;

            var result = _dbContext.RaterLine
               .Include(a => a.EmployeeRater)
               .Include(a => a.Employee)
               .Include(a => a.Employee.Company)
               .Include(a => a.Employee.Branch)
               .Include(a => a.Employee.Department)
               .Include(a => a.Employee.Position)
               .Where(a => a.IsDeleted == false && a.EmployeeRater.Id == headerid);
            var items = result.Select(a => new EmployeePerRaterViewModel
            {
                Id = a.Employee.Id,
                Name = a.Employee.FirstName + " " + a.Employee.MiddleName + " " + a.Employee.LastName,
                IsRated = IsEvaluated(userid, a.Employee.Id)
            }).ToList();
            return items;
        }
        public IEnumerable<EvaluationCriteria> BehavioralStrength(int employeeid, int userid)
        {
            var result = _dbContext.RatingBehavioralFactors
                .Include(a => a.RatingHeader)
                .Include(a => a.RatingHeader.Rater)
                .Include(a => a.RatingHeader.Ratee)
                .Include(a => a.BehavioralFactorItem)
                .Where(a => a.RatingHeader.Ratee.Id == employeeid && a.RatingHeader.Rater.Id == userid && a.RatingHeader.IsExpired == false);
            var items = result.Select(a => new { _Title = a.BehavioralFactorItem.Description, _Weight = a.BehavioralFactorItem.Weight, _Score = a.Score, _Difference = a.BehavioralFactorItem.Weight - a.Score })
                .ToList()
                .OrderBy(a => a._Difference);

            var strengths = items.Take(5).Where(a => a._Difference <= 0)
                .Select(a => new EvaluationCriteria
                {
                    Title = a._Title,
                    Description = string.Empty,
                    Weight = a._Weight,
                    Score = a._Score
                }).ToList();
            return strengths;
        }
        public IEnumerable<EvaluationCriteria> BehavioralWeakness(int employeeid, int userid)
        {
            var result = _dbContext.RatingBehavioralFactors
                .Include(a => a.RatingHeader)
                .Include(a => a.RatingHeader.Rater)
                .Include(a => a.RatingHeader.Ratee)
                .Include(a => a.BehavioralFactorItem)
                .Where(a => a.RatingHeader.Ratee.Id == employeeid && a.RatingHeader.Rater.Id == userid && a.RatingHeader.IsExpired == false);
            var items = result.Select(a => new { _Title = a.BehavioralFactorItem.Description, _Weight = a.BehavioralFactorItem.Weight, _Score = a.Score, _Difference = a.BehavioralFactorItem.Weight - a.Score })
                .ToList()
                .OrderByDescending(a => a._Difference);

            var weakness = items.Take(5).Where(a => a._Difference <= 0)
                .Select(a => new EvaluationCriteria
                {
                    Title = a._Title,
                    Description = string.Empty,
                    Weight = a._Weight,
                    Score = a._Score
                }).ToList();
            return weakness;
        }
        public IEnumerable<EvaluationCriteria> KeyResultAreaStrength(int employeeid, int userid)
        {
            var result = _dbContext.RatingKeySuccessAreas
                .Include(a => a.RatingHeader)
                .Include(a => a.RatingHeader.Rater)
                .Include(a => a.RatingHeader.Ratee)
                .Include(a => a.KeySuccessIndicator)
                .Where(a => a.RatingHeader.Ratee.Id == employeeid && a.RatingHeader.Rater.Id == userid && a.RatingHeader.IsExpired == false);
            var items = result.Select(a => new { _Title = a.KeySuccessIndicator.Title, _Description = a.KeySuccessIndicator.Description, _Weight = a.KeySuccessIndicator.Weight, _Score = a.Score, _Difference = a.KeySuccessIndicator.Weight - a.Score })
                .ToList()
                .OrderBy(a => a._Difference);

            var strengths = items.Take(5).Where(a => a._Difference <= 0)
                .Select(a => new EvaluationCriteria
                {
                    Title = a._Title,
                    Description = a._Description,
                    Weight = a._Weight,
                    Score = a._Score
                }).ToList();
            return strengths;
        }
        public IEnumerable<EvaluationCriteria> KeyResultAreaWeakness(int employeeid, int userid)
        {
            var result = _dbContext.RatingKeySuccessAreas
                .Include(a => a.RatingHeader)
                .Include(a => a.RatingHeader.Rater)
                .Include(a => a.RatingHeader.Ratee)
                .Include(a => a.KeySuccessIndicator)
                .Where(a => a.RatingHeader.Ratee.Id == employeeid && a.RatingHeader.Rater.Id == userid && a.RatingHeader.IsExpired == false);
            var items = result.Select(a => new { _Title = a.KeySuccessIndicator.Title, _Description = a.KeySuccessIndicator.Description, _Weight = a.KeySuccessIndicator.Weight, _Score = a.Score, _Difference = a.KeySuccessIndicator.Weight - a.Score })
                .ToList()
                .OrderByDescending(a => a._Difference);

            var weakness = items.Take(5).Where(a => a._Difference <= 0)
                .Select(a => new EvaluationCriteria
                {
                    Title = a._Title,
                    Description = a._Description,
                    Weight = a._Weight,
                    Score = a._Score
                }).ToList();
            return weakness;
        }
        private bool IsEvaluated(int userid, int rateeid)
        {
            var isEvaluated = _dbContext.RatingHeader
                .Include(a => a.Ratee)
                .Include(a => a.Rater)
                .Any(a => a.Rater.Id == userid && a.Ratee.Id == rateeid && a.IsDeleted == false && a.IsExpired == false);
            return isEvaluated;
        }
        #endregion
        #region Approver Dashboard
        public DataPoint ApprovedEvaluation(int userid)
        {
            var AssignedUser = _dbContext.ApproverAssignments
                .Include(a => a.User)
                .Include(a => a.Approver)
                .Where(a => a.Approver.Id == userid && a.IsDeleted == false)
                .Select(a => a.User.Id);

            var ratings = _dbContext.RatingHeader
                .Include(a => a.Rater)
                .Include(a => a.Ratee)
                .Include(a => a.Status)
                .Include(a => a.Type)
                .Where(a => AssignedUser.Contains(a.Rater.Id) && a.IsExpired == false);
            var count = ratings.Count();
            var approved = ratings.Where(a => a.Status.Id == TransactionStatus.Approved.ToInt()).Count();
            var pending = ratings.Where(a => a.Status.Id == TransactionStatus.ForApproval.ToInt()).Count();
            return new DataPoint(count, approved, pending, "Approved Evalation");
        }
        public IEnumerable<UserPerApprover> ApproverAssignedUser(int userid)
        {
            var AssignedUser = _dbContext.ApproverAssignments
              .Include(a => a.User)
              .Include(a => a.Approver)
              .Where(a => a.Approver.Id == userid && a.IsDeleted == false)
              .Select(a => new UserPerApprover
              {
                  Id = a.User.Id,
                  Name = a.User.FirstName + " " + a.User.LastName
              }).ToList();
            return AssignedUser;
        }
        #endregion
        #endregion
    }
}
