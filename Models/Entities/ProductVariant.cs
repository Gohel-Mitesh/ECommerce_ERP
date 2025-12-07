using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerce_ERP.Models.Entities
{
    public class ProductVariant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VariantId { get; set; }

        public int ProductId { get; set; }  // Foreign key
        public string VariantSize { get; set; }

        [MaxLength(10)]
        public string VariantColorHex { get; set; }

        public int VariantQuantity { get; set; }

        // Photo storage – we'll store filenames/paths here
        public ICollection<ProductVariantPhoto> Photos { get; set; }

        public Product Product { get; set; }
    }
}
