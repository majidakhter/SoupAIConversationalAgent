using System.Text.Json.Serialization;

namespace SoupAIConversationalAgent.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SoupSize
    {
        Small, //upto 250ml
        Medium, //upto 500 ml
        Large  //more than 500ml
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SoupToppings
    {
        Creamy,
        Pureed,
        Broth,
        Noodle,
        Hearty
    }
    public class Soup
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("size")]
        public SoupSize Size { get; set; }

        [JsonPropertyName("toppings")]
        public List<SoupToppings> Toppings { get; set; } = new();

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("specialinstructions")]
        public string? SpecialInstructions { get; set; }

        [JsonPropertyName("unitprice")]
        public decimal UnitPrice { get; set; }

        [JsonPropertyName("totalprice")]
        public decimal TotalPrice { get; set; }

        [JsonPropertyName("addedat")]
        public DateTime AddedAt { get; set; }

        public Soup()
        {
            AddedAt = DateTime.UtcNow;
        }
    }
}
