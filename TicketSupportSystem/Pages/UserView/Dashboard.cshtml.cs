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
    public class DashboardModel : PageModel
    {
        private readonly AppDbContext _db;
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
        public DashboardModel(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var idClaim = int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int id);
            if (idClaim) {
            var results = await _db.Tickets.Where(q => q.UserID == id).OrderByDescending(q => q.CreatedAt).ToListAsync();
            Tickets.AddRange(results); 
            }
            else { TempData["ErrorMessage"] = "Session expired"; 
            await HttpContext.SignOutAsync("UserScheme");
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
