using Excellency.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.Persistence
{
    public class EASDbContext : DbContext
    {
        public EASDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<EmployeeRaterHeader> RaterHeader { get; set; }
        public DbSet<EmployeeRaterLine> RaterLine { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<KeyResultArea> KeyResultAreas { get; set; }
        public DbSet<KeySuccessIndicator> KeySuccessIndicators { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<RatingTable> RatingTables { get; set; }
        public DbSet<RatingTableItem> RatingTableItems { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<EvaluationHeader> EvaluationHeader { get; set; }
        public DbSet<EvaluationLine> EvaluationLine { get; set; }
        public DbSet<EvaluationSetting> EvaluationSettings { get; set; }
        public DbSet<BehavioralFactor> BehavioralFactors { get; set; }
        public DbSet<BehavioralFactorItem> BehavioralFactorItems { get; set; }
        public DbSet<EmployeeCategory> EmployeeCategories { get; set; }
        public DbSet<EmployeeAssignment> EmployeeAssignments { get; set; }
        public DbSet<EmployeeKRAAssignment> EmployeeKRAAssignments { get; set; }
        public DbSet<EmployeeBehavioralAssignment> EmployeeBehavioralAssignments { get; set; }
        public DbSet<RatingHeader> RatingHeader { get; set; }
        public DbSet<RatingBehavioralFactor> RatingBehavioralFactors { get; set; }
        public DbSet<RatingKeySuccessArea> RatingKeySuccessAreas { get; set; }
        public DbSet<ApproverAssignment> ApproverAssignments { get; set; }

        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<AccountRoleAssignment> AccountRoleAssignments { get; set; }

        public DbSet<Criteria> Criterias { get; set; }
        public DbSet<PeerCriteria> PeerCriterias { get; set; }
        public DbSet<PeerCriteriaLine> PeerCriteriaLine { get; set; }
        public DbSet<Registration> Registrations { get; set; } 
        public DbSet<Interpretation> Interpretations { get; set; }
        public DbSet<PeerEvaluationHeader> PeerEvaluationHeader { get; set; }
        public DbSet<PeerEvaluationLine> PeerEvaluationLine { get; set; }
        public DbSet<ApprovalLevelAssignment> ApprovalLevelAssignment { get; set; }
        public DbSet<PeerAssignment> PeerAssignment { get; set; }
    }
}
