using Microsoft.Extensions.DependencyInjection;
using RubiksCubeSimulator.Wpf.App.Infrastructure.EventHandlers;
using RubiksCubeSimulator.Wpf.App.Infrastructure.EventHandlers.MoveRubiksCubeEventHandlers;
using RubiksCubeSimulator.Wpf.App.Infrastructure.EventSubscriptionManagers;
using RubiksCubeSimulator.Wpf.App.Infrastructure.RubiksCubeControlViewModelBuilders;
using RubiksCubeSimulator.Wpf.App.Infrastructure.ViewModelEventLinker;

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

    public static IServiceCollection AddViewModelLinker(this IServiceCollection services)
    {
        services.AddSingleton<IRubiksCubeControlEventHandler, RubiksCubeControlEventHandler>();
        services.AddSingleton<IMainWindowEventHandler, MainWindowEventHandler>();
        services.AddSingleton<IMoveRubiksCubeEventHandlerFactory, MoveRubiksCubeEventHandlerFactory>();

        services.AddSingleton<IRubiksCubeControlEventSubscriptionManager, RubiksCubeControlEventSubscriptionManager>();
        services.AddSingleton<IMainWindowEventSubscriptionManager, MainWindowEventHandlerSubscriptionManager>();
        services.AddSingleton<IMoveRubiksCubeEventSubscriptionManager, MoveMoveRubiksCubeEventSubscriptionManager>();

        services.AddSingleton<IViewModelEventLinker, ViewModelEventLinker.ViewModelEventLinker>();

        return services;
    }
}