
using SoupAIConversationalAgent.Models.DTOs;
using System.Text.Json.Serialization;

namespace SoupAIConversationalAgent.Models
{
    public class Menu
    {
        [JsonPropertyName("availablesoups")]
        public List<SoupMenuItem> AvailableSoups { get; set; } = new();

        [JsonPropertyName("sizeprices")]
        public Dictionary<SoupSize, decimal> SizePrices { get; set; } = new();

        [JsonPropertyName("availabletoppings")]
        public List<ToppingInfo> AvailableToppings { get; set; } = new();

        [JsonPropertyName("currency")]
        public string Currency { get; set; } = "USD";
    }
}
 