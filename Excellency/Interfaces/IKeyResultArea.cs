using Excellency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.Interfaces
{
    public interface IKeyResultArea
    {
        #region Key Result Area
        void Add(KeyResultArea keyResultArea);
        void Update(KeyResultArea keyResultArea);
        void RemoveById(int id);
        KeyResultArea GetKeyResultAreaById(int id);
        IEnumerable<KeyResultArea> GetAllKeyResultArea();
        #endregion

        #region Key Success Indicator
        void SaveKeySuccessIndicator(KeySuccessIndicator successIndicator,string UserId);
        void RemoveSuccessIndicatorById(int id);
        KeySuccessIndicator GetKeySuccessIndicatorById(int id);
        IEnumerable<KeySuccessIndicator> SuccessIndicatorPerKRA(int KRAId);
        IEnumerable<RatingTable> GetRatingTables();
        RatingTable GetRatingTablePerId(int id);
        IEnumerable<RatingTableItem> RatingTableItems(int RatingTableId);
        #endregion

        #region Category
        void SaveCategory(Category category,string UserId);
        void RemoveCategoryPerId(int id);
        Category GetCategoryById(int id);
        IEnumerable<Category> CategoriesPerKSIId(int KSIId); 
        #endregion


    }
}
