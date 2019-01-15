using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Excellency.ViewModels
{
    public class InterpretationIndexViewModel
    {
        [NotMapped]
        public IEnumerable<InterpretationItemViewModel> Interpretations { get; set; }
        public InterpretationItemViewModel Interpretation { get; set; }
    }
}
