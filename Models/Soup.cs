using System.Text.Json.Serialization;

namespace SoupAIConversationalAgent.Models
{
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
        public int Id { get; set; }
        public SoupSize Size { get; set; }
        public List<SoupToppings> Toppings { get; set; } = new();
        public int Quantity { get; set; }
        public string? SpecialInstructions { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime AddedAt { get; set; }

        public Soup()
        {
            AddedAt = DateTime.UtcNow;
        }
    }
}
