using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using SoupAIConversationalAgent;
using SoupAIConversationalAgent.Plugins;
using SoupAIConversationalAgent.Services;
using SoupAIConversationalAgent.Services.Implementations;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var builderHosting = Host.CreateApplicationBuilder();


// 1. Obtain the configuration interface (already injected by the builder))
var configuration = builderHosting.Configuration;

// 2. Read the values ​​from the "GeminiConfiguration" section."
var modelId = configuration["GeminiConfiguration:ModelId"];
var apiKey = configuration["GeminiConfiguration:ApiKey"];

// 3. Basic verification (optional but recommended)
if (string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(modelId))
{
    Console.WriteLine("Error: The API Key or Model ID is not configured in appsettings.json.");
    return; // Terminates the application if credentials are missing.
}

// Logging configuration
builderHosting.Logging.ClearProviders();
builderHosting.Logging.AddConsole();
builderHosting.Logging.AddDebug();
builderHosting.Logging.SetMinimumLevel(LogLevel.Trace);

builderHosting.Services.AddSingleton<ISoupService, SoupService>();
builderHosting.Services.AddSingleton<IUserContext, UserContext>();
builderHosting.Services.AddSingleton<IPaymentService, PaymentService>();

builderHosting.Services.AddScoped(sp => new ChatHistory());

// 3. Register the Plugin (KernelFunctions container)
// We register it as Transient or Singleton so that the container resolves it.. 
// Plugin registration
builderHosting.Services.AddTransient<OrderSoupPlugin>();

// 4. Register the kernel instance
// Kernel Log
//The Kernel object is your central orchestration hub. It manages AI service connections, registered plugins, and execution context for all AI operations.
builderHosting.Services.AddTransient(sp => new Kernel(sp));

// 5. Register the LLM service (IChatCompletionService)
// LLM provider configuration (Google Gemini in this case)
builderHosting.Services.AddGoogleAIGeminiChatCompletion(modelId, apiKey);


// Register the worker that will use the Kernel and Logger.
builderHosting.Services.AddTransient<WorkerProcess>();

var host = builderHosting.Build();
var worker = host.Services.GetRequiredService<WorkerProcess>();
await worker.RunAsync();