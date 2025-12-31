using Microsoft.AspNetCore.Mvc;

public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Dashboard()
    {
        var model = new AdminDashboardViewModel
        {
            ParkingSpots = _context.ParkingSpots.ToList(),
            ChargingRequests = _context.ChargingRequests.ToList(),
            MWBot = _context.MWBots.FirstOrDefault()
        };

        return View(model);
    }
}
