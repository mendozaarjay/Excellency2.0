﻿using Excellency.Dashboard;
using Excellency.Models;
using Excellency.ViewModels;
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
        IEnumerable<ChartPoint> AccountPerCompany();
        IEnumerable<DataPoint> EmployeePerCompany();
        int UserCount();
        int RaterCount();
        int ApproverCount();
        int EmployeeCount();
        string AccountPeriod();
        IEnumerable<Account> MostRecentAccounts();
        IEnumerable<Account> MostRecentEmployees();
        //Rater


        int AssignedRateeCount(int userid);
        int EvaluatedRateeCount(int userid);
        int ApprovedRatingCount(int userid);
        int PendingRatingCount(int userid);

        DataPoint RatedEmployees(int userid);
        IEnumerable<EmployeePerRaterViewModel> EmployeesPerRater(int userid);
        string EmployeeNameById(int id);
        int EmployeeTotalScore(int id);
        IEnumerable<EvaluationCriteria> BehavioralStrength(int employeeid);
        IEnumerable<EvaluationCriteria> BehavioralWeakness(int employeeid);
        IEnumerable<EvaluationCriteria> KeyResultAreaStrength(int employeeid);
        IEnumerable<EvaluationCriteria> KeyResultAreaWeakness(int employeeid);
        //Approver
        int AssignedPerApprover(int userid);
        int ApprovedEvaluation(int userid);
        int PendingForApproval(int userid);
        IEnumerable<UserPerApprover> ApproverAssignedUser(int userid);

        IEnumerable<EvaluationHeaderItem> GetBehavioralEvaluations(int id);
        IEnumerable<EvaluationHeaderItem> GetKRAEvaluations(int id);
        List<EvaluationLineItem> GetBehavioralLineItems(int headerid);
        List<EvaluationLineItem> GetKRAEvaluationLineItems(int headerid);


        bool _IsAdmin(int userid);
        bool _IsRater(int userid);
        bool _IsApprover(int userid);
        bool _IsEmployee(int userid);
        IEnumerable<NotificationItem> Notifications(int id);
    }
}
