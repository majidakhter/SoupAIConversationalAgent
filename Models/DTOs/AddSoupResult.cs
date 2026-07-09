
using System.Text.Json.Serialization;

namespace SoupAIConversationalAgent.Models.DTOs
{
    public class AddSoupResult
    {
        [JsonPropertyName("newitem")]
        public SoupItem? NewItem { get; set; }

        [JsonPropertyName("updatedcart")]
        public ShoppingCart? UpdatedCart { get; set; }
    }
}
