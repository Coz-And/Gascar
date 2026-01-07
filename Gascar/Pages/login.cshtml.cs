using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class LoginModel : PageModel
{
    [BindProperty]
    public string Username { get; set; }

    [BindProperty]
    public string Password { get; set; }

    public IActionResult OnPost()
    {
        if (Username == "admin" && Password == "admin123")
        {
            return RedirectToPage("/Admin");
        }

        return Page();
    }
     public string ErrorMessage { get; set; }
}

