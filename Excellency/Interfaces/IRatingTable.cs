using Excellency.Models;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface IRatingTable
    {
        void Save(RatingTable item,string UserId);
        void RemoveById(int id);
        RatingTable GetRatingTableById(int id);
        IEnumerable<RatingTable> RatingTables();
        void AddItem(RatingTableItem item);
        void RemoveItemPerId(int id);
        RatingTableItem GetTableItemPerId(int id);
        IEnumerable<RatingTableItem> TableItemsPerId(int RatingTableId);
        bool IsWithActiveSeason();
        EvaluationSeason ActiveSeason();
    }
}
