using Excellency.Models;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface IDepartment
    {
        void Add(Department Department);
        void Update(Department Department);
        IEnumerable<Department> Departments();
        Department GetDepartmentById(int id);
        void RemoveById(int Id);
    }
}
