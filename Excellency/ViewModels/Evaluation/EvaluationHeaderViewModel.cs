using System.ComponentModel.DataAnnotations;

namespace Excellency.ViewModels
{
    public class EvaluationHeaderViewModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int KeyResultAreaId { get; set; }
        [MaxLength(500,ErrorMessage = "Remarks should be less than or equal to 500 characters.")]
        public string Remarks { get; set; }
    }
}
