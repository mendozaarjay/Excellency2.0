namespace Excellency.Models
{
    public class PeerCriteriaLine
    {
        public int Id { get; set; }
        public virtual PeerCriteriaHeader Header { get; set; }
        public virtual Criteria Criteria { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
