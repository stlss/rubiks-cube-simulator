using Microsoft.Extensions.DependencyInjection;
using RubiksCubeSimulator.Wpf.App.Infrastructure.RubiksCubeControlViewModelBuilders;

namespace RubiksCubeSimulator.Wpf.App.Infrastructure.RubiksCubeServiceProvider;

public static class RubiksCubeServiceCollectionExtensions
{
    public static IServiceCollection AddRubiksCubeControlViewModelBuilders(this IServiceCollection services)
    {
        services.AddSingleton<IRubiksCubeStickerControlViewModelBuilder, RubiksCubeStickerControlViewModelBuilder>();
        services.AddSingleton<IRubiksCubeFaceControlViewModelBuilder, RubiksCubeFaceControlViewModelBuilder>();
        services.AddSingleton<IRubiksCubeControlViewModelBuilder, RubiksCubeControlViewModelBuilder>();

        return services;
    }
}