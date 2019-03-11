namespace Excellency.Models
{
    public class AppraisalHistory
    {
        public int Id { get; set; }
        public string Period { get; set; }
        public string Name { get; set; }
        public decimal TotalScore { get; set; }
        public decimal TotalWeight { get; set; }
        public decimal ConvertedScore { get; set; }
        public decimal WeightedScore { get; set; }
        public decimal Percentage { get; set; }
    }
}
