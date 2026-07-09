
using System.Text.Json.Serialization;

namespace SoupAIConversationalAgent.Models.DTOs
{
    public class CartChange
    {
        [JsonPropertyName("addedsoup")]
        public Soup? AddedSoup { get; set; }

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
