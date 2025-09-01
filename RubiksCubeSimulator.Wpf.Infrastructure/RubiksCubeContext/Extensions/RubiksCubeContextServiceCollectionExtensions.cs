using Microsoft.Extensions.DependencyInjection;
using RubiksCubeSimulator.Application.Infrastructure.Extensions;
using RubiksCubeSimulator.Domain.Services;
using RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext.Mappers;

namespace RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext.Extensions;

public static class RubiksCubeContextServiceCollectionExtensions
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
}