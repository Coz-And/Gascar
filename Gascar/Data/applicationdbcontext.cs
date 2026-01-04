using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<ParkingSpot> ParkingSpots { get; set; }
    public DbSet<ChargingRequest> ChargingRequests { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<MWBot> MWBots { get; set; }
    public DbSet<Notification> Notifications { get; set; }

}
