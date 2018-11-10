using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Excellency.ViewModels
{
    public class RoleModuleAssignmentViewModel
    {
        public RoleModuleHeaderViewModel Header { get; set; }
        public IEnumerable<RoleModuleLineViewModel> LineItems { get; set; }
    }
}
