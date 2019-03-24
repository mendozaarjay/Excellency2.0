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

        public bool IsAdmin { get; set; }
        public bool IsApprover { get; set; }
        public bool IsEmployee { get; set; }
        public bool IsRater { get; set; }
        public IEnumerable<NotificationItem> Notifications { get; set; }
    }
}
