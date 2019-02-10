using System.Collections.Generic;


namespace Excellency.ViewModels
{
    public class ApprovalIndexViewModel
    {
        public IEnumerable<FirstApprovalItem> FirstApprovalItems { get; set; }
        public IEnumerable<SecondApprovalItem> SecondApprovalItems { get; set; }
        public bool IsWithFirstApproval { get; set; }
        public bool IsWithSecondApproval { get; set; }
    }
}
