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

        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Dislike> Dislikes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
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
