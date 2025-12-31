public interface IChargingService
{
    void RequestCharging(int carId, int percentage);
    void CompleteCharging(int requestId);
}
