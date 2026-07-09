
using System.Text.Json.Serialization;

namespace SoupAIConversationalAgent.Models.DTOs
{
    public class OrderResponse
    {
        [JsonPropertyName("orderid")]
        public Guid OrderId { get; set; }

        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("totalamount")]
        public decimal TotalAmount { get; set; }

        [JsonPropertyName("orderdate")]
        public DateTime OrderDate { get; set; }
    }
}
