using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.ViewModels
{
    public class PeerEvaluationLineItemViewModel
    {
        public int Id { get; set; }
        public int HeaderId { get; set; }
        public int CriteriaId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Weight { get; set; }
        public int Score { get; set; }
        public string Comment { get; set; }
    }
}
