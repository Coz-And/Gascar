public class ChargingRequest
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public int RequestedPercentage { get; set; }
    public string Status { get; set; } // Waiting, Charging, Completed
}
