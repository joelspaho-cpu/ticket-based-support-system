using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TicketSupportSystem.Pages;

public class AccessDeniedModel : PageModel
{
    public IActionResult OnGet()
    {
        return Page();
    }
}

