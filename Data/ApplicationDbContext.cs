using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MovieTimeStreaming.Models;

namespace MovieTimeStreaming.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
        


        public DbSet<Media> Media { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<WatchHistory> WatchHistory { get; set; }
         public DbSet<MediaBookmarks> MediaBookmarks { get; set; }
        
    }
}