using System.Collections.Generic;

namespace Excellency.ViewModels
{
    public class PeerCriteriaLineIndexViewModel
    {
        public int HeaderId { get; set; }
        public string HeaderTitle { get; set; }
        public string HeaderDescription { get; set; }
        public PeerCriteriaLineViewModel Criteria { get; set; }
        public IEnumerable<PeerCriteriaLineViewModel> LineItems { get; set; }
    }
}
