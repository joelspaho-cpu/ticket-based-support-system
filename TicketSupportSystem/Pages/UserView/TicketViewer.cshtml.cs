using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using TicketSupportSystem.Data;
using TicketSupportSystem.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace TicketSupportSystem.Pages.UserView
{
    [Authorize(AuthenticationSchemes = "UserScheme")]
    public class TicketViewerModel : PageModel
    {
        private readonly AppDbContext _db;
        public Ticket ticket;
        public TicketViewerModel(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
           var idClaim = int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int result);
           if (idClaim)
            {
                var final = await _db.Tickets.FirstOrDefaultAsync(q => q.UserID == result &&
                                                                        q.TicketID == id);
                if (final != null) { ticket = final; }
                else { TempData["ErrorMessage"] = "Ticket not found"; return RedirectToPage("Dashboard"); }                                                           
            } else {  await HttpContext.SignOutAsync("UserScheme");
                return RedirectToPage("/UserView/Login"); }
            return Page();
        }
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync("UserScheme");
            return RedirectToPage("/UserView/Login");
        }
    }
}
