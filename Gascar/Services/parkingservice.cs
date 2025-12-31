public class ParkingService
{
    private readonly ApplicationDbContext _context;

    public ParkingService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<ParkingSpot> GetParkingStatus()
    {
        return _context.ParkingSpots.ToList();
    }
}
