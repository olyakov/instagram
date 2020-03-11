using Instagram.Data.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Data
{
    public class IdentityDbContext : IdentityDbContext<AspNetUsers>
    {
        public DbSet<AspNetUsers> ApplicationUsers { get; set; }

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Follow>()
                .HasKey(f => new { f.FollowingId, f.FollowerId });

            builder.Entity<AspNetUsers>()
                .HasMany(u => u.Followers)
                .WithOne(f => f.Following)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AspNetUsers>()
                .HasMany(u => u.Followings)
                .WithOne(f => f.Follower)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

    }
}
