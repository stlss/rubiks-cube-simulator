using Microsoft.Extensions.DependencyInjection;
using RubiksCubeSimulator.Application.RubiksCubeBuilder;
using RubiksCubeSimulator.Application.RubiksCubeMover;
using RubiksCubeSimulator.Application.RubiksCubeMover.Checkers;
using RubiksCubeSimulator.Application.RubiksCubeMover.Mappers;
using RubiksCubeSimulator.Application.RubiksCubeMover.MutableRubiksCube.Movers;
using RubiksCubeSimulator.Domain.Services;

namespace RubiksCubeSimulator.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddRubiksCubeBuilder(this IServiceCollection services)
    {
        services.AddSingleton<IRubiksCubeBuildExceptionThrower, RubiksCubeBuildExceptionThrower>();

        services.AddSingleton<IRubiksCubeBuilder>(sp => new RubiksCubeBuilder.RubiksCubeBuilder(
            cubeBuildExceptionThrower: sp.GetRequiredService<IRubiksCubeBuildExceptionThrower>()));
    }

    public static void AddRubiksCubeMover(this IServiceCollection services)
    {
        services.AddSingleton<IRubiksCubeChecker, RubiksCubeChecker>();
        services.AddSingleton<IRubiksCubeMoveChecker, RubiksCubeMoveChecker>();

        services.AddSingleton<IRubiksCubeMoveExceptionThrower, RubiksCubeMoveExceptionThrower>();

        services.AddSingleton<IRubiksCubeMapper, RubiksCubeMapper>();
        services.AddSingleton<IRubiksCubeMoveMapper, RubiksCubeMoveMapper>();

        services.AddSingleton<IClockwiseMutableRubiksCubeMover, ClockwiseMutableRubiksCubeMover>();
        services.AddSingleton<ICounterclockwiseMutableRubiksCubeMover, CounterclockwiseMutableRubiksCubeMover>();

        services.AddSingleton<IRubiksCubeMover>(sp => new RubiksCubeMover.RubiksCubeMover(
            cubeMoveExceptionThrower: sp.GetRequiredService<IRubiksCubeMoveExceptionThrower>(),
            cubeMapper: sp.GetRequiredService<IRubiksCubeMapper>(),
            cubeMoveMapper: sp.GetRequiredService<IRubiksCubeMoveMapper>(),
            clockwiseMutableCubeMover: sp.GetRequiredService<IClockwiseMutableRubiksCubeMover>(),
            counterclockwiseMutableCubeMover: sp.GetRequiredService<ICounterclockwiseMutableRubiksCubeMover>()));
    }

    public static void AddMoveGenerator(this IServiceCollection services)
    {
        services.AddSingleton<IMoveGenerator>(sp => new MoveGenerator.MoveGenerator());
    }
}