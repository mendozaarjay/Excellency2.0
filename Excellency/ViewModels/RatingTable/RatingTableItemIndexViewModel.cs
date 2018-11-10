using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.ViewModels
{
    public class RatingTableItemIndexViewModel
    {
        public int RatingTableId { get; set; }
        public string RatingTableDescription { get; set; }
        public IEnumerable<RatingTableItemViewModel> RatingTableItems { get; set; }
        public RatingTableItemViewModel RatingTableItem { get; set; }
    }
}
