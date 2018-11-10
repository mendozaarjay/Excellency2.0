using Excellency.Models;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface IAccountRole
    {
        void AddRole(AccountRole role,int UserId);
        void UpdateRole(AccountRole role, int UserId);
        AccountRole GetRoleById(int id);
        IEnumerable<AccountRole> GetAllRoles();
        AccountRoleAssignment GetAccountAssignmentById(int id);
        IEnumerable<AccountRoleAssignment> GetAssignmentsByHeaderId(int id);

        Account GetAccountById(int id);
        IEnumerable<Account> GetAllAccount();

        void AddAccount(int RoleId, List<int> Accounts);
        void RemoveAccount(int id);
        void RemoveAccountAssignment(int id);
    }
}
