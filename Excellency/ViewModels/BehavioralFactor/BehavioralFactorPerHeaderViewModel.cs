using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.ViewModels
{
    public class BehavioralFactorPerHeaderViewModel
    {
        public int HeaderId { get; set; }
        [NotMapped]
        public BehavioralFactorViewModel BehavioralFactor { get; set; }
        [NotMapped]
        public IEnumerable<BehavioralFactorItemViewModel> BehavioralFactorItems { get; set; }
        public BehavioralFactorItemViewModel BehavioralFactorItem { get; set; }
    }
}
