using Excellency.Models;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface IPeerCriteria
    {
        void SavePeerCriteria(PeerCriteriaHeader header, int UserId);

        PeerCriteriaHeader GetPeerCriteriaHeaderById(int id);

        IEnumerable<PeerCriteriaHeader> PeerCriteriaHeaders();

        void SaveCriteriaLine(int HeaderId, List<PeerCriteriaLine> items);
        void SaveCriteriaLine(int HeaderId, PeerCriteriaLine item);
        void RemoveLineById(int id);
        void RemoveHeaderById(int id);

        IEnumerable<PeerCriteriaLine> PeerCriteriaLinesByHeaderId(int HeaderId);


    }
}
