using Excellency.Models;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface IEvaluationSeason
    {
        void Save(EvaluationSeason season, int userid);
        void RemoveById(int id);
        void SetActive(int id);
        IEnumerable<EvaluationSeason> Seasons();
        EvaluationSeason EvaluationSeasonById(int id);
        void CloseAllExisting();
    }
}
