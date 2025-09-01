using Microsoft.Extensions.DependencyInjection;
using RubiksCubeSimulator.Application.Infrastructure.Extensions;
using RubiksCubeSimulator.Domain.Services;
using RubiksCubeSimulator.Wpf.App.Infrastructure.RubiksCubeControlViewModelMappers;
using RubiksCubeSimulator.Wpf.App.Infrastructure.ViewModelEventLinker;
using RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext;
using RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext.Extensions;

namespace RubiksCubeSimulator.Wpf.App.Infrastructure.RubiksCubeServiceProvider;

internal interface IRubiksCubeServiceProvider
{
    public IRubiksCubeBuilder GetCubeBuilder();

    public IRubiksCubeControlViewModelMapper GetCubeViewModelMapper();

    public IViewModelEventLinker GetViewModelLinker();

    public IRubiksCubeContextBuilder GetRubiksCubeContextBuilder();
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
        services.AddRubiksCubeControlViewModelMappers();
        services.AddViewModelLinker();

        services.AddRubiksCubeContextBuilder();
    }

    public IRubiksCubeBuilder GetCubeBuilder() =>
        _serviceProvider.GetRequiredService<IRubiksCubeBuilder>();

    public IRubiksCubeControlViewModelMapper GetCubeViewModelMapper() =>
        _serviceProvider.GetRequiredService<IRubiksCubeControlViewModelMapper>();

    public IViewModelEventLinker GetViewModelLinker() =>
        _serviceProvider.GetRequiredService<IViewModelEventLinker>();

    public IRubiksCubeContextBuilder GetRubiksCubeContextBuilder() =>
        _serviceProvider.GetRequiredService<IRubiksCubeContextBuilder>();
}