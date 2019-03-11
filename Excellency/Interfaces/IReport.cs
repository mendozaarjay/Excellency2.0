using Excellency.Models;
using Excellency.ViewModels;
using System.Collections.Generic;
using System.Data;

namespace Excellency.Interfaces
{
    public interface IReport
    {
        IEnumerable<EvaluationReport> Evaluations(int periodid,string type);
        IEnumerable<EvaluationSeason> EvaluationSeasons();
        IEnumerable<EmployeeInformation> Employees(string keyword);
        IEnumerable<EmployeePerformance> EmployeePerformances(int period, int id);
        IEnumerable<AppraisalHistory> AppraisalHistories(int period, int id);
        IEnumerable<AccountItem> Accounts();
    }
}
