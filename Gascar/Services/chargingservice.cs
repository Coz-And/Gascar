public class ChargingService : IChargingService
{
    private readonly ApplicationDbContext _context;

    public ChargingService(ApplicationDbContext context)
    {
        _context = context;
    }

    public void RequestCharging(int carId, int percentage)
    {
        var request = new ChargingRequest
        {
            CarId = carId,
            RequestedPercentage = percentage,
            Status = "Waiting"
        };

        _context.ChargingRequests.Add(request);
        _context.SaveChanges();
    }

    public void CompleteCharging(int requestId)
    {
        var request = _context.ChargingRequests.Find(requestId);
        request.Status = "Completed";
        _context.SaveChanges();
    }
}
   