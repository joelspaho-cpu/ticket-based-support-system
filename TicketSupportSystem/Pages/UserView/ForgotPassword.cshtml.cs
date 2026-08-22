using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TicketSupportSystem.Pages.UserView
{
    public class ForgotPasswordModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
