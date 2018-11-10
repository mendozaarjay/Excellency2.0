using System.Collections.Generic;

namespace Excellency.ViewModels
{
    public class AccountRoleAssignmentViewModel
    {
        public int RoleId { get; set; }
        public string Description { get; set; }
        public IEnumerable<AccountItem> Accounts { get; set; }
        public IEnumerable<AccountItem> AssignedAccounts { get; set; }
        public string[] SelectedItems { get; set; }
    }
}
