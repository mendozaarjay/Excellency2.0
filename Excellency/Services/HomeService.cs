using Excellency.Dashboard;
using Excellency.Interfaces;
using Excellency.Models;
using Excellency.Persistence;
using Excellency.Secured;
using Excellency.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.Services
{
    public class HomeService : IHome
    {
        private EASDbContext _dbContext;

        private string UserConnectionString { get; }

        public HomeService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
           UserConnectionString = _dbContext.Database.GetDbConnection().ConnectionString;
        }

        public Account GetAccountById(int id)
        {
            return _dbContext.Accounts.FirstOrDefault(a => a.Id == id);
        }
        #region User Account Access

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
                Ratings = (_isRater || _isAdmin),
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
                CreateEvaluation = _isRater,
                Approval = _isApprover,
            };
            if (access.Company || access.Branch || access.Department || access.Position || access.EmployeeCategory)
            {
                access.Maintenance = true;
            }
            if (access.Ratings || access.RatingTable || access.KeyResultArea || access.BehavioralKRA || access.EmployeeKRAAssignment)
            {
                access.Settings = true;
            }
            if (access.Users || access.UserRoles || access.ApproverAssignment)
            {
                access.Accounts = true;
            }
            if (access.Employee || access.RaterAssignment)
            {
                access.Employees = true;
            }
            if (access.CreateEvaluation || access.Approval)
            {
                access.Evaluation = true;
            }
            var isWithEvaluation = SCObjects.ReturnText(string.Format("SELECT [dbo].[fnIsWithEvaluation]({0})", UserId.ToString()), UserConnectionString);
            access.IsWithEvaluation = isWithEvaluation.Length > 0;

            return access;
        }

        #endregion
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
            var isWithEvaluation = SCObjects.ReturnText(string.Format("SELECT [dbo].[fnIsWithEvaluation]({0})", userid.ToString()), UserConnectionString);
            item.IsEmployee = isWithEvaluation.Length > 0;
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
        public IEnumerable<ChartPoint> AccountPerCompany()
        {
            var companies = _dbContext.Companies.Where(a => a.IsDeleted == false);
            var _dataPoints = new List<ChartPoint>();
            foreach (var item in companies)
            {
                var point = new ChartPoint{
                  X = GetAccountPerCompany(item.Id).ToString(),
                  Y = item.Description
                };
                _dataPoints.Add(point);
            }
            return _dataPoints;
        }
        private int GetAccountPerCompany(int id)
        {
            var count = _dbContext.Accounts
                .Include(a => a.Company)
                .Where(a => a.Company.Id == id).Count();
            return count;
        }
        public IEnumerable<DataPoint> EmployeePerCompany()
        {
            var result = _dbContext.Accounts
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


        public int UserCount()
        {
            var item = _dbContext.Accounts.Where(a => a.IsDeactivated == false && a.IsDeleted == false).Count();
            return item;
        }
        public int RaterCount()
        {
            var item = _dbContext.RaterHeader
                .Include(a => a.Rater)
                .Where(a => a.IsDeleted == false)
                .Select(a => a.Rater.Id)
                .Distinct().Count();
            return item;
        }
        public int ApproverCount()
        {
            var item = _dbContext.ApproverAssignments
              .Include(a => a.User)
              .Include(a => a.Approver)
              .Where(a => a.IsDeleted == false)
              .Select(a => a.Approver)
              .Distinct()
              .Count();
            return item;
        }
        public int EmployeeCount()
        {
            var item = _dbContext.Accounts.Where(a => a.IsDeleted == false).Count();
            return item;
        }
        public string AccountPeriod()
        {
            var start = _dbContext.Accounts.Where(a => a.IsDeleted == false).OrderBy(a => a.CreationDate).Select(a => a.CreationDate).Take(1);
            var end = _dbContext.Accounts.Where(a => a.IsDeleted == false).OrderByDescending(a => a.CreationDate).Select(a => a.CreationDate).Take(1);
            var period = "Period : " + start.First().ToString("MM/dd/yyyy") + " - " + end.First().ToString("MM/dd/yyyy");
            return period;
        }


        public IEnumerable<Account> MostRecentAccounts()
        {
            var items = _dbContext.Accounts.Where(a => a.IsDeleted == false).OrderByDescending(a => a.CreationDate).Take(5);
            return items;
        }
        public IEnumerable<Account> MostRecentEmployees()
        {
            var items = _dbContext.Accounts.Where(a => a.IsDeleted == false).OrderByDescending(a => a.CreationDate).Take(5);
            return items;
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

        public int AssignedRateeCount(int userid)
        {
            try
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
                return result.Select(a => a.Employee.Id).Distinct().Count();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int EvaluatedRateeCount(int userid)
        {
            var item = _dbContext.RatingHeader
                .Include(a => a.Rater)
                .Include(a => a.Ratee)
                .Where(a => a.Rater.Id == userid && a.IsExpired == false)
                .Select(a => a.Ratee.Id ).Distinct().Count();
            return item;
        }
        public int ApprovedRatingCount(int userid)
        {
            var item = _dbContext.RatingHeader
                .Include(a => a.Rater)
                .Include(a => a.Ratee)
                .Include(a => a.Status)
                .Where(a => a.Rater.Id == userid && a.IsExpired == false && a.Status.Id == TransactionStatus.Approved.ToInt())
                .Select(a => a.Rater.Id).Distinct().Count();
            return item;
        }
        public int PendingRatingCount(int userid)
        {
            var item = _dbContext.RatingHeader
                .Include(a => a.Rater)
                .Include(a => a.Ratee)
                .Include(a => a.Status)
                .Where(a => a.Rater.Id == userid && a.IsExpired == false && a.Status.Id != TransactionStatus.Approved.ToInt())
                .Select(a => a.Rater.Id).Distinct().Count();
            return item;
        }
        public IEnumerable<EmployeePerRaterViewModel> EmployeesPerRater(int userid)
        {
            try
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
                var employees = result.Select(a => new EmployeePerRaterViewModel
                {
                    Id = a.Employee.Id,
                    Name = a.Employee.FirstName + " " + a.Employee.MiddleName + " " + a.Employee.LastName,
                    IsRated = false
                }).ToList();

                var _returnThis = new List<EmployeePerRaterViewModel>();
                foreach (var item in employees)
                {
                    item.IsRated = IsEvaluated(userid, item.Id);
                    _returnThis.Add(item);
                }
                return _returnThis;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string EmployeeNameById(int id)
        {
            var item = _dbContext.Accounts.FirstOrDefault(a => a.Id == id);
            var name = item.FirstName + " " + item.LastName;
            return name;
        }
        public int EmployeeTotalScore(int id)
        {
            var behavioralid = _dbContext.RatingHeader
                .Include(a => a.Ratee)
                .Where(a => a.Ratee.Id == id && a.IsExpired == false && a.Type == "behavioral" && a.Status.Id == TransactionStatus.Approved.ToInt())
                .Select(a => a.Id);
            var kraid = _dbContext.RatingHeader
               .Include(a => a.Ratee)
               .Where(a => a.Ratee.Id == id && a.IsExpired == false && a.Type == "kra" && a.Status.Id == TransactionStatus.Approved.ToInt())
               .Select(a => a.Id);
            var behavioraltotal = _dbContext.RatingBehavioralFactors
                .Include(a => a.RatingHeader)
                .Where(a => behavioralid.Contains(a.RatingHeader.Id))
                .Sum(a => a.Score);
            var kratotal = _dbContext.RatingKeySuccessAreas
                .Include(a => a.RatingHeader)
                .Where(a => kraid.Contains(a.RatingHeader.Id))
                .Sum(a => a.Score);
            return behavioraltotal + kratotal;

        }
        public IEnumerable<EvaluationCriteria> BehavioralStrength(int employeeid)
        {
            var behavioralid = _dbContext.RatingHeader
                .Include(a => a.Ratee)
                .Where(a => a.Ratee.Id == employeeid && a.IsExpired == false && a.Type == "behavioral" && a.Status.Id == TransactionStatus.Approved.ToInt())
                .Select(a => a.Id);

            var result = _dbContext.RatingBehavioralFactors
                .Include(a => a.RatingHeader)
                .Include(a => a.RatingHeader.Rater)
                .Include(a => a.RatingHeader.Ratee)
                .Include(a => a.BehavioralFactorItem)
                .Where(a => behavioralid.Contains(a.RatingHeader.Id));

            var items = result.Select(a => new { _Title = a.BehavioralFactorItem.Description, _Weight = a.BehavioralFactorItem.Weight, _Score = a.Score, _Difference = a.BehavioralFactorItem.Weight - a.Score })
                .ToList()
                .OrderBy(a => a._Difference);

            var strengths = items.Take(5)
                .Select(a => new EvaluationCriteria
                {
                    Title = a._Title,
                    Description = string.Empty,
                    Weight = a._Weight,
                    Score = a._Score,
                    Percentage = decimal.Parse(a._Score.ToString()) / decimal.Parse(a._Weight.ToString()),
                }).ToList();
            return strengths;
        }
        public IEnumerable<EvaluationCriteria> BehavioralWeakness(int employeeid)
        {
            var behavioralid = _dbContext.RatingHeader
                .Include(a => a.Ratee)
                .Where(a => a.Ratee.Id == employeeid && a.IsExpired == false && a.Type == "behavioral" && a.Status.Id == TransactionStatus.Approved.ToInt())
                .Select(a => a.Id);

            var result = _dbContext.RatingBehavioralFactors
                .Include(a => a.RatingHeader)
                .Include(a => a.RatingHeader.Rater)
                .Include(a => a.RatingHeader.Ratee)
                .Include(a => a.BehavioralFactorItem)
                .Where(a => behavioralid.Contains(a.RatingHeader.Id));

            var items = result.Select(a => new { _Title = a.BehavioralFactorItem.Description, _Weight = a.BehavioralFactorItem.Weight, _Score = a.Score, _Difference = a.BehavioralFactorItem.Weight - a.Score })
                .ToList()
                .OrderByDescending(a => a._Difference);

            var weakness = items.Take(5)
                .Select(a => new EvaluationCriteria
                {
                    Title = a._Title,
                    Description = string.Empty,
                    Weight = a._Weight,
                    Score = a._Score,
                    Percentage = decimal.Parse(a._Score.ToString()) / decimal.Parse(a._Weight.ToString()),
                }).ToList();
            return weakness;
        }
        public IEnumerable<EvaluationCriteria> KeyResultAreaStrength(int employeeid)
        {
            var kraid = _dbContext.RatingHeader
               .Include(a => a.Ratee)
               .Where(a => a.Ratee.Id == employeeid && a.IsExpired == false && a.Type == "kra" && a.Status.Id == TransactionStatus.Approved.ToInt())
               .Select(a => a.Id);
            var result = _dbContext.RatingKeySuccessAreas
                .Include(a => a.RatingHeader)
                .Include(a => a.RatingHeader.Rater)
                .Include(a => a.RatingHeader.Ratee)
                .Include(a => a.KeySuccessIndicator)
                .Where(a => kraid.Contains(a.RatingHeader.Id));
            var items = result.Select(a => new { _Title = a.KeySuccessIndicator.Title, _Description = a.KeySuccessIndicator.Description, _Weight = a.KeySuccessIndicator.Weight, _Score = a.Score, _Difference = a.KeySuccessIndicator.Weight - a.Score })
                .ToList()
                .OrderBy(a => a._Difference);

            var strengths = items.Take(5)
                .Select(a => new EvaluationCriteria
                {
                    Title = a._Title,
                    Description = a._Description,
                    Weight = a._Weight,
                    Score = a._Score,
                    Percentage = decimal.Parse(a._Score.ToString()) / decimal.Parse(a._Weight.ToString()),
                }).ToList();
            return strengths;
        }
        public IEnumerable<EvaluationCriteria> KeyResultAreaWeakness(int employeeid)
        {
            var kraid = _dbContext.RatingHeader
               .Include(a => a.Ratee)
               .Where(a => a.Ratee.Id == employeeid && a.IsExpired == false && a.Type == "kra" && a.Status.Id == TransactionStatus.Approved.ToInt())
               .Select(a => a.Id);
            var result = _dbContext.RatingKeySuccessAreas
                .Include(a => a.RatingHeader)
                .Include(a => a.RatingHeader.Rater)
                .Include(a => a.RatingHeader.Ratee)
                .Include(a => a.KeySuccessIndicator)
                .Where(a => kraid.Contains(a.RatingHeader.Id));
            var items = result.Select(a => new { _Title = a.KeySuccessIndicator.Title, _Description = a.KeySuccessIndicator.Description, _Weight = a.KeySuccessIndicator.Weight, _Score = a.Score, _Difference = a.KeySuccessIndicator.Weight - a.Score })
                .ToList()
                .OrderByDescending(a => a._Difference);

            var weakness = items.Take(5)
                .Select(a => new EvaluationCriteria
                {
                    Title = a._Title,
                    Description = a._Description,
                    Weight = a._Weight,
                    Score = a._Score,
                    Percentage = decimal.Parse(a._Score.ToString()) / decimal.Parse(a._Weight.ToString()),
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

        public int AssignedPerApprover(int userid)
        {
            var item = _dbContext.ApproverAssignments
              .Include(a => a.User)
              .Include(a => a.Approver)
              .Where(a => a.Approver.Id == userid && a.IsDeleted == false)
              .Select(a => a.User.Id)
              .Distinct()
              .Count();
            return item;
        }
        public int ApprovedEvaluation(int userid)
        {
            var users = _dbContext.ApproverAssignments
              .Include(a => a.User)
              .Include(a => a.Approver)
              .Where(a => a.Approver.Id == userid && a.IsDeleted == false)
              .Select(a => a.User.Id)
              .Distinct();
            var item = _dbContext.RatingHeader
                .Include(a => a.Rater)
                .Include(a => a.Status)
                .Where(a => a.IsExpired == false && a.Status.Id == TransactionStatus.Approved.ToInt() && users.Contains(a.Rater.Id))
                .Distinct()
                .Count();
            return item;
        }
        public int PendingForApproval(int userid)
        {
            var users = _dbContext.ApproverAssignments
              .Include(a => a.User)
              .Include(a => a.Approver)
              .Where(a => a.Approver.Id == userid && a.IsDeleted == false)
              .Select(a => a.User.Id)
              .Distinct();
            var item = _dbContext.RatingHeader
                .Include(a => a.Rater)
                .Include(a => a.Status)
                .Where(a => a.IsExpired == false && a.Status.Id != TransactionStatus.Approved.ToInt() && users.Contains(a.Rater.Id))
                .Distinct()
                .Count();
            return item;
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


        #region Employee Evaluation


        public IEnumerable<EvaluationHeaderItem> GetBehavioralEvaluations(int id)
        {
            List<EvaluationHeaderItem> items = new List<EvaluationHeaderItem>();

            string sql = string.Format("SELECT * FROM  [dbo].[fnGetBehavioralEvaluation]({0}) [x]", id.ToString());
            DataTable dt = SCObjects.LoadDataTable(sql, UserConnectionString);
            if(dt != null)
            {
                if(dt.Rows.Count > 0)
                {
                    foreach(DataRow dr in dt.Rows)
                    {
                        var item = new EvaluationHeaderItem
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            Type = dr["Type"].ToString(),
                            Name = dr["EmployeeName"].ToString(),
                            Rater = dr["Rater"].ToString(),
                            DateRated = dr["DateRated"].ToString(),
                            Status = dr["Status"].ToString(),
                            Approver = dr["Approver"].ToString(),
                            ApprovalDate = dr["ApprovedDate"].ToString(),
                            ApproverRemarks = dr["ApproverRemarks"].ToString(),
                            Title = dr["Title"].ToString(),
                            Description = dr["Description"].ToString(),
                            Weight = decimal.Parse(dr["Weight"].ToString()),
                        };
                        items.Add(item);
                    }
                }
            }

            return items;
        }
        public IEnumerable<EvaluationHeaderItem> GetKRAEvaluations(int id)
        {
            List<EvaluationHeaderItem> items = new List<EvaluationHeaderItem>();
            string sql = string.Format("SELECT * FROM  [dbo].[fnGetKRAEvaluation]({0}) [x]", id.ToString());
            DataTable dt = SCObjects.LoadDataTable(sql, UserConnectionString);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        var item = new EvaluationHeaderItem
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            Type = dr["Type"].ToString(),
                            Name = dr["EmployeeName"].ToString(),
                            Rater = dr["Rater"].ToString(),
                            DateRated = dr["DateRated"].ToString(),
                            Status = dr["Status"].ToString(),
                            Approver = dr["Approver"].ToString(),
                            ApprovalDate = dr["ApprovedDate"].ToString(),
                            ApproverRemarks = dr["ApproverRemarks"].ToString(),
                            Title = dr["Title"].ToString(),
                            Description = dr["Description"].ToString(),
                            Weight = decimal.Parse(dr["Weight"].ToString()),
                        };
                        items.Add(item);
                    }
                }
            }

            return items;
        }
        public List<EvaluationLineItem> GetBehavioralLineItems(int headerid)
        {
            List<EvaluationLineItem> items = new List<EvaluationLineItem>();
            string sql = string.Format("SELECT * FROM [dbo].[fnBehavioralSummaryPerRating]({0}) [x]", headerid.ToString());
            DataTable dt = SCObjects.LoadDataTable(sql, UserConnectionString);
            if(dt != null)
            {
                if(dt.Rows.Count > 0)
                {
                    foreach(DataRow dr in dt.Rows)
                    {
                        var item = new EvaluationLineItem
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            HeaderId = int.Parse(dr["RatingHeaderId"].ToString()),
                            Title = dr["Title"].ToString(),
                            Description = dr["Description"].ToString(),
                            Weight = decimal.Parse(dr["Weight"].ToString()),
                            Score = decimal.Parse(dr["Score"].ToString()),
                            Comment = dr["Comment"].ToString()
                        };
                        items.Add(item);
                    }
                }
            }
            return items;
        }
        public List<EvaluationLineItem> GetKRAEvaluationLineItems(int headerid)
        {
            List<EvaluationLineItem> items = new List<EvaluationLineItem>();
            string sql = string.Format("SELECT * FROM [dbo].[fnKRASummaryPerRating]({0}) [x]", headerid.ToString());
            DataTable dt = SCObjects.LoadDataTable(sql, UserConnectionString);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        var item = new EvaluationLineItem
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            HeaderId = int.Parse(dr["RatingHeaderId"].ToString()),
                            Title = dr["Title"].ToString(),
                            Description = dr["Description"].ToString(),
                            Weight = decimal.Parse(dr["Weight"].ToString()),
                            Score = decimal.Parse(dr["Score"].ToString()),
                            Comment = dr["Comment"].ToString()
                        };
                        items.Add(item);
                    }
                }
            }
            return items;
        }
        #endregion
        #endregion
    }
}
