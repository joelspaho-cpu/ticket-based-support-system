using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace TicketSupportSystem.Pages.UserView
{
    [Authorize(AuthenticationSchemes = "UserScheme")]
    public class DashboardModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync("UserScheme");
            return RedirectToPage("/UserView/Login");
        }
    }
}
