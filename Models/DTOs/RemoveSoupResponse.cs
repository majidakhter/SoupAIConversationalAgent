
namespace SoupAIConversationalAgent.Models.DTOs
{
    public class RemoveSoupResponse
    {
        public bool Success { get; set; }
        public int RemovedSoupId { get; set; }
        public ShoppingCart? UpdatedCart { get; set; }
        public decimal PreviousTotal { get; set; }
        public decimal NewTotal { get; set; }
        public string? Message { get; set; }
    }
}
