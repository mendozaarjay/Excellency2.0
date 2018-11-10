namespace Excellency.Models
{
    public class AccountRoleAssignment
    {
        public int Id { get; set; }
        public virtual AccountRole Role { get; set; }
        public virtual Account Account { get; set; }
        public bool IsDeleted { get; set; }
    }
}
