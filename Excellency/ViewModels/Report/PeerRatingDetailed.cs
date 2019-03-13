namespace Excellency.ViewModels
{
    public class PeerRatingDetailed
    {
        public int Id { get; set; }
        public string Period { get; set; }
        public string Rater { get; set; }
        public string Name { get; set; }
        public decimal Weight { get; set; }
        public decimal Score { get; set; }
        public decimal TotalScore { get; set; }
    }
}
