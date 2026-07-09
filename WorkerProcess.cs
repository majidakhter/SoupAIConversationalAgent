using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Google;
using SoupAIConversationalAgent.Plugins;


namespace SoupAIConversationalAgent
{
    public class WorkerProcess
    {
        private readonly ILogger<WorkerProcess> _logger;
        private readonly Kernel _kernel;

        private readonly OrderSoupPlugin orderSoupPlugin;

        private readonly ChatHistory history;
        public WorkerProcess(ILogger<WorkerProcess> logger, Kernel kernel, OrderSoupPlugin orderSoupPlugin, ChatHistory chatHistory)
        {
            _logger = logger;
            _kernel = kernel;
            this.orderSoupPlugin = orderSoupPlugin;
            this.history = chatHistory;
        } 

        public async Task RunAsync()
        {
            _logger.LogInformation("🚀 Starting Worker with Semantic Kernel + Gemini...");


            _kernel.Plugins.AddFromObject(orderSoupPlugin, "OrderSoup");

            GeminiPromptExecutionSettings openAIPromptExecutionSettings = new()
            {
                Temperature = 0.7,
                //
                ToolCallBehavior = GeminiToolCallBehavior.AutoInvokeKernelFunctions
            };



            var history = new ChatHistory();
            history.AddSystemMessage("You are a helpful and friendly assistant.");
            history.Add(new()
            {
                Role = AuthorRole.User,
                AuthorName = "Majid Akhter",
                Items = [
                    new TextContent {Text="I am someone who wants to take a test."}
                ]
            });


            Console.WriteLine("Type your messages (or 'exit' to finish):\n");
            // Initiate a back-and-forth chat
            string? userInput = "";

            // Get chat service from the kernel
            var chat = _kernel.GetRequiredService<IChatCompletionService>();

            do
            {
                // Collect user input
                Console.Write("User > ");
                userInput = Console.ReadLine();

                // Check if the user wants to exit or if the input is null.
                if (string.IsNullOrWhiteSpace(userInput) || userInput.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                // Add user input
                history.AddUserMessage(userInput);

                // Get the response from the AI
                var result = await chat.GetChatMessageContentAsync(
                    history,
                    executionSettings: openAIPromptExecutionSettings,
                    kernel: _kernel);

                // Print the results
                Console.WriteLine("Assistant > " + result);

                // Add the message from the agent to the chat history
                history.AddMessage(result.Role, result.Content ?? string.Empty);
            } while (userInput is not null);
        }
    }
}
