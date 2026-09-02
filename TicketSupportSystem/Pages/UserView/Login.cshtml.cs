using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TicketSupportSystem.Data;
using TicketSupportSystem.Services;
using TicketSupportSystem.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

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
        [BindProperty]
        public bool RemainSignedIn {get; set;}
        public IActionResult OnGet()
        {
            if (User.Identity?.IsAuthenticated == true) return RedirectToPage("/UserView/Dashboard");
            return Page();
        }
        public LoginModel (IHashingService hasher, AppDbContext db)
        {
            _hasher = hasher;
            _db = db;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            Email = Email.Trim().ToLowerInvariant();
            var user = _db.Users.FirstOrDefault(u => u.Email == Email);
            if (user != null)
            {
                var passResult = _hasher.Verify(Password, user.PasswordHash); 
                if (passResult == HashCheckResult.Failed)
                {
                    ModelState.AddModelError("Email", "The email or password is invalid");
                    return Page();
                }
                if (passResult == HashCheckResult.SuccessRehashNeeded)
                {
                    var newPass = _hasher.Hash(Password);
                    user.PasswordHash = newPass;
                    _db.SaveChanges();
                }
            }
            if (user == null) 
            {
                var dummyPass = _hasher.DummyHashVerify(Password); // making response times equal in both cases
                ModelState.AddModelError("Email", "The email or password is invalid");
                    return Page();
            }
                var claims = new List<Claim>
                {
                  new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.UserID)),
                  new Claim(ClaimTypes.Email, user.Email)
                };
                var identity = new ClaimsIdentity(claims, "UserScheme");
                var principal  = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync("UserScheme", principal, new AuthenticationProperties { IsPersistent = RemainSignedIn });
                return RedirectToPage("/UserView/Dashboard");}
    }
}