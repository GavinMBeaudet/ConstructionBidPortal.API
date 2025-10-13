using Microsoft.EntityFrameworkCore;
using ConstructionBidPortal.API.Models;

namespace ConstructionBidPortal.API.Data
{
    public class BidPortalContext : DbContext
    {
        public BidPortalContext(DbContextOptions<BidPortalContext> options) : base(options) { }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the composite key for ProjectCategory
            modelBuilder.Entity<ProjectCategory>()
                .HasKey(pc => new { pc.ProjectId, pc.CategoryId });
                
            // Configure relationship between ProjectCategory and Project
            modelBuilder.Entity<ProjectCategory>()
                .HasOne(pc => pc.Project)
                .WithMany(p => p.ProjectCategories)
                .HasForeignKey(pc => pc.ProjectId);
                
            // Configure relationship between ProjectCategory and Category
            modelBuilder.Entity<ProjectCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.ProjectCategories)
                .HasForeignKey(pc => pc.CategoryId);
        }
    }
}
