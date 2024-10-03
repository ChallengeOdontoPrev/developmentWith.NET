using Microsoft.EntityFrameworkCore;
using OdontoPrev.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace OdontoPrev.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Posts)
                .WithOne(p => p.Author)
                .HasForeignKey(p => p.AuthorId);

            // You can add more configurations here if needed
        }
    }
}