using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Excellency.ViewModels
{
    public class EvaluationSeasonItem
    {
        public int Id { get; set; }
        public string Title { get; set; }  
        public string Remarks { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [NotMapped]
        public string CreatedBy { get; set; }
        [NotMapped]
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
    }
}
