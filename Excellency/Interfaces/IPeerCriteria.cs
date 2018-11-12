using Excellency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.Interfaces
{
    public interface IPeerCriteria
    {
        void SavePeerCriteria(PeerCriteriaHeader header, int UserId);

    }
}
