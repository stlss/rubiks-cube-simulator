using Microsoft.Extensions.DependencyInjection;
using RubiksCubeSimulator.Application.RubiksCubeBuilder;
using RubiksCubeSimulator.Domain.Services;

namespace RubiksCubeSimulator.Application;

public static class RubiksCubeServiceCollectionExtensions
{
    public static IServiceCollection AddRubiksCubeBuilder(this IServiceCollection services)
    {
        services.AddSingleton<IRubiksCubeBuildExceptionThrower, RubiksCubeBuildExceptionThrower>();
        services.AddSingleton<IRubiksCubeBuilder, RubiksCubeBuilder.RubiksCubeBuilder>();

        return services;
    }
}