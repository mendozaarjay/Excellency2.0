using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.ViewModels
{
    public class ApproverAssignmentViewModel
    {
        public int ApproverId { get; set; }
        public string ApproverName { get; set; }
        public IEnumerable<AssignedAccountViewModel> AssignedAccounts { get; set; }
        public IEnumerable<AccountItemViewModel> Accounts { get; set; }
        public string[] SelectedItems { get; set; }
    }
}
