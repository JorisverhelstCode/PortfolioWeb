using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortfolioWeb.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioWeb.Database
{
    public class PortfolioDbContext : IdentityDbContext<ProjectAppUser>
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

            modelBuilder.Entity<ProjectTag>()
                .HasKey(mt => new { mt.PortfolioID, mt.TagId });

            modelBuilder.Entity<ProjectTag>()
                .HasOne(mt => mt.Portfolio)
                .WithMany(m => m.ProjectTags)
                .HasForeignKey(mt => mt.PortfolioID);

            modelBuilder.Entity<ProjectTag>()
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

        public DbSet<Project> Movies { get; set; }
        public DbSet<ProjectStatus> ProjectStatuses { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProjectTag> MovieTags { get; set; }
    }
}
