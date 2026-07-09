using SoupAIConversationalAgent.Models;
using SoupAIConversationalAgent.Models.DTOs;

namespace SoupAIConversationalAgent.Services
{
    public interface ISoupService
    {


        Task<Menu> GetMenu();
        Task<CartChange> AddSoupToCart(
            Guid cartId,
            SoupSize size,
            List<SoupToppings> toppings,
            int quantity,
            string specialInstructions);
        Task<RemoveSoupResponse> RemoveSoupFromCart(Guid cartId, int soupId);
        Task<Soup?> GetSoupFromCart(Guid cartId, int soupId);
        Task<ShoppingCart> GetCart(Guid cartId);
        Task<CheckoutResponse> Checkout(Guid cartId, Guid paymentId);
    }
}
