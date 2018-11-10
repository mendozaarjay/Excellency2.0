using Excellency.Models;
using System.Collections.Generic;
namespace Excellency.Interfaces
{
    public interface IEvaluationApproval
    {
        IEnumerable<RatingHeader> GetAssignedEvaluation(int UserId);
        RatingHeader GetRatingHeaderById(int id);
        IEnumerable<RatingBehavioralFactor> GetRatingBehavioralFactorsById(int headerid);
        IEnumerable<RatingKeySuccessArea> GetRatingKeySuccessAreasById(int headerid);

        void Approved(int HeaderId, int UserId, string Remarks);
        void Disapproved(int HeaderId, int UserId, string Remarks);

        KeyResultArea GetKeyResultAreaByHeaderId(int headerid);
        BehavioralFactor GetBehavioralFactorByHeaderId(int headerid);
        string SuccessRating(int headerid, int id);
        int SuccessScore(int headerid, int id);

        int GetBehavioralTotalScore(int headerid);
        int GetKeyResultAreaTotalScore(int headerid);
    }
}
