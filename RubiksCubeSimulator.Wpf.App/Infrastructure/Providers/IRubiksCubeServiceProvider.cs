using Microsoft.Extensions.DependencyInjection;
using RubiksCubeSimulator.Application.Infrastructure.Extensions;
using RubiksCubeSimulator.Domain.Services;
using RubiksCubeSimulator.Wpf.App.Infrastructure.ViewModelBuilders.RubiksCubeControls;

namespace RubiksCubeSimulator.Wpf.App.Infrastructure.Providers;

internal interface IRubiksCubeServiceProvider
{
    public IRubiksCubeBuilder GetCubeBuilder();

    public IRubiksCubeControlViewModelBuilder GetCubeViewModelBuilder();
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
        services.AddRubiksCubeBuilder();
        services.AddRubiksCubeControlViewModelBuilders();
    }

    public IRubiksCubeBuilder GetCubeBuilder()
        => _serviceProvider.GetRequiredService<IRubiksCubeBuilder>();

    public IRubiksCubeControlViewModelBuilder GetCubeViewModelBuilder()
        => _serviceProvider.GetRequiredService<IRubiksCubeControlViewModelBuilder>();
}