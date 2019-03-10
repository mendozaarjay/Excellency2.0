using Excellency.Models;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface IReport
    {
        IEnumerable<EvaluationReport> Evaluations(int periodid,string type);
        IEnumerable<EvaluationSeason> EvaluationSeasons();
    }
}
