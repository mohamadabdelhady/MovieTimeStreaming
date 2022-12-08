namespace MovieTimeStreaming.Models
{

    public class Media
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateOnly ReleaseDate { get; set; }
        public string Genre { get; set; }
        [DataType(DataType.Text)]
        public string about { get; set; }
        public float Rating { get; set; }
        public string mediaType { get; set; }
        public string mediaImg { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public double MediaDuration { get; set; }
        public string MediaSrc { get; set; }
        public int WatchCount { get; set; } = 0;
        public int SeasonsCount { get; set; }

    }
    
}