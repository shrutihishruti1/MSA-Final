using Microsoft.EntityFrameworkCore;
using MSA_Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSA_Final.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Viewer> Viewers { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Content>()
                .HasOne(p => p.Viewer)
                .WithMany(s => s.Contents)
                .HasForeignKey(p => p.ViewerId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Viewer)
                .WithMany(s => s.Comments)
                .HasForeignKey(c => c.ViewerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Content)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.ContentId);
        }
    }
}
