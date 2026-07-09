
using System.Text.Json.Serialization;

namespace SoupAIConversationalAgent.Models.DTOs
{
    public class CheckoutResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("orderid")]
        public Guid OrderId { get; set; }

        [JsonPropertyName("paymentid")]
        public Guid PaymentId { get; set; }

        [JsonPropertyName("totalamount")]
        public decimal TotalAmount { get; set; }

        [JsonPropertyName("tax")]
        public decimal Tax { get; set; }

        [JsonPropertyName("subtotal")]
        public decimal Subtotal { get; set; }

        [JsonPropertyName("orderstatus")]
        public string? OrderStatus { get; set; }

        [JsonPropertyName("orderplacedat")]
        public DateTime OrderPlacedAt { get; set; }


        [JsonPropertyName("estimateddeliveryminutes")]
        public int EstimatedDeliveryMinutes { get; set; }

        [JsonPropertyName("confirmationmessage")]
        public string? ConfirmationMessage { get; set; }

        [JsonPropertyName("orderedsoups")]
        public List<Soup> OrderedSoups { get; set; } = new();
    }
}
