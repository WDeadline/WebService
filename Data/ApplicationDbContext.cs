using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Models;

namespace WebService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            modelBuilder.Entity<Appreciation>()
                   .HasKey(appreciation => new { appreciation.ApplicationUserID, appreciation.DestinationID });
        }

        public DbSet<WebService.Models.ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<WebService.Models.Appreciation> Appreciations { get; set; }
        public DbSet<WebService.Models.Destination> Destinations { get; set; }
        public DbSet<WebService.Models.Location> Locations { get; set; }
        public DbSet<WebService.Models.Picture> Pictures { get; set; }

    }
}
