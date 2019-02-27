using Excellency.Models;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface IRecommendation
    {
        void Save(Recommendation item, int userid);
        IEnumerable<Account> Accounts(int userid);
        Account GetAccountById(int id);
        RatingHeader RatingById(int id);
        RatingHeader BehavioralEvaluationByEmployee(int id);
        RatingHeader KRAEvaluationByEmployee(int id);
        IEnumerable<RatingBehavioralFactor> BehavioralRatingById(int id);
        IEnumerable<RatingKeySuccessArea> KRARatingById(int id);
        IEnumerable<Recommendation> GetAllRecommendations(int id);
        Recommendation RecommendationById(int id);
        Recommendation RecommendationByEmployeeId(int id);
        string GetNameById(int id);
        void RemoveById(int id);
        bool IsWithRecommendation(int id);
        bool IsWithActiveSeason();
        EvaluationSeason ActiveSeason();
    }
}
