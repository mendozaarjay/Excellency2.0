using Excellency.Dashboard;
using Excellency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.ViewModels
{
    public class UserAccountViewModel
    {
        public string Name { get; set; }
        public DashboardAccess DashboardAccess { get; set; }
        public string AccountPeriod { get; set; }
        public int UserCount { get; set; }
        public int RaterCount { get; set; }
        public int ApproverCount { get; set; }
        public int EmployeeCount { get; set; }
        public IEnumerable<RecentAccount> RecentAccounts { get; set; }
        public IEnumerable<RecentEmployee> RecentEmployees { get; set; }

        //Rater
        public int AssignedRateeCount { get; set; }
        public int EvaluatedRateeCount { get; set; }
        public int ApprovedRatingCount { get; set; }
        public int PendingRatingCount { get; set; }
        public IEnumerable<EmployeePerRaterViewModel> Employees { get; set; }

        //Approver
        public int AssignedPerApprover { get; set; }
        public int ApprovedEvaluation { get; set; }
        public int PendingEvaluation { get; set; }
        public IEnumerable<UserPerApprover> UserPerApprovers { get; set; }

        public IEnumerable<EvaluationHeaderItem> BehavioralHeaderItems { get; set; }
        public IEnumerable<EvaluationHeaderItem> KRAHeaderItems { get; set; }

        public IEnumerable<EvaluationLineItem> BehavioralLineItems { get; set; }
        public IEnumerable<EvaluationLineItem> KRALineItems { get; set; }


    }
}
