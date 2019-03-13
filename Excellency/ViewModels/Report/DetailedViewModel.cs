using System.Collections.Generic;

namespace Excellency.ViewModels
{
    public class DetailedViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Weight { get; set; }
        public IEnumerable<PeerRatingDetailed> Items { get; set; }
    }
}
