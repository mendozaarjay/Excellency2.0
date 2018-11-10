using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.ViewModels
{
    public class RatingIndexViewModel
    {
        public IEnumerable<RatingViewModel> Ratings { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public int Score { get; set; }
    }
}
