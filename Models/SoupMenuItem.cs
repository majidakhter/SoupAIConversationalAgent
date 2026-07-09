
using System.Text.Json.Serialization;

namespace SoupAIConversationalAgent.Models
{
    public class SoupMenuItem
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("defaulttoppings")]
        public List<SoupToppings> DefaultToppings { get; set; } = new();

        [JsonPropertyName("isspecialty")]
        public bool IsSpecialty { get; set; }
    }
}
