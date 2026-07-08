
namespace SoupAIConversationalAgent.Models.DTOs
{
    public class CartChange
    {
        public Soup? AddedSoup { get; set; }
        public Cart? UpdatedCart { get; set; }
        public decimal PreviousTotal { get; set; }
        public decimal NewTotal { get; set; }
        public string? Message { get; set; }
    }
}
