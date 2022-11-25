namespace MovieTimeStreaming.Models
{
    public class Reviews
    {
        [Key]
        public int ID { get; set; }
        public string UserId { get; set; }
        public string MediaId { get; set; }
        public string UserReview { get; set; }
        public string UserRating { get; set; }
        
        public DateTime CreatedAt { get; set; }
    }
}