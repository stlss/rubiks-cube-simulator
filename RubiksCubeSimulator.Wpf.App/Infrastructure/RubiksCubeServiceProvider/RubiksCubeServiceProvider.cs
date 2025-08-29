using Microsoft.Extensions.DependencyInjection;
using RubiksCubeSimulator.Application.Infrastructure.Extensions;
using RubiksCubeSimulator.Domain.Services;
using RubiksCubeSimulator.Wpf.App.Infrastructure.RubiksCubeControlViewModelBuilders;
using RubiksCubeSimulator.Wpf.App.Infrastructure.ViewModelEventLinker;

namespace RubiksCubeSimulator.Wpf.App.Infrastructure.RubiksCubeServiceProvider;

internal interface IRubiksCubeServiceProvider
{
    public IRubiksCubeBuilder GetCubeBuilder();

    public IRubiksCubeControlViewModelBuilder GetCubeViewModelBuilder();

    public IViewModelEventLinker GetViewModelLinker();
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
        services.AddViewModelLinker();
    }

    public IRubiksCubeBuilder GetCubeBuilder() =>
        _serviceProvider.GetRequiredService<IRubiksCubeBuilder>();

    public IRubiksCubeControlViewModelBuilder GetCubeViewModelBuilder() =>
        _serviceProvider.GetRequiredService<IRubiksCubeControlViewModelBuilder>();

    public IViewModelEventLinker GetViewModelLinker() =>
        _serviceProvider.GetRequiredService<IViewModelEventLinker>();
}