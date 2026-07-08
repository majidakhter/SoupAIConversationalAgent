
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SoupAIConversationalAgent.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly ILogger<PaymentService> _logger;
        public PaymentService(ILogger<PaymentService> logger)
        {
            _logger = logger;
        }
        public async Task<Guid> RequestPaymentFromUserAsync(Guid cartId)
        {

            _logger.LogInformation("Requesting payment");
            // Simulate the payment process
            return await Task.FromResult(Guid.NewGuid());
        }
    }


}