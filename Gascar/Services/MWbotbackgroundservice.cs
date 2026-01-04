using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class MWBotBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public MWBotBackgroundService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var mwbot = context.MWBots.FirstOrDefault();
            if (mwbot == null)
            {
                await Task.Delay(5000, stoppingToken);
                continue;
            }

            if (mwbot.IsAvailable)
            {
                var nextRequest = context.ChargingRequests
                    .Where(r => r.Status == "Waiting")
                    .OrderBy(r => r.Id)
                    .FirstOrDefault();

                if (nextRequest != null)
                {
                    //INIZIO RICARICA
                    mwbot.IsAvailable = false;
                    nextRequest.Status = "Charging";
                    context.SaveChanges();

                    //Recupero auto
                    var car = context.Cars.Find(nextRequest.CarId);

                    //Calcolo energia da caricare (kWh)
                    double energyNeeded =
                        car.BatteryCapacityKw *
                        (nextRequest.RequestedPercentage - car.CurrentChargePercentage) / 100.0;

                    //Calcolo tempo di ricarica (ore)
                    double hoursNeeded = energyNeeded / mwbot.PowerKw;

                    //Conversione in millisecondi (tempo ridotto per demo)
                    int milliseconds = (int)(hoursNeeded * 3600 * 1000);
                    milliseconds /= 360; // 1 ora reale = 10 secondi demo

                    await Task.Delay(milliseconds, stoppingToken);

                    //FINE RICARICA
                    car.CurrentChargePercentage = nextRequest.RequestedPercentage;
                    nextRequest.Status = "Completed";
                    mwbot.IsAvailable = true;
                    context.SaveChanges();
                }
            }

            await Task.Delay(5000, stoppingToken);
        }
    }
}
