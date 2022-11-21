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
        public DbSet<Anime> Anime { get; set; }
        public DbSet<Documentaries> Documentaries { get; set; }
        public DbSet<Movies> Movies { get; set; }
        public DbSet<Anime> TvShows { get; set; }
        
    }
}