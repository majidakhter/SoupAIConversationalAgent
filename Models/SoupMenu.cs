
using System.Text.Json.Serialization;

namespace SoupAIConversationalAgent.Models
{
    public class SoupMenu
    {
        [JsonPropertyName("items")]
        public List<MenuItem> Items { get; set; } = new();

        [JsonPropertyName("prices")]
        public Dictionary<SoupSize, decimal> Prices { get; set; } = new();

        [JsonPropertyName("availabletoppings")]
        public List<string> AvailableToppings { get; set; } = new();
    }
}
