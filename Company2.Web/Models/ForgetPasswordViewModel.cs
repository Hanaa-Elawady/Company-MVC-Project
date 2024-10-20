using System.ComponentModel.DataAnnotations;

namespace Company2.Web.Models
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "InValid Format")]
        public string Email { get; set; }
    }
}
