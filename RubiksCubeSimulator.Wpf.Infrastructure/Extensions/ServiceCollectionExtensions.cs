using Microsoft.Extensions.DependencyInjection;
using RubiksCubeSimulator.Application.Extensions;
using RubiksCubeSimulator.Domain.Services;
using RubiksCubeSimulator.Wpf.Infrastructure.EventPublishers;
using RubiksCubeSimulator.Wpf.Infrastructure.EventPublishers.Builders;
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

        services.AddSingleton<IRubiksCubeManager, RubiksCubeManager>();
        services.AddSingleton<IRubiksCubeFaceManager, RubiksCubeFaceManager>();
        services.AddSingleton<IRubiksCubeStickerColorManager, RubiksCubeStickerColorManager>();

        services.AddSingleton<IMoveDirectionMapper, MoveDirectionMapper>();
        services.AddSingleton<ISliceNumberMapper, SliceNumberMapper>();

        services.AddSingleton<IFaceMoveSetter, FaceMoveSetter>();
        services.AddSingleton<ICubeMoveSetter, CubeCubeMoveSetter>();

        services.AddSingleton<IRubiksCubeContextBuilder>(sp => new RubiksCubeContextBuilder(
            cubeBuilder: sp.GetRequiredService<IRubiksCubeBuilder>(),
            cubeManager: sp.GetRequiredService<IRubiksCubeManager>(),
            cubeMover: sp.GetRequiredService<IRubiksCubeMover>(),
            cubeMoveSetter: sp.GetRequiredService<ICubeMoveSetter>()));

        return services;
    }

    public static IServiceCollection AddRubiksCubeContextLinker(this IServiceCollection services)
    {
        services.AddSingleton<IKeyRubiksCubePublisherBuilder, KeyRubiksCubePublisherBuilder>();
        services.AddSingleton<IMovingRubiksCubePublisherBuilder, MovingRubiksCubePublisherBuilder>();
        services.AddSingleton<IMovedRubiksCubePublisherBuilder, MovedRubiksCubePublisherBuilder>();

        services.AddSingleton<IMoveBuilder, MoveBuilder>();

        services.AddSingleton<IMoveArrowSetterBuilder>(sp => new MoveArrowSetterBuilder(
            moveBuilder: sp.GetRequiredService<IMoveBuilder>()));

        services.AddSingleton<IMoveApplierBuilder>(sp => new MoveApplierBuilder(
            moveBuilder: sp.GetRequiredService<IMoveBuilder>()));

        services.AddSingleton<IRubiksCubeContextLinker>(sp => new RubiksCubeContextLinker(
            keyCubePublisherBuilder: sp.GetRequiredService<IKeyRubiksCubePublisherBuilder>(),
            movingCubePublisherBuilder: sp.GetRequiredService<IMovingRubiksCubePublisherBuilder>(),
            movedCubePublisherBuilder: sp.GetRequiredService<IMovedRubiksCubePublisherBuilder>(),
            moveArrowSetterBuilder: sp.GetRequiredService<IMoveArrowSetterBuilder>(),
            moveApplierBuilder: sp.GetRequiredService<IMoveApplierBuilder>()
        ));

        return services;
    }
}