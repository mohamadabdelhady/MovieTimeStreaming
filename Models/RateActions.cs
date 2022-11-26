namespace MovieTimeStreaming.Models
{
    public class RateActions
    {
        [Key] public int ID { get; set; }
        public string UserId { get; set; }

        public string MediaId { get; set; }

        //true=like and false=dis like
        public bool RateType { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}