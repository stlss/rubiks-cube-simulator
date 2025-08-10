using Microsoft.Extensions.DependencyInjection;
using RubiksCubeSimulator.Domain.Services;

namespace RubiksCubeSimulator.Application.Infrastructure.Extensions;

public static class RubiksCubeServiceCollectionExtensions
{
    public static IServiceCollection AddRubiksCubeBuilder(this IServiceCollection services)
    {
        services.AddSingleton<IRubiksCubeBuilder, RubiksCubeBuilder.RubiksCubeBuilder>();

        return services;
    }
}