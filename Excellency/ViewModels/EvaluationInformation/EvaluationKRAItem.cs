using System.Collections.Generic;

namespace Excellency.ViewModels
{
    public class EvaluationKRAItem
    {
        public EIEvaluationItem Header { get; set; }
        public IEnumerable<EIEvaluationLineItem> LineItems { get; set; }
    }
}
