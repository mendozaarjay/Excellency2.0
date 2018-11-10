using Excellency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.ViewModels
{
    public class CurrentUserViewModel
    {
        public string Name { get; set; }
        public UserAccess UserAccess;
    }
}
