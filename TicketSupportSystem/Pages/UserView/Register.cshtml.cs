using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace TicketSupportSystem.Pages.UserView
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Please enter a valid display name"), MaxLength(50)]
        public string DisplayName {get; set;} = string.Empty;
        [BindProperty]
        [Required(ErrorMessage = "Please enter a valid email"), EmailAddress, MaxLength(254)]
        public string Email {get; set;} = string.Empty;
        [BindProperty]
        [Required(ErrorMessage = "Please enter a valid password"), DataType(DataType.Password), MinLength(8), MaxLength(100)]
        public string Password {get; set;} = string.Empty;
        [BindProperty]
        [Required(ErrorMessage = "Please ensure the passwords match"), DataType(DataType.Password), Compare(nameof(Password)), MinLength(8), MaxLength(100)]
        public string ConfirmPassword {get; set;} = string.Empty;
        [BindProperty]
        public bool Has2fa {get; set;}
        [BindProperty]
        [Required(ErrorMessage = "Please select your region from the dropdown list"), MaxLength(10)]
        public string Region {get; set;} = string.Empty;
        [BindProperty]
        [Required(ErrorMessage = "Please select your language from the dropdown list"), MaxLength(10)]
        public string Language {get; set;} = string.Empty;
        public IActionResult OnGet()
        {
            return Page();
        }
        public IActionResult OnPost()
        {
          if (!ModelState.IsValid) return Page();
          // once the info is correct we hash the password, save the user to database and redirect to dashboard
          return RedirectToPage("Dashboard");
        }
    }
}
