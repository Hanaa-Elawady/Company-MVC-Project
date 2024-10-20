using System.ComponentModel.DataAnnotations;

namespace Company2.Web.Models
{
    public class LogInViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "InValid Format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{6,}$", ErrorMessage = "Password must have at least one uppercase letter, one lowercase letter, one digit, one non-alphanumeric character, and must be at least 6 characters long.")]
        public string Password { get; set; }
    }
}
