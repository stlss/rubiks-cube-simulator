using Microsoft.Extensions.DependencyInjection;
using RubiksCubeSimulator.Application;
using RubiksCubeSimulator.Domain.Services;

namespace RubiksCubeSimulator.WpfApp.Infrastructure.Providers;

internal interface IRubiksCubeServiceProvider
{
    public IRubiksCubeBuilder GetRubiksCubeBuilder();
}

internal sealed class RubiksCubeServiceProvider : IRubiksCubeServiceProvider
{
    private readonly ServiceProvider _serviceProvider = CreateServiceProvider();

    private static ServiceProvider CreateServiceProvider()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);

        var provider = services.BuildServiceProvider();
        return provider;
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IRubiksCubeBuilder, RubiksCubeBuilder>();
    }

    public IRubiksCubeBuilder GetRubiksCubeBuilder() => _serviceProvider.GetRequiredService<IRubiksCubeBuilder>();
}
