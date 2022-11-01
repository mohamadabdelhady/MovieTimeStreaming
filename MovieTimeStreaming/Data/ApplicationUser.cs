using Microsoft.AspNetCore.Identity;

namespace MovieTimeStreaming.Data;

public class ApplicationUser:IdentityUser
{
    public string ProfileImage { get; set; }
}