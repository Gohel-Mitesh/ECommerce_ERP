namespace ECommerce_ERP.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductSKU { get; set; }
        public string ProductBarcode { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal? ProductDiscountPrice { get; set; }
        public bool ProductChargeTax { get; set; }
        public bool ProductInStock { get; set; }
        public string ProductStatus { get; set; }
        //public string CategoryStatusEnum { get; set; }
        public string ProductTags { get; set; }
        public string CategoryTitle { get; set; }
    }
}
