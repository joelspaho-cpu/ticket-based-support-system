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
        public Ticket Ticket;
        public List<Message> Messages { get; set; } = new List<Message>();
        [BindProperty]
        public required string ReplyText {get; set;} = string.Empty;
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
                var messagesResult = await _db.Messages.Where(q => q.IsInternal == false && q.TicketID == id).Include(m => m.ResponseByUser).Include(m => m.ResponseByStaff).OrderBy(q => q.PostedAt).ToListAsync();
                if (messagesResult != null) { Messages = messagesResult; }
                if (final != null) { Ticket = final; }
                else { TempData["ErrorMessage"] = "Ticket not found"; return RedirectToPage("Dashboard"); }                                                           
            } else {  await HttpContext.SignOutAsync("UserScheme");
                return RedirectToPage("/UserView/Login"); }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var idClaim = int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int result);
                                                                   
            if (ModelState.IsValid)
            {
                if(idClaim){
                var final = await _db.Tickets.FirstOrDefaultAsync(q => q.UserID == result &&
                                                                        q.TicketID == id);
                var messagesResult = await _db.Messages.Where(q => q.IsInternal == false && q.TicketID == id).Include(m => m.ResponseByUser).Include(m => m.ResponseByStaff).OrderBy(q => q.PostedAt).ToListAsync();
                if (messagesResult != null) { Messages = messagesResult; } else { return RedirectToPage("/UserView/Dashboard"); } 
                if (final != null) { Ticket = final; }  else { return RedirectToPage("/UserView/Dashboard"); }
                Message message = new Message
                {
                    Response = ReplyText,
                    TicketID = Ticket.TicketID,
                    ResponseByUserID = result,
                    PostedAt = DateTime.UtcNow,
                    IsInternal = false,
                    IPAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown"
                };
                _db.Messages.Add(message);
                await _db.SaveChangesAsync();
                return RedirectToPage($"/UserView/TicketViewer", new { id });
                }
                else
                {
                    TempData["ErrorMessage"] = "Your session has expired";
                    return RedirectToPage("/UserView/Login");
                }
            }
            else { ModelState.AddModelError("ReplyText", "Please write a reply before submitting."); return Page(); }   
        }
        
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync("UserScheme");
            return RedirectToPage("/UserView/Login");
        }
    }
}
