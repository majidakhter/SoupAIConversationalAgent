
using SoupAIConversationalAgent.Models.DTOs;

namespace SoupAIConversationalAgent.Models
{
    public class Menu
    {
        public List<SoupMenuItem> AvailableSoups { get; set; } = new();
        public Dictionary<SoupSize, decimal> SizePrices { get; set; } = new();
        public List<ToppingInfo> AvailableToppings { get; set; } = new();
        public string Currency { get; set; } = "USD";
    }
}
 