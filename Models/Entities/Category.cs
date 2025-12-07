using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ECommerce_ERP.Migrations;

namespace ECommerce_ERP.Models.Entities
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Required, MaxLength(255)]
        public string CategoryTitle { get; set; } = "";

        [Required, MaxLength(255)]
        public string CategorySlug { get; set; } = "";

        public string CategorImage { get; set; } = "";

        public int CategoryParentCategory { get; set; }

        [Required]
        public CategoryStatusEnum CategoryStatus { get; set; }

        [MaxLength(500)]
        public string CategoryDescription { get; set; } = "";
        // Optional: reverse navigation
        public ICollection<Product> Products { get; set; }
    }

    public enum CategoryStatusEnum
    {
        Scheduled = 1,
        Publish = 2,
        Inactive = 3
    }
}
