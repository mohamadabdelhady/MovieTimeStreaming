namespace MovieTimeStreaming.Models
{
    public class AdminData
    {
        [Key]
        public string id { get; set; }

        public string UserName { get; set; }
        public bool IsSupervisor { get; set; } = false;
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}