using Excellency.ViewModels;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface IApproval
    {
        IEnumerable<FirstApprovalItem> GetAllFirstApprovals(int userid);
        IEnumerable<SecondApprovalItem> GetAllSecondApprovals(int userid);
        bool IsWithFirstApproval(int userid);
        bool IsWithSecondApproval(int userid);

        void ApproveFirstApproval(int id, string remarks, int userid);
        void DisapproveFirstApproval(int id, string remarks, int userid);
        void ApproveSecondApproval(int id, string remarks, int userid);
        void DisapproveSecondApproval(int id, string remarks, int userid);
    }
}
