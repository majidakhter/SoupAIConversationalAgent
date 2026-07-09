
using System.Text.Json.Serialization;

namespace SoupAIConversationalAgent.Models.DTOs
{
    public class RemoveSoupResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("removedsoupId")]
        public int RemovedSoupId { get; set; }

        [JsonPropertyName("updatedcart")]
        public ShoppingCart? UpdatedCart { get; set; }

        [JsonPropertyName("previoustotal")]
        public decimal PreviousTotal { get; set; }

        [JsonPropertyName("newtotal")]
        public decimal NewTotal { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}
