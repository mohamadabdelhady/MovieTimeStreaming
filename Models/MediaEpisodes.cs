namespace MovieTimeStreaming.Models
{
    public class MediaEpisodes
    {
        [Key]
        public int ID { get; set; }

        public string SeriesMediaId { get; set; }
        public int EpisodeNum { get; set; }
        public double MediaDuration { get; set; }
        public string MediaSrc { get; set; }
        public int WatchCount { get; set; } = 0;
        public DateTime CreatedAt { get; set; }
    }
}