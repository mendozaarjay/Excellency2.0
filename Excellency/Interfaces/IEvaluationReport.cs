using Excellency.Models;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface IEvaluationReport
    {
        IEnumerable<EmployeeEvaluation> GetAllRatingPerUser(int id);
        EvaluationInterpretation InterpretationPerEmployee(int id);
        IEnumerable<EvaluationResultItem> GetResultPerEmployee(int id);
    }
}
