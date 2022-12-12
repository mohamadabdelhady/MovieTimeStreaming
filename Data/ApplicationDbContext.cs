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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        
            string ADMIN_ID = "02174cf0–9412–4cfe-afbf-59f706d72cf6";
            string ROLE_ID = "341743f0-asd2–42de-afbf-59kmkkmk72cf6";
            string ROLE2_ID = "02174cf0-asd2–42de-afbf-59kmkkmk72cf6";

            //seed admin role
            builder.Entity<IdentityRole>().HasData(new IdentityRole { 
                Name = "SuperAdmin", 
                NormalizedName = "SUPERADMIN", 
                Id = ROLE_ID,
                ConcurrencyStamp = ROLE_ID
            });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { 
                Name = "Admin", 
                NormalizedName = "ADMIN", 
                Id = ROLE2_ID,
                ConcurrencyStamp = ROLE2_ID
            });

            //create user
            var appUser = new ApplicationUser() { 
                Id = ADMIN_ID,
                Email = "Admin@admin.com",
                EmailConfirmed = true,
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                NormalizedEmail="ADMIN@ADMIN.com"
            };

            //set user password
            PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
            appUser.PasswordHash = ph.HashPassword(appUser, "KKr%@cTU2b#TtKDz");

            //seed user
            builder.Entity<ApplicationUser>().HasData(appUser);

            //set user role to admin
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { 
                RoleId = ROLE_ID, 
                UserId = ADMIN_ID 
            });
        }

        public DbSet<Media> Media { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<WatchHistory> WatchHistory { get; set; }
         public DbSet<MediaBookmarks> MediaBookmarks { get; set; }
         public DbSet<MediaEpisodes> MediaEpisodes { get; set; }
        
    }
}