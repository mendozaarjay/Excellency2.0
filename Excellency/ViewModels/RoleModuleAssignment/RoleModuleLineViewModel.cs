using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.ViewModels
{
    public class RoleModuleLineViewModel
    {
        public int Id { get; set; }

        public string RoleModulesHeaderId { get; set; }
        public string Module { get; set; }

        public bool New { get; set; } 
        public bool Save { get; set; } 
        public bool Delete { get; set; } 
        public bool Search { get; set; } 
        public bool Print { get; set; } 
        public bool Post { get; set; } 
        public bool Import { get; set; } 
        public bool Export { get; set; } 
        public bool Approve { get; set; }


        public IEnumerable<SelectListItem> Modules { get; set; }
    }
}
