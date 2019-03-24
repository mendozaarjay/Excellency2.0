using Excellency.Interfaces;
using Excellency.Models;
using Excellency.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.Services
{
    public class EvaluationService : IEvaluation
    {
        private EASDbContext _dbContext;

        public string UserConnectionString { get; }

        public EvaluationService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
            UserConnectionString = dbContext.Database.GetDbConnection().ConnectionString;
        }

        public void Approve(EvaluationHeader header, Account account)
        {
            header.Approver = account;
            header.ApprovedDate = DateTime.Now;
            header.Status = GetStatusPerId(TransactionStatus.Approved.ToInt());
            _dbContext.Entry(header).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public IEnumerable<Account> AssignedEmployees(int UserId)
        {
            var result = _dbContext.RaterLine.Include(a => a.EmployeeRater)
                .Include(a => a.EmployeeRater.Rater)
                .Include(a => a.Employee)
                .Include(a => a.Employee.Branch)
                .Include(a => a.Employee.Company)
                .Include(a => a.Employee.Department)
                .Include(a => a.Employee.Position)
                .Include(a => a.Employee.Category)
                .Where(a => a.EmployeeRater.Rater.Id == UserId && a.IsDeleted == false)
                .Select(a => new Account
                {
                    Id = a.Employee.Id,
                    FirstName = a.Employee.FirstName,
                    LastName = a.Employee.LastName,
                    MiddleName = a.Employee.MiddleName,
                    Branch = a.Employee.Branch,
                    Company = a.Employee.Company,
                    Department = a.Employee.Department,
                    Position = a.Employee.Position,
                    Category = a.Employee.Category,
                    EmployeeNo = a.Employee.EmployeeNo,
                }).ToList();
            return result;
        }

        public IEnumerable<BehavioralFactor> BehavioralFactorPerEmployee(int id)
        {
            var items = _dbContext.EmployeeBehavioralAssignments
                .Include(a => a.EmployeeAssignment)
                .Include(a => a.BehavioralFactor)
                .Include(a => a.EmployeeAssignment.Employee)
                .Include(a => a.EvaluationSeason)
                .Where(a => a.EmployeeAssignment.Employee.Id == id && a.EmployeeAssignment.IsDeleted == false && a.EvaluationSeason.Id == ActiveSeason().Id && a.IsDeleted == false)
                .Select(a => new BehavioralFactor
                {
                    Id = a.BehavioralFactor.Id,
                    Title = a.BehavioralFactor.Title,
                    Description = a.BehavioralFactor.Description,
                    Category = a.BehavioralFactor.Category,
                    Weight = a.BehavioralFactor.Weight
                }).ToList();
            return items;
        }

        public string EmployeeNameById(int id)
        {
            var item = GetEmployeePerId(id);
            var name = item.FirstName + " " + item.MiddleName + " " + item.LastName;
            return name;
        }

        public IEnumerable<Account> Employees()
        {
            return _dbContext.Accounts.Where(a => a.IsDeleted == false);
        }

        public IEnumerable<EvaluationLine> EvaluationLinesPerId(int headerid)
        {
            return _dbContext.EvaluationLine
                .Include(a => a.EvaluationHeader)
                .Include(a => a.RatingTableItem)
                .Include(a => a.SuccessIndicator)
                .Where(a => a.EvaluationHeader.Id == headerid);
        }

        public IEnumerable<EvaluationHeader> EvaluationsPerUser(int id)
        {
            return _dbContext.EvaluationHeader
                .Include(a => a.Rater)
                .Where(a => a.Rater.Id == id);
        }

        public Account GetAccountById(int id)
        {
            return _dbContext.Accounts.FirstOrDefault(a => a.Id == id);
        }

        public BehavioralFactor GetBehavioralFactorById(int id)
        {
            return _dbContext.BehavioralFactors.FirstOrDefault(a => a.Id == id);
        }

        public BehavioralFactorItem GetBehavioralFactorItemById(int id)
        {
            return _dbContext.BehavioralFactorItems.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<BehavioralFactorItem> GetBehavioralItemsByHeaderId(int id)
        {
            var items = _dbContext.BehavioralFactorItems
                .Include(a => a.BehavioralFactor)
                .Where(a => a.IsDeleted == false && a.BehavioralFactor.Id == id);
            return items;
        }

        public Account GetEmployeePerId(int id)
        {
            return _dbContext.Accounts
                .Include(a => a.Company)
                .Include(a => a.Branch)
                .Include(a => a.Department)
                .Include(a => a.Position)
                .FirstOrDefault(a => a.Id == id);
        }

        public KeyResultArea GetKeyResultAreaById(int id)
        {
            return _dbContext.KeyResultAreas.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<KeySuccessIndicator> GetKeySuccessIndicators(int kraid)
        {
            var result = _dbContext.KeySuccessIndicators
                .Include(a => a.KeyResultArea)
                .Include(a => a.RatingTable)
                .Where(a => a.IsDeleted == false && a.KeyResultArea.Id == kraid);
            return result;
        }

        public IEnumerable<RatingBehavioralFactor> GetRatingBehavioralByHeaderId(int factorid, int id)
        {
            var header = _dbContext.RatingHeader
                .Include(a => a.Ratee)
                .FirstOrDefault(a => a.Ratee.Id == id && a.IsExpired == false && a.Type == "behavioral");
            var result = _dbContext.RatingBehavioralFactors
                .Include(a => a.RatingHeader)
                .Include(a => a.BehavioralFactor)
                .Include(a => a.BehavioralFactorItem)
                .Where(a => a.BehavioralFactor.Id == factorid && a.RatingHeader.Id == header.Id);
            return result;
        }

        public RatingHeader GetRatingHeaderBehavioral(int id)
        {
            var header = _dbContext.RatingHeader
                .Include(a => a.Ratee)
                .FirstOrDefault(a => a.Ratee.Id == id && a.IsExpired == false && a.Type == "behavioral");
            return header;
        }

        public RatingHeader GetRatingHeaderKeyResult(int id)
        {
            var header = _dbContext.RatingHeader
               .Include(a => a.Ratee)
               .FirstOrDefault(a => a.Ratee.Id == id && a.IsExpired == false && a.Type == "kra");
            return header;
        }

        public IEnumerable<RatingKeySuccessArea> GetRatingKeyResultByHeaderId(int kraid, int id)
        {
            var header = _dbContext.RatingHeader
                .Include(a => a.Ratee)
                .FirstOrDefault(a => a.Ratee.Id == id && a.IsExpired == false && a.Type == "kra");
            var result = _dbContext.RatingKeySuccessAreas
                         .Include(a => a.RatingHeader)
                         .Include(a => a.KeyResultArea)
                         .Include(a => a.KeySuccessIndicator)
                         .Include(a => a.KeySuccessIndicator.RatingTable)
                         .Where(a => a.RatingHeader.Id == header.Id && a.KeyResultArea.Id == kraid);
            return result;
        }

        public IEnumerable<RatingTableItem> GetRatingTableItemsPerId(int RatingTableId)
        {
            return _dbContext.RatingTableItems
                .Include(a => a.RatingTable)
                .Where(a => a.RatingTable.Id == RatingTableId && a.IsDeleted == false);
        }

        public Status GetStatusPerId(int id)
        {
            return _dbContext.Statuses.FirstOrDefault(a => a.Id == id);
        }

        public KeySuccessIndicator GetSuccessIndicatorById(int id)
        {
            return _dbContext.KeySuccessIndicators.FirstOrDefault(a => a.Id == id);
        }

        public bool IsBehavioralExists(int behavioralid, int employeeid)
        {
            var isExist = _dbContext.RatingBehavioralFactors
                          .Include(a => a.RatingHeader)
                          .Include(a => a.BehavioralFactor)
                          .Include(a => a.RatingHeader.Ratee)
                          .Any(a => a.BehavioralFactor.Id == behavioralid && a.RatingHeader.IsExpired == false && a.RatingHeader.Ratee.Id == employeeid);
            return isExist;
        }

        public bool IsBehavioralSaved(int behavioralid, int employeeid)
        {
            var isPosted = _dbContext.RatingBehavioralFactors
              .Include(a => a.RatingHeader)
              .Include(a => a.BehavioralFactor)
              .Include(a => a.RatingHeader.Ratee)
              .Include(a => a.RatingHeader.Status)
              .Any(a => a.BehavioralFactor.Id == behavioralid && a.RatingHeader.IsExpired == false && a.RatingHeader.Ratee.Id == employeeid && a.RatingHeader.Status.Id == TransactionStatus.Save.ToInt());
            return isPosted;
        }

        public bool IsKeyResultExists(int kraid, int employeeid)
        {
            var isExist = _dbContext.RatingKeySuccessAreas
                           .Include(a => a.RatingHeader)
                           .Include(a => a.RatingHeader.Ratee)
                           .Include(a => a.KeyResultArea)
                           .Any(a => a.KeyResultArea.Id == kraid && a.RatingHeader.Ratee.Id == employeeid && a.RatingHeader.IsExpired == false);
            return isExist;

        }

        public bool IsKeyResultSaved(int kraid, int employeeid)
        {
            var isPosted = _dbContext.RatingKeySuccessAreas
                           .Include(a => a.RatingHeader)
                           .Include(a => a.RatingHeader.Ratee)
                           .Include(a => a.KeyResultArea)
                           .Include(a => a.RatingHeader.Status)
                           .Any(a => a.KeyResultArea.Id == kraid && a.RatingHeader.Ratee.Id == employeeid && a.RatingHeader.IsExpired == false && a.RatingHeader.Status.Id == TransactionStatus.Save.ToInt());
            return isPosted;
        }

        public KeyResultArea KeyResultAreaPerId(int id)
        {
            return _dbContext.KeyResultAreas.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<KeyResultArea> KeyResultAreas()
        {
            return _dbContext.KeyResultAreas.Where(a => a.IsDeleted == false);
        }

        public IEnumerable<KeyResultArea> KeyResultAreasPerEmployee(int id)
        {
            var items = _dbContext.EmployeeKRAAssignments
                .Include(a => a.EmployeeAssignment)
                .Include(a => a.KeyResultArea)
                .Include(a => a.EmployeeAssignment.Employee)
                .Include(a => a.EvaluationSeason)
                .Where(a => a.EmployeeAssignment.Employee.Id == id && a.IsDeleted == false && a.EmployeeAssignment.IsDeleted == false && a.KeyResultArea.IsDeleted == false && a.EvaluationSeason.Id == ActiveSeason().Id)
                .Select(a => new KeyResultArea
                {
                    Id = a.KeyResultArea.Id,
                    Title = a.KeyResultArea.Title,
                    Description = a.KeyResultArea.Description,
                    Weight = a.KeyResultArea.Weight
                }).ToList();

            return items;
        }

        public int KeySuccessCounterByKRA(int kraid)
        {
            var items = GetKeySuccessIndicators(kraid);
            return items.Count();
        }
        public KeySuccessIndicator KeySuccessIndicatorPerId(int id)
        {
            return _dbContext.KeySuccessIndicators.FirstOrDefault(a => a.IsDeleted == false);
        }

        public void Post(EvaluationHeader header, Account account)
        {
            header.PostedDate = DateTime.Now;
            header.Status = GetStatusPerId(TransactionStatus.ForApproval.ToInt());
            _dbContext.Entry(header).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void PostBehavioral(int behavioralid, int employeeid)
        {
             var item = _dbContext.RatingBehavioralFactors
              .Include(a => a.RatingHeader)
              .Include(a => a.BehavioralFactor)
              .Include(a => a.RatingHeader.Ratee)
              .Include(a => a.RatingHeader.Status)
              .FirstOrDefault(a => a.BehavioralFactor.Id == behavioralid && a.RatingHeader.IsExpired == false && a.RatingHeader.Ratee.Id == employeeid);
            var header = _dbContext.RatingHeader.FirstOrDefault(a => a.Id == item.RatingHeader.Id);
            var status = _dbContext.Statuses.FirstOrDefault(a => a.Id == TransactionStatus.ForApproval.ToInt());
            header.Status = status;
            header.PostedDate = DateTime.Now;
            _dbContext.Entry(header).State = EntityState.Modified;
            _dbContext.SaveChanges();
            var id = item.RatingHeader.Id;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbo].[spEvaluationPostingNotification]";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@QueryType", 1);
            var result = SCObjects.ExecuteNonQuery(cmd, UserConnectionString);
        }

        public void PostKeyResultArea(int kraid, int employeeid)
        {
            var item = _dbContext.RatingKeySuccessAreas
                           .Include(a => a.RatingHeader)
                           .Include(a => a.RatingHeader.Ratee)
                           .Include(a => a.KeyResultArea)
                           .Include(a => a.RatingHeader.Status)
                           .FirstOrDefault(a => a.KeyResultArea.Id == kraid && a.RatingHeader.Ratee.Id == employeeid && a.RatingHeader.IsExpired == false);
            var header = _dbContext.RatingHeader.FirstOrDefault(a => a.Id == item.RatingHeader.Id);
            var status = _dbContext.Statuses.FirstOrDefault(a => a.Id == TransactionStatus.ForApproval.ToInt());
            header.Status = status;
            header.PostedDate = DateTime.Now;
            _dbContext.Entry(header).State = EntityState.Modified;
            _dbContext.SaveChanges();
            var id = item.RatingHeader.Id;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbo].[spEvaluationPostingNotification]";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@QueryType", 2);
            var result = SCObjects.ExecuteNonQuery(cmd, UserConnectionString);
        }

        public RatingTableItem RatingTableItem(int id)
        {
            return _dbContext.RatingTableItems
                .Include(a => a.RatingTable)
                .FirstOrDefault(a => a.Id == id);
        }

        public RatingTable RatingTablePerId(int id)
        {
            return _dbContext.RatingTables.FirstOrDefault(a => a.Id == id);
        }

        public void Save(EvaluationHeader header, IEnumerable<EvaluationLine> lineitems, Account account)
        {
            if (header.Id == 0)
            {
                header.Rater = account;
                header.CreatedBy = account.Username;
                header.CreationDate = DateTime.Now;
                _dbContext.Add(header);
            }
            else
            {
                header.ModifiedBy = account.Username;
                header.ModifiedDate = DateTime.Now;
                _dbContext.Entry(header).State = EntityState.Modified;
            }
            foreach (var item in lineitems)
            {
                item.EvaluationHeader = header;
                _dbContext.Add(item);
            }
            _dbContext.SaveChanges();
        }

        public void SaveBehavioralEvaluation(RatingHeader header, IEnumerable<RatingBehavioralFactor> ratings, int userid, int employeeid)
        {
            if (header.Id == 0)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "[dbo].[spNotifications]";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@From", userid);
                cmd.Parameters.AddWithValue("@To", employeeid);
                cmd.Parameters.AddWithValue("@Message", "Behavioral evaluation was created.");
                cmd.Parameters.AddWithValue("@QueryType", 1);
                var result = SCObjects.ExecuteNonQuery(cmd, UserConnectionString);

                header.EvaluationSeason = ActiveSeason();
                _dbContext.Add(header);
            }
            else
            {
                header.EvaluationSeason = ActiveSeason();
                _dbContext.Entry(header).State = EntityState.Modified;
               

            }
            foreach (var item in ratings)
            {
                item.RatingHeader = header;
                if (item.Id == 0)
                {
                    _dbContext.Add(item);
                }
                else
                {
                    _dbContext.Entry(item).State = EntityState.Modified;
                }
            }
            _dbContext.SaveChanges();

        }

        public void SaveKeyResultAreaEvaluation(RatingHeader header, IEnumerable<RatingKeySuccessArea> ratings,int userid, int employeeid)
        {
            if (header.Id == 0)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "[dbo].[spNotifications]";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@From", userid);
                cmd.Parameters.AddWithValue("@To", employeeid);
                cmd.Parameters.AddWithValue("@Message", "KRA evaluation was created.");
                cmd.Parameters.AddWithValue("@QueryType", 1);
                var result = SCObjects.ExecuteNonQuery(cmd, UserConnectionString);

                header.EvaluationSeason = ActiveSeason();
                _dbContext.Add(header);
            }
            else
            {
                header.EvaluationSeason = ActiveSeason();
                _dbContext.Entry(header).State = EntityState.Modified;
            }
            foreach (var item in ratings)
            {
                item.RatingHeader = header;
                if (item.Id == 0)
                {
                    _dbContext.Add(item);
                }
                else
                {
                    _dbContext.Entry(item).State = EntityState.Modified;
                }
            }
            _dbContext.SaveChanges();
        }

        public void SaveRating(RatingHeader header, IEnumerable<RatingBehavioralFactor> ratings, IEnumerable<RatingKeySuccessArea> areas)
        {
            if(header.Id == 0)
            {
                _dbContext.Add(header);
            }
            else
            {
                _dbContext.Entry(header).State = EntityState.Modified;
            }
            foreach(var item in ratings)
            {
                item.RatingHeader = header;
                if(item.Id == 0)
                {
                    _dbContext.Add(item);
                }
                else
                {
                    _dbContext.Entry(item).State = EntityState.Modified;
                }
            }
            foreach (var item in areas)
            {
                item.RatingHeader = header;
                if (item.Id == 0)
                {
                    _dbContext.Add(item);
                }
                else
                {
                    _dbContext.Entry(item).State = EntityState.Modified;
                }
            }
            _dbContext.SaveChanges();
        }

        public void UpdateRatingBehavioral(int HeaderId, int UserId, IEnumerable<RatingBehavioralFactor> ratings)
        {
            var header = _dbContext.RatingHeader.FirstOrDefault(a => a.Id == HeaderId);
            header.ModifiedBy = UserId.ToString();
            header.ModifiedDate = DateTime.Now;
            header.EvaluationSeason = ActiveSeason();
            _dbContext.Entry(header).State = EntityState.Modified;
            foreach(var item in ratings)
            {
                item.RatingHeader = header;
                _dbContext.Entry(item).State = EntityState.Modified;
            }
            _dbContext.SaveChanges();
        }

        public void UpdateRatingKeyResultArea(int HeaderId, int UserId, IEnumerable<RatingKeySuccessArea> ratings)
        {
            var header = _dbContext.RatingHeader.FirstOrDefault(a => a.Id == HeaderId);
            header.ModifiedBy = UserId.ToString();
            header.ModifiedDate = DateTime.Now;
            header.EvaluationSeason = ActiveSeason();
            _dbContext.Entry(header).State = EntityState.Modified;
            foreach(var item in ratings)
            {
                item.RatingHeader = header;
                _dbContext.Entry(item).State = EntityState.Modified;
            }
            _dbContext.SaveChanges();
        }
        public EvaluationSeason ActiveSeason()
        {
            return _dbContext.EvaluationSeasons.FirstOrDefault(a => a.IsActive == true);
        }
        public bool IsWithActiveSeason()
        {
            return _dbContext.EvaluationSeasons.Any(a => a.IsActive == true);
        }
    }
}
