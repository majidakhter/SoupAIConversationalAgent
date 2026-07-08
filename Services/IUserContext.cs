namespace SoupAIConversationalAgent.Services
{
    public interface IUserContext
    {
        Guid GetCartId();
        Task<Guid> GetCartIdAsync();
    }

}
