using System.Collections.Generic;

namespace Excellency.ViewModels
{
    public class ModuleIndexViewModel
    {
        public ModuleViewModel Module { get; set; }
        public IEnumerable<ModuleViewModel> Modules { get; set; }
    }
}
