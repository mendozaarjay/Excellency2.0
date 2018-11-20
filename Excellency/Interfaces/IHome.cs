using Excellency.Dashboard;
using Excellency.Models;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface IHome
    {
        string GetUserId(Account account);

        bool IsUserAlreadyExist(Account account);
        bool IsAccountLocked(Account account);
        bool IsAvailableToLogin(Account account);
        bool IsLoginExpired(Account account);
        bool IsNotExist(Account account);

        UserAccess UserAccess(int UserId);
        bool IsAdministrator(int UserId);
        bool IsApprover(int UserId);
        bool IsRater(int UserId);
        Account GetAccountById(int id);

        string GetUserNameById(int id);


        //List<DataPoint> EvaluatedEmployees(int userid);

        //DASHBOARD METHODS AND FUNCTIONS
        DashboardAccess DashboardAccessPerUser(int userid);
        //Administrator
        IEnumerable<Account> Accounts();
        IEnumerable<DataPoint> AccountPerCompany();
        IEnumerable<DataPoint> EmployeePerCompany();

        //Rater
        DataPoint RatedEmployees(int userid);
        IEnumerable<EmployeePerRaterViewModel> EmployeesPerRater(int userid);
        IEnumerable<EvaluationCriteria> BehavioralStrength(int employeeid, int userid);
        IEnumerable<EvaluationCriteria> BehavioralWeakness(int employeeid, int userid);
        IEnumerable<EvaluationCriteria> KeyResultAreaStrength(int employeeid, int userid);
        IEnumerable<EvaluationCriteria> KeyResultAreaWeakness(int employeeid, int userid);
        //Approver

    }
}
