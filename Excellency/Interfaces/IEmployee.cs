using Excellency.Models;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface IEmployee
    {
        void Add(Employee Employee);
        void Update(Employee Employee);
        IEnumerable<Employee> Employees();
        Employee GetEmployeeById(int id);
        void RemoveById(int Id);

        IEnumerable<Company> Companies();
        IEnumerable<Branch> Branches();
        IEnumerable<Department> Departments();
        IEnumerable<Position> Positions();
        IEnumerable<EmployeeCategory> EmployeeCategories();

        Company GetCompanyById(int id);
        Branch GetBranchById(int id);
        Department GetDepartmentById(int id);
        Position GetPositionById(int id);
        EmployeeCategory GetCategoryById(int id);

        bool IsAlreadyExisting(string EmployeeNo);

    }
}
