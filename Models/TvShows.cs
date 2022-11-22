namespace MovieTimeStreaming.Models
{
    public class TvShows
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        [DataType(DataType.Text)]
        public string about { get; set; }
        public float Rating { get; set; }
    }
}