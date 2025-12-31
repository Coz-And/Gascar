public class PaymentService
{
    public double CalculateParkingCost(double hours, double pricePerHour)
    {
        return hours * pricePerHour;
    }

    public double CalculateChargingCost(double kw, double pricePerKw)
    {
        return kw * pricePerKw;
    }
}
