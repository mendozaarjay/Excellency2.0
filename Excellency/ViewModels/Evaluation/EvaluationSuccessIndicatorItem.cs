using System.Collections.Generic;

namespace Excellency.ViewModels
{
    public class EvaluationSuccessIndicatorItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Weight { get; set; }
        public IEnumerable<EvaluationRatingTableItem> RatingTableItems { get; set; }
        public int Score { get; set;}
        public string Comment { get; set; }
    }
}
