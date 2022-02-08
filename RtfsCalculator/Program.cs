using Microsoft.Extensions.DependencyInjection;
using RtfsCalculator.Abstractions.Interfaces.Services;
using RtfsCalculator.Implementations.Services;

namespace RtfsCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetService<RtfsCalculator>().Run().Wait();
        }

        static IServiceCollection ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<RtfsCalculator>();
            serviceCollection.AddSingleton<IRtfsCalculatorService, RtfsCalculatorService>();
            return serviceCollection;
        }
    }
}
