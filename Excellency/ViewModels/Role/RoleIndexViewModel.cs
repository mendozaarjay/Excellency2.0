using System.Collections.Generic;

namespace Excellency.ViewModels
{
    public class RoleIndexViewModel
    {
        public RoleViewModel Role { get; set; }
        public IEnumerable<RoleViewModel> Roles { get; set; }
    }
}
