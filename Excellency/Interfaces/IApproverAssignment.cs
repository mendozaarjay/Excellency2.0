using Excellency.Models;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface IApproverAssignment
    {
        IEnumerable<Account> GetAssignmentAccountByApproverId(int id);
        IEnumerable<Account> GetAccountsById(int id);
        void AssignAccounts(List<int> Accounts, int ApproverId, int UserId);
        void RemoveById(int id);
        string GetNameById(int id);
        Account GetAccountById(int id);
        IEnumerable<Account> GetAllAccounts();
        IEnumerable<ApproverAssignment> GetAssignedAccountsById(int id);
    }
}
