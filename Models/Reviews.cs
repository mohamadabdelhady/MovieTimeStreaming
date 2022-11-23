namespace MovieTimeStreaming.Models
{
    public class Reviews
    {
        [Key]
        public int ID { get; set; }
        public int UserId { get; set; }
        public int MediaId { get; set; }
        public string UserReview { get; set; }
        public string UserRating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}