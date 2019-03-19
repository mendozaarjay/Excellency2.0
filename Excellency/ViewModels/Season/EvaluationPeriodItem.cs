using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Excellency.ViewModels
{
    public class EvaluationPeriodItem : IValidatableObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        public string Remarks { get; set; }
        [Required(ErrorMessage = "Start Date is required.")] 
        [DataType(DataType.Date,ErrorMessage = "Invalid format for start date.")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid format for end date.")]
        public DateTime EndDate { get; set; }
        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            List<ValidationResult> res = new List<ValidationResult>();
            if (EndDate <= StartDate)
            {
                ValidationResult mss = new ValidationResult("End date should be greater than start date.");
                res.Add(mss);

            }
            return res;
        }
    }
}
