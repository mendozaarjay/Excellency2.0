using Excellency.Models;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface ICriteriaEvaluation
    {
        IEnumerable<Account> Accounts(int userId);
        EvaluationSeason ActivePeriod();
        EmployeeCriteriaAssignment EmployeeCriteriaAssignmentByEmployeeId(int id);
        CriteriaHeader CriteriaHeaderById(int id);
        IEnumerable<CriteriaLine> CriteriaLinesByHeaderId(int id);
        CriteriaLine CriteriaLineById(int id);

        CriteriaEvaluationHeader CriteriaEvaluationPerEmployeeId(int id,int userid);
        IEnumerable<CriteriaEvaluationLine> EvaluationLinesByHeaderId(int id);
        CriteriaEvaluationLine CriteriaEvaluationLineById(int id);
        string GetName(int id);
        Account GetAccountById(int id);

        void Save(CriteriaEvaluationHeader header, IEnumerable<CriteriaEvaluationLine> items, int userid);
        void Update(CriteriaEvaluationHeader header, IEnumerable<CriteriaEvaluationLine> items, int userid);
        void Post(int id);
        bool IsEvaluated(int employeeid, int id, int userid);
        bool IsPosted(int employeeid, int id, int userid);

    }
}
