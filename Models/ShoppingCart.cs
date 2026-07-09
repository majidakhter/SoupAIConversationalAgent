
namespace SoupAIConversationalAgent.Models
{
    public class ShoppingCart
    {
        public Guid CartId { get; set; }
        public List<Soup> Items { get; set; } = new();
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ShoppingCart()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
