using System.Collections.Generic;

namespace Excellency.ViewModels
{
    public class PeerAssignViewModel
    {
        public int RaterId { get; set; }
        public string RaterName { get; set; }
        public IEnumerable<PeerAssignmentAccountViewModel> Accounts { get; set; }
        public IEnumerable<AssignedPeerItemViewModel> Assigned { get; set; }
    }
}
