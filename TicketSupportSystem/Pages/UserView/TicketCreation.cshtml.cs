using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketSupportSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using TicketSupportSystem.Data;


namespace TicketSupportSystem.Pages.UserView
{
    [Authorize(AuthenticationSchemes = "UserScheme")]
    public class TicketCreationModel : PageModel
    {
        private readonly AppDbContext _db; 
        [BindProperty]
        [Required, MaxLength(30)]
        public string Subject {get; set;} = string.Empty;
        [BindProperty]
        [Required, MaxLength(5000)]
        public string Description {get; set;} = string.Empty;
        [BindProperty]
        [Required]
        public TicketQuery Query {get; set;} = 0;
        public TicketCreationModel (AppDbContext db)
        {
            _db = db;
        }
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            var idClaim = int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int result);
            if (idClaim && result != 0) {
            Ticket ticket = new Ticket{
                UserID = result,
                Subject = Subject,
                Description = Description,
                Query = Query,
                IPAddress = ip,
                CreatedAt = DateTime.UtcNow
            };
            _db.Tickets.Add(ticket);
            await _db.SaveChangesAsync();
            }
            else { 
                TempData["ErrorMessage"] = "Session expired";
                await HttpContext.SignOutAsync("UserScheme");
                return RedirectToPage("/UserView/Login");
                }
            TempData["SuccessMessage"] = "Submitted Successfully";
            return RedirectToPage("/UserView/Dashboard");
        }
    }
}
