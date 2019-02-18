using Excellency.Models;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface IRecommendation
    {
        void Save(Recommendation item, int userid);
        IEnumerable<Account> Accounts(int userid);
        RatingHeader RatingById(int id);
        RatingHeader BehavioralEvaluationByEmployee(int id);
        RatingHeader KRAEvaluationByEmployee(int id);
        IEnumerable<RatingBehavioralFactor> BehavioralRatingById(int id);
        IEnumerable<RatingKeySuccessArea> KRARatingById(int id);
        IEnumerable<Recommendation> GetAllRecommendations(int id);
        Recommendation RecommendationById(int id);
        string GetNameById(int id);
        void RemoveById(int id);
    }
}
