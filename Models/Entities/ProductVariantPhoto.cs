using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerce_ERP.Models.Entities
{
    public class ProductVariantPhoto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhotoId { get; set; }

        public int VariantId { get; set; }

        [Required]
        [MaxLength(255)]
        public string FilePath { get; set; }  // e.g. "uploads/products/photo1.jpg"

        public ProductVariant Variant { get; set; }
    }
}
