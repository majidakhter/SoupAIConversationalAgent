using Microsoft.SemanticKernel;
using SoupAIConversationalAgent;
using SoupAIConversationalAgent.Models;
using SoupAIConversationalAgent.Models.DTOs;
using SoupAIConversationalAgent.Services;
using System.ComponentModel;

namespace SoupAIConversationalAgent.Plugins
{
    //Plugins are the core extension mechanism in Semantic Kernel. They expose C# methods to LLMs as callable functions --
    //enabling AI agents to interact with your application logic, external APIs, and databases.
    public class OrderSoupPlugin
    {
        private readonly ISoupService soupService;
        private readonly IUserContext userContext;
        private readonly IPaymentService paymentService;

        public OrderSoupPlugin(ISoupService soupService, IUserContext userContext, IPaymentService paymentService)
        {
            this.soupService = soupService;
            this.userContext = userContext;
            this.paymentService = paymentService;
        }


        //Native Function Plugins
        [KernelFunction("get_soup_menu")]
        public async Task<Menu> GetPizzaMenuAsync()
        {
            return await soupService.GetMenu();
        }

        [KernelFunction("add_soup_to_cart")]
        [Description("Add a soup to the user's cart; returns the new item and updated cart")]
        public async Task<CartChange> AddSoupToCart(
            SoupSize size,
            List<SoupToppings> toppings,
            int quantity = 1,
            string specialInstructions = ""
        )
        {
            Guid cartId = userContext.GetCartId();
            return await soupService.AddSoupToCart(
                cartId: cartId,
                size: size,
                toppings: toppings,
                quantity: quantity,
                specialInstructions: specialInstructions);
        }

        [KernelFunction("remove_soup_from_cart")]
        public async Task<RemoveSoupResponse> RemoveSoupFromCart(int soupId)
        {
            Guid cartId = userContext.GetCartId();
            return await soupService.RemoveSoupFromCart(cartId, soupId);
        }

        [KernelFunction("get_soup_from_cart")]
        [Description("Returns the specific details of a soup in the user's cart; use this instead of relying on previous messages since the cart may have changed since then.")]
        public async Task<Soup?> GetSoupFromCart(int soupId)
        {
            Guid cartId = await userContext.GetCartIdAsync();
            return await soupService.GetSoupFromCart(cartId, soupId);
        }

        [KernelFunction("get_cart")]
        [Description("Returns the user's current cart, including the total price and items in the cart.")]
        public async Task<Cart> GetCart()
        {
            Guid cartId = await userContext.GetCartIdAsync();
            return await soupService.GetCart(cartId);
        }

        [KernelFunction("checkout")]
        [Description("Checkouts the user's cart; this function will retrieve the payment from the user and complete the order.")]
        public async Task<CheckoutResponse> Checkout()
        {
            Guid cartId = await userContext.GetCartIdAsync();
            Guid paymentId = await paymentService.RequestPaymentFromUserAsync(cartId);

            return await soupService.Checkout(cartId, paymentId);
        }
    }
}
