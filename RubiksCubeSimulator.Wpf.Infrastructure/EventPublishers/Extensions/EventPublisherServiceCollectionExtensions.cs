using Microsoft.Extensions.DependencyInjection;

namespace RubiksCubeSimulator.Wpf.Infrastructure.EventPublishers.Extensions;

public static class EventPublisherServiceCollectionExtensions
{
    public static IServiceCollection AddEventPublishers(this IServiceCollection services)
    {
        services.AddSingleton<IKeyRubiksCubePublisher>(_ => new KeyRubiksCubePublisher());
        services.AddSingleton<IMovedRubiksCubePublisher>(_ => new MovedRubiksCubePublisher());
        services.AddSingleton<IMovingRubiksCubePublisher>(_ => new MovingRubiksCubePublisher());

        return services;
    }
}