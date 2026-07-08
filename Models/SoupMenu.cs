
namespace SoupAIConversationalAgent.Models
{
    public class SoupMenu
    {
        public List<MenuItem> Items { get; set; } = new();
        public Dictionary<SoupSize, decimal> Prices { get; set; } = new();
        public List<string> AvailableToppings { get; set; } = new();
    }
}
