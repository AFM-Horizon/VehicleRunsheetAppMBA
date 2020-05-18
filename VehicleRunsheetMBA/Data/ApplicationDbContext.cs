using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VehicleRunsheetMBA.Models;
using VehicleRunsheetMBAProj.Models;

namespace VehicleRunsheetMBAProj.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Runsheet> Runsheets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<VehicleDetails> VehicleDetails { get; set; }
    }
}
