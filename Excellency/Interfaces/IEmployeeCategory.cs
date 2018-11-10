using Excellency.Models;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface IEmployeeCategory
    {
        void Save(EmployeeCategory category,string UserId);
        void RemoveCategoryPerId(int id);
        EmployeeCategory GetEmployeeCategory(int id);
        IEnumerable<EmployeeCategory> EmployeeCategories();
    }
}
