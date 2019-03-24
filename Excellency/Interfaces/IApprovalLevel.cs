using Excellency.Models;
using Excellency.ViewModels;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface IApprovalLevel
    {
        void Save(ApprovalLevelAssignment approval,int userid);
        IEnumerable<ApprovalLevelAssignment> ApprovalLevels();
        IEnumerable<Account> Employees();
        IEnumerable<Account> Approvers(int id);
        ApprovalLevelAssignment ApprovalLevelById(int id);
        ApprovalLevelAssignment ApprovalAssignmentByEmployee(int id);
        Account GetAccountById(int? id);
        string GetNameById(int id);
        void RemoveById(int id);
        bool IsSet(int id);
        IEnumerable<ApprovalLevelAccountItem> ApprovalLevelItems(int page);
    }
}
