namespace Excellency.Models
{
    public class RatingKeySuccessArea
    {
        public int Id { get; set; }
        public virtual RatingHeader RatingHeader { get; set; }
        public virtual KeyResultArea KeyResultArea { get; set; }
        public virtual KeySuccessIndicator KeySuccessIndicator { get; set; }
        public int Score { get; set; }
        public string Comment { get; set; }
        
    }
}
