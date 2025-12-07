using System.ComponentModel.DataAnnotations;

namespace ECommerce_ERP.Models
{
    public class LoginViewModel
    {
        [Required, MaxLength(255)]
        [Display(Name = "Email or Username")]
        public string EmailOrUsername { get; set; } = "";

        [Required, DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = "";

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
