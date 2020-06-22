using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortfolioWeb.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioWeb.Database
{
    public class PortfolioDbContext : IdentityDbContext<PortfolioAppUser>
    {
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProjectStatus>().HasData(
                new ProjectStatus()
                {
                    ID = 1,
                    Name = "Gepasseerd"
                },
                new ProjectStatus()
                {
                    ID = 2,
                    Name = "Mee bezig"
                },
                new ProjectStatus()
                {
                    ID = 3,
                    Name = "Toekomstig"
                });

            modelBuilder.Entity<PortfolioTag>()
                .HasKey(mt => new { mt.PortfolioID, mt.TagId });

            modelBuilder.Entity<PortfolioTag>()
                .HasOne(mt => mt.Portfolio)
                .WithMany(m => m.PortfolioTags)
                .HasForeignKey(mt => mt.PortfolioID);

            modelBuilder.Entity<PortfolioTag>()
                .HasOne(mt => mt.Tag)
                .WithMany(t => t.PortfolioTags)
                .HasForeignKey(mt => mt.TagId);

            modelBuilder.Entity<Tag>()
                .HasData(
                new Tag() { Id = 1, Name = "Verschietend" },
                new Tag() { Id = 2, Name = "Grappig" },
                new Tag() { Id = 3, Name = "Nice" },
                new Tag() { Id = 4, Name = "Demo" });
        }

        public DbSet<Portfolio> Movies { get; set; }
        public DbSet<ProjectStatus> ProjectStatuses { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PortfolioTag> MovieTags { get; set; }
    }
}
