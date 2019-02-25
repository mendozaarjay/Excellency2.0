using Excellency.Models;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface IBehavioralFactor
    {
        void SaveBehavioralFactor(BehavioralFactor behavioralFactor);
        void RemoveBehavioralFactorById(int id);
        BehavioralFactor GetBehavioralFactorById(int id);
        IEnumerable<BehavioralFactor> GetBehavioralFactors();

        void SaveFactorItem(BehavioralFactorItem factorItem);
        void RemoveFactorItemById(int id);
        BehavioralFactorItem GetBehavioralFactorItemById(int id);
        IEnumerable<BehavioralFactorItem> GetBehavioralFactorItemsByHeaderId(int id);

        IEnumerable<EmployeeCategory> EmployeeCategories();
        EmployeeCategory GetEmployeeCategoryById(int id);
        bool IsWithActiveSeason();
        EvaluationSeason ActiveSeason();

    }
}
