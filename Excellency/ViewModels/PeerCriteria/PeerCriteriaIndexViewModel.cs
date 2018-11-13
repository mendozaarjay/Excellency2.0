using System.Collections.Generic;

namespace Excellency.ViewModels
{
    public class PeerCriteriaIndexViewModel
    {
        public PeerCriteriaHeaderViewModel PeerCriteria { get; set; }
        public IEnumerable<PeerCriteriaHeaderViewModel> Criterias { get; set; }
    }
}
