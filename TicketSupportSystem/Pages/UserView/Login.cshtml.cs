using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TicketSupportSystem.Data;
using TicketSupportSystem.Services;
using TicketSupportSystem.Models;

namespace TicketSupportSystem.Pages.UserView
{
    public class LoginModel : PageModel
    {
        private readonly IHashingService _hasher;
        private readonly AppDbContext _db;
        [BindProperty]
        [Required, EmailAddress]
        public string Email {get; set;} = string.Empty;
        [BindProperty]
        [Required, DataType(DataType.Password)]
        public string Password {get; set;} = string.Empty;
        public IActionResult OnGet()
        {
            return Page();
        }
        public LoginModel (IHashingService hasher, AppDbContext db)
        {
            _hasher = hasher;
            _db = db;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();
            Email = Email.Trim().ToLowerInvariant();
            var user = _db.Users.FirstOrDefault(u => u.Email == Email);
            if (user != null)
            {
                var passResult = _hasher.Verify(Password, user.PasswordHash);
                switch (passResult)
                {
                    case HashCheckResult.Success:
                       return RedirectToPage("Dashboard");
                    case HashCheckResult.Failed:
                        ModelState.AddModelError("Email", "The email or password is invalid.");
                        return Page();
                    case HashCheckResult.SuccessRehashNeeded:
                        var newPass = _hasher.Hash(Password);
                        user.PasswordHash = newPass;
                        _db.SaveChanges();
                        return RedirectToPage("Dashboard");
                }
                } else {  
                var passResult = _hasher.DummyHashVerify(Password); // making response times equal in both branches
                ModelState.AddModelError("Email", "The email or password is invalid.");
                return Page(); 
            }
            return Page();  
        }
    }
}
