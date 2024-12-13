using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataModel;

public partial class FlowerDbContext : IdentityDbContext<AppUser>
{
    public FlowerDbContext()
    {
    }

    public FlowerDbContext(DbContextOptions<FlowerDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Genus> Genuses { get; set; } = null!;
    public virtual DbSet<Species> Species { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false);
        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Genus>(entity =>
        {
            entity.ToTable("Genera");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.ScientificName)
                .HasColumnName("scientificName")
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.ColloquialName)
                .HasColumnName("colloquialName")
                .HasMaxLength(50)
                .IsRequired();
        });

        modelBuilder.Entity<Species>(entity =>
        {
            entity.ToTable("Species");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.ScientificName)
                .HasColumnName("scientificName")
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.ColloquialName)
                .HasColumnName("colloquialName")
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.GenusId)
                .HasColumnName("genusId");

            entity.HasOne(d => d.Genus)
                .WithMany(p => p.Species)
                .HasForeignKey("GenusId")
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Species_Genus");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}