using Microsoft.Extensions.DependencyInjection;
using RubiksCubeSimulator.Wpf.Infrastructure.Extensions;
using RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext;

namespace RubiksCubeSimulator.Wpf.App.Infrastructure;

internal interface IRubiksCubeServiceProvider
{
    public IRubiksCubeContextBuilder RubiksCubeContextBuilder { get; }

    public IKeySubscriberBuilder KeySubscriberBuilder { get;  }
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
        services.AddKeySubscriberBuilder();
    }

    public IRubiksCubeContextBuilder RubiksCubeContextBuilder =>
        _serviceProvider.GetRequiredService<IRubiksCubeContextBuilder>();

    public IKeySubscriberBuilder KeySubscriberBuilder =>
        _serviceProvider.GetRequiredService<IKeySubscriberBuilder>();
}