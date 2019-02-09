using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.ViewModels
{
    public class UserAccessViewModel
    {
        public string Name { get; set; }
        /// <summary>
        /// Is with access with dashboard module.
        /// </summary>
        public bool Dashboard { get; set; }

        /// <summary>
        /// Maintenance Main Module.
        /// </summary>
        public bool Maintenance { get; set; }

        //Maintenance sub modules.
        public bool Company { get; set; }
        public bool Branch { get; set; }
        public bool Department { get; set; }
        public bool Position { get; set; }
        public bool EmployeeCategory { get; set; }

        /// <summary>
        /// Settings Main Module.
        /// </summary>
        public bool Settings { get; set; }
        //Settings sub modules.
        public bool Ratings { get; set; }
        public bool RatingTable { get; set; }
        public bool KeyResultArea { get; set; }
        public bool BehavioralKRA { get; set; }
        public bool EmployeeKRAAssignment { get; set; }

        /// <summary>
        /// Accounts Main Module.
        /// </summary>
        public bool Accounts { get; set; }
        //Accounts sub modules.
        public bool Users { get; set; }
        public bool UserRoles { get; set; }
        public bool ApproverAssignment { get; set; }

        /// <summary>
        /// Employees Main Module.
        /// </summary>
        public bool Employees { get; set; }

        //Employees sub module.
        public bool Employee { get; set; }
        public bool RaterAssignment { get; set; }

        /// <summary>
        /// Evaluation Main Module.
        /// </summary>
        public bool Evaluation { get; set; }
        //Evaluation sub module.
        public bool CreateEvaluation { get; set; }
        public bool Approval { get; set; }

        //Employee
        public bool IsWithEvaluation { get; set; }
    }
}
