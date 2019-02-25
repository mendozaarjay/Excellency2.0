using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.ViewModels
{
    public class RatingTableIndexViewModel
    {
        public IEnumerable<RatingTableViewModel> RatingTables { get; set; }
        public RatingTableViewModel RatingTable { get; set; }
        public bool IsWithActiveSeason { get; set; }
        public EvaluationSeasonItem ActiveSeason { get; set; }
    }
}
