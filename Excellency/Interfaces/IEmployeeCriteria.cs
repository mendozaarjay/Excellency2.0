using Excellency.Models;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface IEmployeeCriteria
    {
        IEnumerable<CriteriaHeader> Criterias();
        IEnumerable<CriteriaLine> CriteriaDetails(int id);
        IEnumerable<Account> Employees(int headerid);
        IEnumerable<Account> Employees(int headerid, int userid);
        IEnumerable<EmployeeCriteriaAssignment> Assignments(int criteriaid);
        CriteriaHeader CriteriaHeaderById(int id);
        EvaluationSeason ActivePeriod();
        void RemoveById(int id);
        void Add(int criteriaid, int employeeid, int userid);
    }
}
