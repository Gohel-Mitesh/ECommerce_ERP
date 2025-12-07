using ECommerce_ERP.Models.Entities;

namespace ECommerce_ERP.Models
{
    public class ProductFormViewModel
    {
        // Basic
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductSKU { get; set; }
        public string ProductBarcode { get; set; }
        public string ProductDescription { get; set; }

        // Pricing
        public decimal ProductPrice { get; set; }
        public decimal? ProductDiscountPrice { get; set; }
        public bool ProductChargeTax { get; set; }
        public bool ProductInStock { get; set; }

        // Organize
        public int CategoryId { get; set; }
        public int ProductStatus { get; set; }
        public string ProductTags { get; set; }

        // Variants
        public List<ProductVariantViewModel> Variants { get; set; }
    }

    public class ProductVariantViewModel
    {
        public string VariantSize { get; set; }
        public string VariantColorHex { get; set; }
        public int VariantQuantity { get; set; }
        public List<IFormFile> Photos { get; set; }
    }
}
