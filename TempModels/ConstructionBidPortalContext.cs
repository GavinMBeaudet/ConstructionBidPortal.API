using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ConstructionBidPortal.API.TempModels;

public partial class ConstructionBidPortalContext : DbContext
{
    public ConstructionBidPortalContext()
    {
    }

    public ConstructionBidPortalContext(DbContextOptions<ConstructionBidPortalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bid> Bids { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=ConstructionBidPortal.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bid>(entity =>
        {
            entity.HasIndex(e => e.ContractorId, "IX_Bids_ContractorId");

            entity.HasIndex(e => e.ProjectId, "IX_Bids_ProjectId");

            entity.HasOne(d => d.Contractor).WithMany(p => p.Bids).HasForeignKey(d => d.ContractorId);

            entity.HasOne(d => d.Project).WithMany(p => p.Bids).HasForeignKey(d => d.ProjectId);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasIndex(e => e.OwnerId, "IX_Projects_OwnerId");

            entity.HasOne(d => d.Owner).WithMany(p => p.Projects).HasForeignKey(d => d.OwnerId);

            entity.HasMany(d => d.Categories).WithMany(p => p.Projects)
                .UsingEntity<Dictionary<string, object>>(
                    "ProjectCategory",
                    r => r.HasOne<Category>().WithMany().HasForeignKey("CategoryId"),
                    l => l.HasOne<Project>().WithMany().HasForeignKey("ProjectId"),
                    j =>
                    {
                        j.HasKey("ProjectId", "CategoryId");
                        j.ToTable("ProjectCategory");
                        j.HasIndex(new[] { "CategoryId" }, "IX_ProjectCategory_CategoryId");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
