using Microsoft.Extensions.DependencyInjection;
using RubiksCubeSimulator.Application.RubiksCubeBuilder;
using RubiksCubeSimulator.Application.RubiksCubeMover;
using RubiksCubeSimulator.Application.RubiksCubeMover.Checkers;
using RubiksCubeSimulator.Application.RubiksCubeMover.Mappers;
using RubiksCubeSimulator.Application.RubiksCubeMover.MutableRubiksCube.Movers;
using RubiksCubeSimulator.Domain.Services;

namespace RubiksCubeSimulator.Application.Infrastructure.Extensions;

public static class RubiksCubeServiceCollectionExtensions
{
    public static IServiceCollection AddRubiksCubeBuilder(this IServiceCollection services)
    {
        services.AddSingleton<IRubiksCubeBuildExceptionThrower, RubiksCubeBuildExceptionThrower>();
        services.AddSingleton<IRubiksCubeBuilder, RubiksCubeBuilder.RubiksCubeBuilder>();

        return services;
    }

    public static IServiceCollection AddRubiksCubeMover(this IServiceCollection services)
    {
        services.AddSingleton<IRubiksCubeChecker, RubiksCubeChecker>();
        services.AddSingleton<IRubiksCubeMoveChecker, RubiksCubeMoveChecker>();

        services.AddSingleton<IRubiksCubeMoveExceptionThrower, RubiksCubeMoveExceptionThrower>();

        services.AddSingleton<IRubiksCubeMapper, RubiksCubeMapper>();
        services.AddSingleton<IRubiksCubeMoveMapper, RubiksCubeMoveMapper>();

        services.AddSingleton<IClockwiseMutableRubiksCubeMover, ClockwiseMutableRubiksCubeMover>();
        services.AddSingleton<ICounterclockwiseMutableRubiksCubeMover, CounterclockwiseMutableRubiksCubeMover>();

        services.AddSingleton<IRubiksCubeMover, RubiksCubeMover.RubiksCubeMover>();

        return services;
    }
}