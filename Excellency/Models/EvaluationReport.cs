namespace Excellency.Models
{
    public class EvaluationReport
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Rater { get; set; }
        public string DateCreated { get; set; }
        public string Period { get; set; }
        public string Status { get; set; }
    }
}
