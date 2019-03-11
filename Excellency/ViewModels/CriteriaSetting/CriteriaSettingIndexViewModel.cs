using System.Collections.Generic;

namespace Excellency.ViewModels
{
    public class CriteriaSettingIndexViewModel
    {
        public CriteriaHeaderViewModel Header { get; set; }
        public IEnumerable<CriteriaHeaderViewModel> Items { get; set; }
    }
}
