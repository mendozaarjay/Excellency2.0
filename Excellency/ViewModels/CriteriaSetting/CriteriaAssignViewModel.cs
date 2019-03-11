using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.ViewModels
{
    public class CriteriaAssignViewModel
    {
        public CriteriaHeaderViewModel Header { get; set; }
        public CriteriaLineViewModel Line { get; set; }
        public IEnumerable<CriteriaLineViewModel> LineItems { get; set; }
    }
}
