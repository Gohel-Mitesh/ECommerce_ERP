using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerce_ERP.Models.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, MaxLength(255)]
        public string UserName { get; set; } = "";

        [Required, MaxLength(255)]
        public string UserEmail { get; set; } = "";

        public int UserRole { get; set; } = 2;

        [Required, MaxLength(500)]
        public string PasswordHash { get; set; } = "";   // Hashed password

    }
}
