using Microsoft.Extensions.DependencyInjection;
using RubiksCubeSimulator.Wpf.Infrastructure.EventPublishers;
using RubiksCubeSimulator.Wpf.Infrastructure.EventSubscribers;
using RubiksCubeSimulator.Wpf.Infrastructure.EventSubscribers.Builders;
using RubiksCubeSimulator.Wpf.Infrastructure.Extensions;
using RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext;

namespace RubiksCubeSimulator.Wpf.App.Infrastructure;

internal interface IRubiksCubeServiceProvider
{
    public IRubiksCubeContextBuilder RubiksCubeContextBuilder { get; }

    public IRubiksCubeContextLinker RubiksCubeContextLinker { get;  }
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
        services.AddRubiksCubeContextBuilder();
        services.AddRubiksCubeContextLinker();
    }


    public IRubiksCubeContextBuilder RubiksCubeContextBuilder =>
        _serviceProvider.GetRequiredService<IRubiksCubeContextBuilder>();

    public IRubiksCubeContextLinker RubiksCubeContextLinker =>
        _serviceProvider.GetRequiredService<IRubiksCubeContextLinker>();
}