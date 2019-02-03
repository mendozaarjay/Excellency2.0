using Excellency.Models;
using System.Collections.Generic;

namespace Excellency.ViewModels
{
    public class EvaluationResultViewModel
    {
        public IEnumerable<EvaluationResultItem> EvaluationResultItems { get; set; }
        public EvaluationInterpretation InterpretationPerEmployee { get; set; }
    }
}
