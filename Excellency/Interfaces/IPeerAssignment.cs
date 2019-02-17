using Excellency.Models;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface IPeerAssignment
    {
        void Save(PeerAssignment peerAssignment,int UserId);
        IEnumerable<PeerAssignment> GetAssignmentsPerRater(int id);
        Account GetAccountById(int id);
        IEnumerable<Account> GetAllAccountsByRaterId(int id);
        IEnumerable<Account> GetAllAccounts();
        PeerAssignment PeerAssignmentById(int id);
        void RemoveById(int id);
    }
}
