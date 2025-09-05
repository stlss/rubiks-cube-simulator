using Microsoft.Extensions.DependencyInjection;
using RubiksCubeSimulator.Wpf.Infrastructure.EventPublishers;
using RubiksCubeSimulator.Wpf.Infrastructure.EventPublishers.Extensions;
using RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext;
using RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext.Extensions;

namespace RubiksCubeSimulator.Wpf.App.Infrastructure;

internal interface IRubiksCubeServiceProvider
{
    public IRubiksCubeContextBuilder RubiksCubeContextBuilder { get; }

    public IKeyRubiksCubePublisher KeyRubiksCubePublisher { get; }

    public IMovedRubiksCubePublisher MovedRubiksCubePublisher { get; }

    public IMovingRubiksCubePublisher MovingRubiksCubePublisher { get; }
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
        services.AddEventPublishers();
    }

    public IRubiksCubeContextBuilder RubiksCubeContextBuilder =>
        _serviceProvider.GetRequiredService<IRubiksCubeContextBuilder>();

    public IKeyRubiksCubePublisher KeyRubiksCubePublisher =>
        _serviceProvider.GetRequiredService<IKeyRubiksCubePublisher>();

    public IMovedRubiksCubePublisher MovedRubiksCubePublisher =>
        _serviceProvider.GetRequiredService<IMovedRubiksCubePublisher>();

    public IMovingRubiksCubePublisher MovingRubiksCubePublisher =>
        _serviceProvider.GetRequiredService<IMovingRubiksCubePublisher>();
}