
using System.Text.Json.Serialization;

namespace SoupAIConversationalAgent.Models
{
    public class ShoppingCart
    {
        [JsonPropertyName("cartid")]
        public Guid CartId { get; set; }

        [JsonPropertyName("items")]
        public List<Soup> Items { get; set; } = new();

        [JsonPropertyName("subtotal")]
        public decimal Subtotal { get; set; }

        [JsonPropertyName("tax")]
        public decimal Tax { get; set; }

        [JsonPropertyName("totalprice")]
        public decimal TotalPrice { get; set; }

        [JsonPropertyName("createdat")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedat")]
        public DateTime UpdatedAt { get; set; }

        public ShoppingCart()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
