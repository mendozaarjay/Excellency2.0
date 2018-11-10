using Excellency.Interfaces;
using Excellency.Models;
using Excellency.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Excellency.Services
{
    public class RaterAssignmentService : IRaterAssignment
    {
        private EASDbContext _dbContext;

        public RaterAssignmentService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddEmployee(int Id, int RaterId)
        {
            var Header = new EmployeeRaterHeader();
            var Rater = new Account();
            if (this.GetRaterById(RaterId) != null)
            {
                var headerItem = GetRaterById(RaterId);
                Header.Id = headerItem.Id;
                Header.Rater = _dbContext.Accounts.FirstOrDefault(a => a.Id == RaterId);

                var lineItem = new EmployeeRaterLine
                {
                    EmployeeRater = headerItem,
                    Employee = _dbContext.Employees.FirstOrDefault(a => a.Id == Id),
                };
                _dbContext.Entry(Header).State = EntityState.Modified;
                _dbContext.Add(lineItem);
                _dbContext.SaveChanges();
            }
            else
            {
                Header.Rater = _dbContext.Accounts.FirstOrDefault(a => a.Id == RaterId);
                var lineItem = new EmployeeRaterLine
                {
                    EmployeeRater = Header,
                    Employee = _dbContext.Employees.FirstOrDefault(a => a.Id == Id),
                };
                _dbContext.Add(Header);
                _dbContext.Add(lineItem);
                _dbContext.SaveChanges();
            }
        }

        public void AddEmployee(IEnumerable<int> Items, int RaterId,string UserId)
        {
            var Header = new EmployeeRaterHeader();
            var GetHeaderItem = this.GetRaterById(RaterId);
            if (GetHeaderItem != null)
            {
                Header = GetHeaderItem;
                Header.ModifiedBy = UserId;
                Header.ModifiedDate = DateTime.Now;
                _dbContext.Entry(Header).State = EntityState.Modified;
            }
            else
            {
                Header.Id = 0;
                Header.CreatedBy = UserId;
                Header.CreationDate = DateTime.Now;
                Header.Rater = _dbContext.Accounts.FirstOrDefault(a => a.Id == RaterId);
                _dbContext.Add(Header);
            }
            foreach (var item in Items)
            {
                var lineItem = new EmployeeRaterLine
                {
                    EmployeeRater = Header,
                    Employee = _dbContext.Employees.FirstOrDefault(a => a.Id == item)
                };
                _dbContext.Add(lineItem);
            }
            _dbContext.SaveChanges();
        }

        public IEnumerable<Employee> AssignedEmployees(int RaterId)
        {
            var id = GetRaterHeaderId(RaterId);
            var items = RaterAssignedEmployees(id)
                .Select(
                a => new Employee
                {
                    Id = a.Employee.Id,
                    EmployeeNo = a.Employee.EmployeeNo,
                    FirstName = a.Employee.FirstName,
                    MiddleName = a.Employee.MiddleName,
                    LastName = a.Employee.LastName,
                    Company = a.Employee.Company,
                    Branch = a.Employee.Branch,
                    Department = a.Employee.Department,
                    Position = a.Employee.Position,
                    CreatedBy = a.Employee.CreatedBy,
                    CreationDate = a.Employee.CreationDate,
                    ModifiedBy = a.Employee.ModifiedBy,
                    ModifiedDate = a.Employee.ModifiedDate,
                    IsDeleted = a.Employee.IsDeleted
                }
                ).ToList();
            return items;
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



        public IEnumerable<Employee> Employees(int RaterId)
        {
            if (GetRaterById(RaterId) == null)
            {
                return _dbContext.Employees.Where(a => a.IsDeleted == false);
            }
            else
            {
                var items = AssignedEmployees(RaterId);
                return _dbContext.Employees.Where(a => a.IsDeleted == false).Except(items);
            }

        }

        public Account GetAccountById(int id)
        {
            return _dbContext.Accounts
                  .Include(a => a.Company)
                  .Include(a => a.Branch)
                  .Include(a => a.Department)
                  .Include(a => a.Position).FirstOrDefault(a => a.Id == id);
        }

        public Branch GetBranchById(int id)
        {
            return _dbContext.Branches.FirstOrDefault(a => a.Id == id);
        }

        public Company GetCompanyById(int id)
        {
            return _dbContext.Companies.FirstOrDefault(a => a.Id == id);
        }

        public Department GetDepartmentById(int id)
        {
            return _dbContext.Departments.FirstOrDefault(a => a.Id == id);
        }

        public Employee GetEmployeeById(int id)
        {
            return _dbContext.Employees
                   .Include(a => a.Company)
                   .Include(a => a.Branch)
                   .Include(a => a.Department)
                   .Include(a => a.Position)
                   .FirstOrDefault(a => a.Id == id);
        }

        public Position GetPositionById(int id)
        {
            return _dbContext.Positions.FirstOrDefault(a => a.Id == id);
        }

        public EmployeeRaterHeader GetRaterById(int RaterId)
        {
            return _dbContext.RaterHeader
                   .Include(a => a.Rater)
                   .FirstOrDefault(a => a.Rater.Id == RaterId);
        }

        public int GetRaterHeaderId(int RaterId)
        {
            return _dbContext.RaterHeader.Include(a => a.Rater).FirstOrDefault(a => a.Rater.Id == RaterId).Id;
        }

        public IEnumerable<Position> Positions()
        {
            return _dbContext.Positions.Where(a => a.IsDeleted == false);
        }

        public IEnumerable<EmployeeRaterLine> RaterAssignedEmployees(int HeaderId)
        {
            return _dbContext.RaterLine
               .Include(a => a.EmployeeRater)
               .Include(a => a.Employee)
               .Include(a => a.Employee.Company)
               .Include(a => a.Employee.Branch)
               .Include(a => a.Employee.Department)
               .Include(a => a.Employee.Position)
               .Where(a => a.IsDeleted == false && a.EmployeeRater.Id == HeaderId);
        }

        public IEnumerable<Account> Raters()
        {
            return _dbContext.Accounts
                   .Include(a => a.Company)
                   .Include(a => a.Branch)
                   .Include(a => a.Department)
                   .Include(a => a.Position).Where(a => a.IsDeleted == false);
        }

        public void RemoveLineItem(int Id)
        {
            var item = _dbContext.RaterLine.FirstOrDefault(a => a.Id == Id);
            item.IsDeleted = true;
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
