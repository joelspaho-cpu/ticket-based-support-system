using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace TicketSupportSystem.Pages.UserView
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        [Required, MaxLength(50)]
        public string DisplayName {get; set;} = string.Empty;
        [BindProperty]
        [Required, EmailAddress, MaxLength(254)]
        public string Email {get; set;} = string.Empty;
        [BindProperty]
        [Required, DataType(DataType.Password), MinLength(8), MaxLength(50)]
        public string Password {get; set;} = string.Empty;
        [BindProperty]
        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword {get; set;} = string.Empty;
        [BindProperty]
        public bool Has2fa {get; set;}
        [BindProperty]
        [Required, MaxLength(10)]
        public string Region {get; set;} = string.Empty;
        [BindProperty]
        [Required, MaxLength(10)]
        public string Language {get; set;} = string.Empty;
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
          if (!ModelState.IsValid) return Page();
          // once the info is correct we hash the password, save the user to database and redirect to dashboard
          return Page();
        }
    }
}
