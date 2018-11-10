using Excellency.Models;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface IRating
    {
        void Add(Rating rating);
        void Update(Rating rating);
        void RemoveById(int id);
        IEnumerable<Rating> GetAllRatings();
        Rating GetRatingById(int id);

    }
}
