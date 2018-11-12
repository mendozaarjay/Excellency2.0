using Excellency.Models;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface ICriteria
    {
        void Save(Criteria criteria, int UserId);
        void Remove(int id);
        Criteria GetCriteriaById(int id);
        IEnumerable<Criteria> GetCriterias();
    }
}
