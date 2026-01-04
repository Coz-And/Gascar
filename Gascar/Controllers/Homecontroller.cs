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
        //per ora userId simulato
        int userId = 1;

        var notifications = _context.Notifications
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.Date)
            .ToList();

        //(opzionale) segna come lette
        foreach (var n in notifications.Where(n => !n.IsRead))
        {
            n.IsRead = true;
        }
        _context.SaveChanges();

        return View(notifications);
    }
}
