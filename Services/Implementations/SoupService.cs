using SoupAIConversationalAgent.Models;
using SoupAIConversationalAgent.Models.DTOs;

namespace SoupAIConversationalAgent.Services.Implementations
{
public class SoupService : ISoupService
    {
        // We simulate in-memory storage for the shopping carts.
        private static readonly Dictionary<Guid, Cart> _carts = new();
        private static int _nextSoupId = 1;

        public async Task<Menu> GetMenu()
        {
            return await Task.FromResult(
                 new Menu
                 {
                     AvailableSoups = new List<SoupMenuItem> {
                    new () {
                        Name = "FrenchOnion",
                        Description = "Grated Parmesan cheese, toasted bread (baguette) slices broiled with melted cheese, and a swirl of pesto",
                        DefaultToppings = new List<SoupToppings> { SoupToppings.Hearty },
                        IsSpecialty = true
                    },
                    new () {
                        Name = "Ramen",
                        Description = "Crispy bacon bits, homemade Croûtons, toasted seeds/nuts, or a dollop of Greek yogurt",
                        DefaultToppings = new List<SoupToppings> { SoupToppings.Hearty, SoupToppings.Noodle },
                        IsSpecialty = true
                    },
                    new () {
                        Name = "Mixed",
                        Description = "Soy-marinated boiled eggs, fresh scallions, crunchy chili oil, cilantro, bean sprouts, and crispy fried garlic",
                        DefaultToppings = new List<SoupToppings> { SoupToppings.Hearty, SoupToppings.Creamy },
                        IsSpecialty = true
                    }
                },
                     SizePrices = new Dictionary<SoupSize, decimal> {
                    { SoupSize.Small, 250.0m },
                    { SoupSize.Medium, 500.0m },
                    { SoupSize.Large, 1000.0m }
                },
                     AvailableToppings = new List<ToppingInfo> {
                    new () { Topping = SoupToppings.Hearty, Name = "Hearty", AdditionalPrice = 0m },
                    new () { Topping = SoupToppings.Noodle, Name = "Noodle", AdditionalPrice = 1.50m },
                    new () { Topping = SoupToppings.Creamy, Name = "Creamy", AdditionalPrice = 1.00m }
                },
                     Currency = "USD"
                 }
            );
        }

        public async Task<CartChange> AddSoupToCart(Guid cartId, SoupSize size, List<SoupToppings> toppings, int quantity, string specialInstructions)
        {
            // Get or create the cart
            if (!_carts.ContainsKey(cartId))
            {
                _carts[cartId] = new Cart { CartId = cartId };
            }

            var cart = _carts[cartId];
            var previousTotal = cart.TotalPrice;

            // Calculate price
            var menu = await GetMenu();
            var basePrice = menu.SizePrices[size];
            var toppingsPrice = toppings
                .Select(t => menu.AvailableToppings.First(ti => ti.Topping == t).AdditionalPrice)
                .Sum();

            var unitPrice = basePrice + toppingsPrice;
            var totalPrice = unitPrice * quantity;

            // Create the new soup
            var newSoup = new Soup
            {
                Id = _nextSoupId++,
                Size = size,
                Toppings = toppings,
                Quantity = quantity,
                SpecialInstructions = specialInstructions,
                UnitPrice = unitPrice,
                TotalPrice = totalPrice
            };

            // Add to cart
            cart.Items.Add(newSoup);
            cart.UpdatedAt = DateTime.UtcNow;

            // Recalculate cart totals
            UpdateCartTotals(cart);

            var cartDelta = new CartChange
            {
                AddedSoup = newSoup,
                UpdatedCart = cart,
                PreviousTotal = previousTotal,
                NewTotal = cart.TotalPrice,
                Message = $"Added {quantity} {size} soup(s) to cart"
            };

            return await Task.FromResult(cartDelta);
        }

        public async Task<RemoveSoupResponse> RemoveSoupFromCart(Guid cartId, int soupId)
        {
            if (!_carts.ContainsKey(cartId)) 
            {
                return await Task.FromResult(new RemoveSoupResponse
                {
                    Success = false,
                    RemovedSoupId = soupId,
                    Message = "Cart not found"
                });
            }

            var cart = _carts[cartId];
            var previousTotal = cart.TotalPrice;
            var soupToRemove = cart.Items.FirstOrDefault(p => p.Id == soupId);

            if (soupToRemove == null)
            {
                return await Task.FromResult(new RemoveSoupResponse
                {
                    Success = false,
                    RemovedSoupId = soupId,
                    UpdatedCart = cart,
                    PreviousTotal = previousTotal,
                    NewTotal = cart.TotalPrice,
                    Message = "Soup not found in cart"
                });
            }

            cart.Items.Remove(soupToRemove);
            cart.UpdatedAt = DateTime.UtcNow;
            UpdateCartTotals(cart);

            return await Task.FromResult(new RemoveSoupResponse
            {
                Success = true,
                RemovedSoupId = soupId,
                UpdatedCart = cart,
                PreviousTotal = previousTotal,
                NewTotal = cart.TotalPrice,
                Message = $"Removed soup #{soupId} from cart"
            });
        }

        public async Task<Soup?> GetSoupFromCart(Guid cartId, int soupId)
        {
            if (!_carts.ContainsKey(cartId))
            {
                return await Task.FromResult<Soup?>(null);
            }

            var cart = _carts[cartId];
            Soup? soup = cart.Items.FirstOrDefault(p => p.Id == soupId);

            return await Task.FromResult(soup);
        }

        public async Task<Cart> GetCart(Guid cartId)
        {
            if (!_carts.ContainsKey(cartId))
            {
                _carts[cartId] = new Cart { CartId = cartId };
            }

            return await Task.FromResult(_carts[cartId]);
        }

        public async Task<CheckoutResponse> Checkout(Guid cartId, Guid paymentId)
        {
            if (!_carts.ContainsKey(cartId))
            {
                return await Task.FromResult(new CheckoutResponse
                {
                    Success = false,
                    ConfirmationMessage = "Cart not found"
                });
            }

            var cart = _carts[cartId];

            if (cart.Items.Count == 0)
            {
                return await Task.FromResult(new CheckoutResponse
                {
                    Success = false,
                    ConfirmationMessage = "Cart is empty"
                });
            }

            var orderId = Guid.NewGuid();
            var checkoutResponse = new CheckoutResponse
            {
                Success = true,
                OrderId = orderId,
                PaymentId = paymentId,
                Subtotal = cart.Subtotal,
                Tax = cart.Tax,
                TotalAmount = cart.TotalPrice,
                OrderStatus = "Confirmed",
                OrderPlacedAt = DateTime.UtcNow,
                EstimatedDeliveryMinutes = 30 + (cart.Items.Count * 5),
                ConfirmationMessage = $"Order #{orderId} placed successfully! Estimated delivery in 30-45 minutes.",
                OrderedSoups = new List<Soup>(cart.Items)
            };

            // Clear the cart after checkout
            _carts[cartId] = new Cart { CartId = cartId };

            return await Task.FromResult(checkoutResponse);
        }

        private void UpdateCartTotals(Cart cart)
        {
            cart.Subtotal = cart.Items.Sum(p => p.TotalPrice);
            cart.Tax = cart.Subtotal * 0.08m; // 8% tax
            cart.TotalPrice = cart.Subtotal + cart.Tax;
        }
    }

}