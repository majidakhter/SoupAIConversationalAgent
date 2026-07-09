
using System.Text.Json.Serialization;

namespace SoupAIConversationalAgent.Models
{
    public class MenuItem
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("baseprice")]
        public decimal BasePrice { get; set; }
    }
}
