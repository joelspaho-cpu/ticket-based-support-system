using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TicketSupportSystem.Pages.UserView
{
    public class DashboardModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
