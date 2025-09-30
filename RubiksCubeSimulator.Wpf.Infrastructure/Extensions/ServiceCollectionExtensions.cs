using Microsoft.Extensions.DependencyInjection;
using RubiksCubeSimulator.Application.Extensions;
using RubiksCubeSimulator.Domain.Services;
using RubiksCubeSimulator.Wpf.Infrastructure.EventPublishers;
using RubiksCubeSimulator.Wpf.Infrastructure.EventSubscribers.Builders;
using RubiksCubeSimulator.Wpf.Infrastructure.MoveServices;
using RubiksCubeSimulator.Wpf.Infrastructure.MoveServices.Mappers;
using RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext;
using RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext.Managers;

namespace RubiksCubeSimulator.Wpf.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRubiksCubeContextBuilder(this IServiceCollection services)
    {
        services.AddRubiksCubeBuilder();
        services.AddRubiksCubeMover();

        AddRubiksCubeManagers(services);
        AddMoveSetters(services);

        services.AddSingleton<IRubiksCubeContextBuilder>(sp => new RubiksCubeContextBuilder(
            cubeBuilder: sp.GetRequiredService<IRubiksCubeBuilder>(),
            cubeManager: sp.GetRequiredService<IRubiksCubeManager>(),
            cubeMover: sp.GetRequiredService<IRubiksCubeMover>(),
            cubeMoveSetter: sp.GetRequiredService<ICubeMoveSetter>()));

        return services;
    }

    private static IServiceCollection AddRubiksCubeManagers(this IServiceCollection services)
    {
        services.AddSingleton<IRubiksCubeManager, RubiksCubeManager>();
        services.AddSingleton<IRubiksCubeFaceManager, RubiksCubeFaceManager>();
        services.AddSingleton<IRubiksCubeStickerColorManager, RubiksCubeStickerColorManager>();

        return services;
    }

    private static IServiceCollection AddMoveSetters(this IServiceCollection services)
    {
        services.AddSingleton<IMoveDirectionMapper, MoveDirectionMapper>();
        services.AddSingleton<ISliceNumberMapper, SliceNumberMapper>();

        services.AddSingleton<IFaceMoveSetter, FaceMoveSetter>();
        services.AddSingleton<ICubeMoveSetter, CubeCubeMoveSetter>();

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
        services.AddSingleton<IMoveBuilder, MoveBuilder>();

        services.AddSingleton<IMoveArrowSetterBuilder>(sp => new MoveArrowSetterBuilder(
            moveBuilder: sp.GetRequiredService<IMoveBuilder>()));

        services.AddSingleton<IMoveApplierBuilder>(sp => new MoveApplierBuilder(
            moveBuilder: sp.GetRequiredService<IMoveBuilder>()));

        return services;
    }
}