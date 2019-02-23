using System;

namespace Excellency.ViewModels
{
    public class EvaluationSeasonItem
    {
        public int Id { get; set; }
        public string Title { get; set; }  
        public string Remarks { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
