using Microsoft.Extensions.DependencyInjection;
using RubiksCubeSimulator.Application.Infrastructure.Extensions;
using RubiksCubeSimulator.Domain.Services;
using RubiksCubeSimulator.Wpf.Infrastructure.EventPublishers;
using RubiksCubeSimulator.Wpf.Infrastructure.EventSubscribers;
using RubiksCubeSimulator.Wpf.Infrastructure.EventSubscribers.Builders;
using RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext;
using RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext.Mappers;

namespace RubiksCubeSimulator.Wpf.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRubiksCubeContextBuilder(this IServiceCollection services)
    {
        services.AddRubiksCubeBuilder();
        services.AddRubiksCubeMover();

        AddRubiksCubeMappers(services);

        services.AddSingleton<IRubiksCubeContextBuilder>(sp =>
        {
            var cubeBuilder = sp.GetRequiredService<IRubiksCubeBuilder>();
            var cubeMapper = sp.GetRequiredService<IRubiksCubeMapper>();
            var cubeMover = sp.GetRequiredService<IRubiksCubeMover>();

            return new RubiksCubeContextBuilder(cubeBuilder, cubeMapper, cubeMover);
        });

        return services;
    }

    private static IServiceCollection AddRubiksCubeMappers(this IServiceCollection services)
    {
        services.AddSingleton<IRubiksCubeMapper, RubiksCubeMapper>();
        services.AddSingleton<IRubiksCubeFaceMapper, RubiksCubeFaceMapper>();
        services.AddSingleton<IRubiksCubeStickerColorMapper, RubiksCubeStickerColorMapper>();

        return services;
    }

    public static IServiceCollection AddEventPublishers(this IServiceCollection services)
    {
        services.AddSingleton<IKeyRubiksCubePublisher>(_ => new KeyRubiksCubePublisher());
        services.AddSingleton<IMovedRubiksCubePublisher>(_ => new MovedRubiksCubePublisher());
        services.AddSingleton<IMovingRubiksCubePublisher>(_ => new MovingRubiksCubePublisher());

        return services;
    }

    public static IServiceCollection AddEventSubscribersBuilders(this IServiceCollection services)
    {
        services.AddSingleton<IMoveArrowSetterBuilder>(_ => new MoveArrowSetterBuilder());
        services.AddSingleton<IMoveApplierBuilder>(_ => new MoveApplierBuilder());

        return services;
    }
}