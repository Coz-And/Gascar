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
                    // Inizia la ricarica
                    mwbot.IsAvailable = false;
                    nextRequest.Status = "Charging";
                    context.SaveChanges();

                    // Simula tempo di ricarica (10 secondi)
                    await Task.Delay(10000, stoppingToken);

                    // Fine ricarica
                    nextRequest.Status = "Completed";
                    mwbot.IsAvailable = true;
                    context.SaveChanges();
                }
            }

            await Task.Delay(5000, stoppingToken);
        }
    }
}
