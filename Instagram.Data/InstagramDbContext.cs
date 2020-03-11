using System;
using Instagram.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Data
{
    public class InstagramDbContext : DbContext
    {
        public InstagramDbContext(DbContextOptions<InstagramDbContext> options)
            : base(options)
        { }

        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<Dislike> Dislikes { get; set; }
        public virtual DbSet<Follow> Follows { get; set; }

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

            builder.Entity<Like>()
                .HasOne(l => l.Post)
                .WithMany(p => p.Likes);

            builder.Entity<Dislike>()
                .HasOne(d => d.Post)
                .WithMany(p => p.Dislikes);

            builder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments);
                

            base.OnModelCreating(builder);
        }
    }
}
