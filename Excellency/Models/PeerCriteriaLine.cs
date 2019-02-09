namespace Excellency.Models
{
    public class PeerCriteriaLine
    {
        public int Id { get; set; }
        public virtual PeerCriteria Header { get; set; }
        public string Description { get; set; }
        public int Weight { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
