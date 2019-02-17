using System.ComponentModel.DataAnnotations.Schema;

namespace Excellency.ViewModels
{
    public class ApprovalLevelItemViewModel
    {
        [NotMapped]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int FirstApprovalId { get; set; }
        [NotMapped]
        public int SecondApprovalId { get; set; }
        public bool IsWithSecondApproval { get; set; }
    }
}
