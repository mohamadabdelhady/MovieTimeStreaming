
namespace MovieTimeStreaming.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        
        public string firstName { get; set; }
        public string lastName { get; set; }
        
        public string email { get; set; }

        public string profile_img { get; set; }
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        public string password { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateUpdated { get; set; }

    }
}