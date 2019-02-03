namespace Excellency.Models
{
    public class EvaluationInterpretation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Score { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal ScoreFrom { get; set; }
        public decimal ScoreTo { get; set; }
    }
}
