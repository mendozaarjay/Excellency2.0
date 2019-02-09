using Excellency.Models;
using Excellency.ViewModels;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface IPeerEvaluation
    {
        void SavePeerEvaluation(PeerEvaluationHeader header, IEnumerable<PeerEvaluationLine> items, int userid);
        IEnumerable<EmployeeItem> GetAccounts(int userid);
        PeerEvaluationHeader GetHeader(int id);
        IEnumerable<PeerEvaluationLine> GetLineItems(int headerid);
        IEnumerable<PeerEvaluationHeader> EvaluationHeaders(int userid);
        IEnumerable<PeerCriteria> GetCriterias();
    }
}
