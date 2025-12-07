using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerce_ERP.Models.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(200)]
        public string ProductName { get; set; }

        [Required,MaxLength(50)]
        public string ProductSKU { get; set; }

        [Required, MaxLength(100)]
        public string ProductBarcode { get; set; }
        [Required]
        public string ProductDescription { get; set; }

        // Pricing
        [Required]
        [Precision(18, 2),DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        public decimal ProductPrice { get; set; }

        [Precision(18, 2),DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        public decimal? ProductDiscountPrice { get; set; }

        public bool ProductChargeTax { get; set; }

        public bool ProductInStock { get; set; }

        // Organize
        public int CategoryId { get; set; }  // Foreign Key
        public Category Category { get; set; }
        public ProductStatusEnum ProductStatus { get; set; } // Enum-like: Scheduled = 1, Publish = 2, Inactive = 3

        [Required, MaxLength(300)]
        public string ProductTags { get; set; }

        // Navigation property
        public ICollection<ProductVariant> Variants { get; set; }
    }

    public enum ProductStatusEnum
    {
        Scheduled = 1,
        Publish = 2,
        Inactive = 3
    }
}
