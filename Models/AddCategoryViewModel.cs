namespace ECommerce_ERP.Models
{
    public class AddCategoryViewModel
    {
        public int? CategoryId { get; set; }
        public string CategoryTitle { get; set; }

        public string CategorySlug { get; set; }

        public string CategorImage { get; set; }

        public int CategoryParentCategory { get; set; }

        public int CategoryStatus { get; set; }

        public string CategoryDescription { get; set; }
    }
}
