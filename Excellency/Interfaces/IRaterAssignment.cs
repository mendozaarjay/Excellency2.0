using Excellency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.Interfaces
{
    public interface IRaterAssignment
    {
        IEnumerable<Account> Raters();
        IEnumerable<Account> Employees(int RaterId);
        IEnumerable<Account> AssignedEmployees(int RaterId);
        IEnumerable<Branch> Branches();
        IEnumerable<Company> Companies();
        IEnumerable<Department> Departments();
        IEnumerable<Position> Positions();
        Account GetAccountById(int id);
        Account GetEmployeeById(int id);
        Company GetCompanyById(int id);
        Branch GetBranchById(int id);
        Department GetDepartmentById(int id);
        Position GetPositionById(int id);
        EmployeeRaterHeader GetRaterById(int RaterId);
        IEnumerable<EmployeeRaterLine> RaterAssignedEmployees(int HeaderId);
        int GetRaterHeaderId(int RaterId);
        void AddEmployee(int Id,int RaterId);
        void AddEmployee(IEnumerable<int> Items,int RaterId,string UserId);
        void RemoveLineItem(int Id);

    }
}
