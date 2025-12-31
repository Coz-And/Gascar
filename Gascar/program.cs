var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("gascardb"));

builder.Services.AddScoped<IChargingService, ChargingService>();

builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();
