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
    public class EmployeeService : IEmployee
    {
        private EASDbContext _dbContext;

        public EmployeeService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Account Employee)
        {
            _dbContext.Accounts.Add(Employee);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Branch> Branches()
        {
            return _dbContext.Branches.Where(a => a.IsDeleted == false);
        }

        public IEnumerable<Company> Companies()
        {
            return _dbContext.Companies.Where(a => a.IsDeleted == false);
        }

        public IEnumerable<Department> Departments()
        {
            return _dbContext.Departments.Where(a => a.IsDeleted == false);
        }

        public IEnumerable<EmployeeCategory> EmployeeCategories()
        {
            return _dbContext.EmployeeCategories.Where(a => a.IsDeleted == false);
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

        public Branch GetBranchById(int id)
        {
            return _dbContext.Branches.FirstOrDefault(a => a.Id == id);
        }

        public EmployeeCategory GetCategoryById(int id)
        {
            return _dbContext.EmployeeCategories.FirstOrDefault(a => a.Id == id);
        }

        public Company GetCompanyById(int id)
        {
            return _dbContext.Companies.FirstOrDefault(a => a.Id == id);
        }

        public Department GetDepartmentById(int id)
        {
            return _dbContext.Departments.FirstOrDefault(a => a.Id == id);
        }

        public Account GetEmployeeById(int id)
        {
            return _dbContext.Accounts
                   .Include(a => a.Company)
                   .Include(a => a.Branch)
                   .Include(a => a.Department)
                   .Include(a => a.Position)
                   .Include(a => a.Category)
                   .FirstOrDefault(a => a.Id == id);
        }

        public Position GetPositionById(int id)
        {
            return _dbContext.Positions.FirstOrDefault(a => a.Id == id);
        }

        public bool IsAlreadyExisting(string EmployeeNo)
        {
            return _dbContext.Accounts.Any(a => a.EmployeeNo == EmployeeNo);
        }

        public IEnumerable<Position> Positions()
        {
            return _dbContext.Positions.Where(a => a.IsDeleted == false);
        }

        public void RemoveById(int Id)
        {
            var employee = _dbContext.Accounts.FirstOrDefault(a => a.Id == Id);
            _dbContext.Entry(employee).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Update(Account Employee)
        {
            _dbContext.Entry(Employee).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
