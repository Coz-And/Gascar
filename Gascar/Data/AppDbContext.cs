using Microsoft.EntityFrameworkCore;
using Gascar.Models;

namespace Gascar.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Parking> Parking { get; set; }
        public DbSet<ChargingRequest> ChargingRequests { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}
