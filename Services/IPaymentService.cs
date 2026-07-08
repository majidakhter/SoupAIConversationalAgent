namespace SoupAIConversationalAgent.Services
{
    public interface IPaymentService
    {
        Task<Guid> RequestPaymentFromUserAsync(Guid cartId);
    }

}
