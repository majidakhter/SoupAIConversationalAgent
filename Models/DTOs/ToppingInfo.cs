
using System.Text.Json.Serialization;

namespace SoupAIConversationalAgent.Models.DTOs
{
    public class ToppingInfo
    {
        [JsonPropertyName("topping")]
        public SoupToppings Topping { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("additionalprice")]
        public decimal AdditionalPrice { get; set; }
    }
}
