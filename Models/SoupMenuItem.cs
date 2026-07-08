
namespace SoupAIConversationalAgent.Models
{
    public class SoupMenuItem
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<SoupToppings> DefaultToppings { get; set; } = new();
        public bool IsSpecialty { get; set; }
    }
}
