namespace ECommerce_ERP.Models.Entities
{
    public class UserSession
    {
        public int UserId { get; set; }          // Unique ID
        public int Role { get; set; }          // Customer / Admin / Seller
        public string Name { get; set; }          // FirstName or FullName
        public string Email { get; set; }         // User Email
        public string? ProfilePic { get; set; }   // Optional

        // ECommerce specific
        public List<CartItem>? Cart { get; set; }
        public string? Currency { get; set; }
        public string? Language { get; set; }
        public DateTime LoginTime { get; set; }
    }
    public class CartItem
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
