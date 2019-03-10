using Excellency.Models;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface IRecommendationAssignment
    {
        void Save(RecommendationAssignment item, int userid);
        IEnumerable<Account> Recommender();
        IEnumerable<Account> Employees(int id);
        IEnumerable<RecommendationAssignment> RecommendationAssignments(int id);
        void RemoveById(int id);
        Account GetAccountById(int id);
        string GetNameById(int id);
    }
}
