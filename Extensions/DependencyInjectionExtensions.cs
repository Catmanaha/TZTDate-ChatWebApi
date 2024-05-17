using Microsoft.Extensions.DependencyInjection;

namespace TZTDate.Infrastructure.Extensions;

public static class DependencyInjectionExtensions
{
    public static void Inject(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAzureBlobService, AzureBlobService>();
        serviceCollection.AddSingleton<HttpClient>();
    }
}
