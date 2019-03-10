using Excellency.Interfaces;
using Excellency.Models;
using Excellency.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Excellency.Services
{
    public class EmployeeAssignmentService : IEmployeeAssignment
    {
        private EASDbContext _dbContext;

        public string UserConnectionString { get; }

        public EmployeeAssignmentService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
            UserConnectionString = _dbContext.Database.GetDbConnection().ConnectionString;
        }

        public BehavioralFactor BehavioralFactorById(int id)
        {
            return _dbContext.BehavioralFactors.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<BehavioralFactor> BehavioralFactors()
        {
            return _dbContext.BehavioralFactors.Where(a => a.IsDeleted == false);
        }

        public EmployeeAssignment EmployeeAssignmentByEmployeeId(int id)
        {
            return _dbContext.EmployeeAssignments
                    .Include(a => a.Employee)
                    .FirstOrDefault(a => a.Employee.Id == id);

        }

        public IEnumerable<EmployeeAssignment> EmployeeAssignments()
        {
            return _dbContext.EmployeeAssignments;
        }

        public IEnumerable<EmployeeBehavioralAssignment> EmployeeBehavioralAssignmentByHeaderId(int id)
        {
            return _dbContext.EmployeeBehavioralAssignments
                .Include(a => a.EmployeeAssignment)
                .Include(a => a.BehavioralFactor)
                .Include(a => a.EvaluationSeason)
                .Where(a => a.EmployeeAssignment.Id == id && a.IsDeleted == false && a.EvaluationSeason.Id == ActiveSeason().Id);
        }

        public Account EmployeeById(int id)
        {
            return _dbContext.Accounts
                .Include(a => a.Company)
                .Include(a => a.Branch)
                .Include(a => a.Department)
                .Include(a => a.Position)
                .Include(a => a.Category)
                .FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<EmployeeKRAAssignment> EmployeeKRAAssignmentByHeaderId(int id)
        {
            return _dbContext.EmployeeKRAAssignments
                .Include(a => a.EmployeeAssignment)
                .Include(a => a.KeyResultArea)
                .Include(a => a.EvaluationSeason)
                .Where(a => a.EmployeeAssignment.Id == id && a.IsDeleted == false && a.EvaluationSeason.Id == ActiveSeason().Id);
        }

        public IEnumerable<Account> Employees()
        {
            return _dbContext.Accounts
                .Include(a => a.Company)
                .Include(a => a.Branch)
                .Include(a => a.Department)
                .Include(a => a.Position)
                .Include(a => a.Category)
                .Where(a => a.IsDeleted == false);
        }

        public KeyResultArea KeyResultAreaById(int id)
        {
            return _dbContext.KeyResultAreas.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<KeyResultArea> KeyResultAreas()
        {
            return _dbContext.KeyResultAreas.Where(a => a.IsDeleted == false);
        }

        public void RemoveAssignmentById(int id)
        {
            var item = _dbContext.EmployeeAssignments.FirstOrDefault(a => a.Id == id);
            item.IsDeleted = true;
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void RemoveBehavioralPerId(int id)
        {
            var item = _dbContext.EmployeeBehavioralAssignments.FirstOrDefault(a => a.Id == id);
            item.IsDeleted = true;
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void RemoveKRAById(int id)
        {
            var item = _dbContext.EmployeeKRAAssignments.FirstOrDefault(a => a.Id == id);
            item.IsDeleted = true;
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Save(EmployeeAssignment employeeAssignment, EmployeeKRAAssignment kraAssignment, EmployeeBehavioralAssignment behavioralAssignment)
        {
            
        }

        public void Save(int EmployeeId, List<int> KRAItems, List<int> BehavioralItems)
        {
            
        }

        public void SaveBehavioralItems(int EmployeeId, List<int> Items)
        {
            var Header = new EmployeeAssignment();
            var headerItem = this.EmployeeAssignmentByEmployeeId(EmployeeId);
            if(headerItem != null)
            {
                Header = headerItem;
                _dbContext.Entry(Header).State = EntityState.Modified;
            }
            else
            {
                Header.Id = 0;
                Header.Employee = EmployeeById(EmployeeId);
                _dbContext.Add(Header);
            }
            foreach(var item in Items)
            {
                var lineItem = new EmployeeBehavioralAssignment
                {
                    EmployeeAssignment = Header,
                    BehavioralFactor = BehavioralFactorById(item),
                    IsDeleted = false,
                    EvaluationSeason = ActiveSeason()
                };
                _dbContext.Add(lineItem);
            }
            _dbContext.SaveChanges();

        }

        public void SaveKRAItems(int EmployeeId, List<int> Items)
        {
            var Header = new EmployeeAssignment();
            var headerItem = this.EmployeeAssignmentByEmployeeId(EmployeeId);
            if (headerItem != null)
            {
                Header = headerItem;
                _dbContext.Entry(Header).State = EntityState.Modified;
            }
            else
            {
                Header.Id = 0;
                Header.Employee = EmployeeById(EmployeeId);
                _dbContext.Add(Header);
            }
            foreach (var item in Items)
            {
                var lineItem = new EmployeeKRAAssignment
                {
                    EmployeeAssignment = Header,
                    KeyResultArea = KeyResultAreaById(item),
                    IsDeleted = false,
                    EvaluationSeason = ActiveSeason()
                };
                _dbContext.Add(lineItem);
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
        public EvaluationSeason EvaluationSeasonById(int id)
        {
            return _dbContext.EvaluationSeasons.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<KeyResultArea> GetAvailableKRA(int employeeid)
        {
            string sql = string.Format(@"SELECT * FROM [dbo].[fnAvailableKRALookup]({0}) [fakl]", employeeid.ToString());
            DataTable dt = SCObjects.LoadDataTable(sql, UserConnectionString);
            List<KeyResultArea> items = new List<KeyResultArea>();
            if(dt != null)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    var item = new KeyResultArea
                    {
                        Id = int.Parse(dr["Id"].ToString()),
                        Title = dr["Title"].ToString(),
                        Description = dr["Description"].ToString(),
                        Weight = int.Parse(dr["Weight"].ToString()),
                    };
                    items.Add(item);
                }
            }
            return items;
        }

        public IEnumerable<BehavioralFactor> GetAvailableBehavioral(int employeeid)
        {
            string sql = string.Format("SELECT * FROM [dbo].[fnAvailableBehavioralLookup]({0}) [fabl]", employeeid.ToString());
            DataTable dt = SCObjects.LoadDataTable(sql, UserConnectionString);
            List<BehavioralFactor> items = new List<BehavioralFactor>();
            if(dt != null)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    var item = new BehavioralFactor
                    {
                        Id = int.Parse(dr["Id"].ToString()),
                        Title = dr["Title"].ToString(),
                        Description = dr["Description"].ToString(),
                        Weight = int.Parse(dr["Weight"].ToString()),
                    };
                    items.Add(item);
                }
            }
            return items;
        }
    }
}
