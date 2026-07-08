
namespace SoupAIConversationalAgent.Models.DTOs
{
    public class OrderResponse
    {
        public Guid OrderId { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
