using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        int userId = 1; // simulato

        var model = new UserDashboardViewModel
        {
            Notifications = _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.Date)
                .ToList(),

            ChargingRequests = _context.ChargingRequests
                .Where(r => r.UserId == userId)
                .ToList()
        };

        return View(model);
    }
}
