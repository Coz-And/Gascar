var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("gascardb"));

builder.Services.AddScoped<IChargingService, ChargingService>();

builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

db.ParkingSpots.AddRange(
    new ParkingSpot { IsOccupied = false },
    new ParkingSpot { IsOccupied = true }
);

db.MWBots.Add(new MWBot { IsAvailable = true });

db.SaveChanges();
