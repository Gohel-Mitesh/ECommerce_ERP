using System.ComponentModel.DataAnnotations;

namespace ECommerce_ERP.Models
{
    public class RegisterViewModel
    {
        [Required, MaxLength(255)]
        [Display(Name = "Username")]
        public string UserName { get; set; } = "";

        [Required, MaxLength(255)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string UserEmail { get; set; } = "";

        [Required, MaxLength(255)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string UserPassword { get; set; } = "";

        [Required, MaxLength(255)]
        [DataType(DataType.Password)]
        [Compare("UserPassword", ErrorMessage = "Passwords do not match")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = "";
    }
}
