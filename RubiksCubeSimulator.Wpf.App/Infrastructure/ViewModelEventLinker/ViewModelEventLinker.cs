using RubiksCubeSimulator.Wpf.App.Infrastructure.EventHandlers;
using RubiksCubeSimulator.Wpf.App.Infrastructure.EventHandlers.RubiksCube;
using RubiksCubeSimulator.Wpf.App.Infrastructure.EventSubscriptionManagers;
using RubiksCubeSimulator.Wpf.App.ViewModels.Windows;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.App.Infrastructure.ViewModelEventLinker;

internal interface IViewModelEventLinker
{
    public void Link(MainWindowViewModel mainWindowViewModel, RubiksCubeControlViewModel cubeViewModel);

    public void Unlink(MainWindowViewModel mainWindowViewModel, RubiksCubeControlViewModel cubeViewModel);
}

internal sealed class ViewModelEventLinker(
    IRubiksCubeControlEventHandler cubeEventHandler,
    IMainWindowEventHandler mainWindowHandler,
    IMoveRubiksCubeEventHandlerFactory moveRubiksCubeHandlerFactory,
    IRubiksCubeControlEventSubscriptionManager cubeEventSubscriptionManager,
    IMainWindowEventSubscriptionManager mainWindowEventSubscriptionManager,
    IMoveRubiksCubeEventSubscriptionManager moveRubiksCubeEventSubscriptionManager) : IViewModelEventLinker
{
    private readonly Dictionary<ViewModelSet, EventHandlerSet> _eventHandlerSetByViewModelSet = [];

    public void Link(MainWindowViewModel mainWindowViewModel, RubiksCubeControlViewModel cubeViewModel)
    {
        var moveRubiksCubeEventHandlers = moveRubiksCubeHandlerFactory.CreateMoveRubiksCubeEventHandlers(cubeViewModel);

        cubeEventSubscriptionManager.Subscribe(cubeViewModel, cubeEventHandler);
        mainWindowEventSubscriptionManager.Subscribe(mainWindowViewModel, mainWindowHandler);
        moveRubiksCubeEventSubscriptionManager.Subscribe(mainWindowHandler, moveRubiksCubeEventHandlers);

        var viewModelSet = new ViewModelSet(mainWindowViewModel, cubeViewModel);
        var eventHandlerSet = new EventHandlerSet(cubeEventHandler, mainWindowHandler, moveRubiksCubeEventHandlers);

        _eventHandlerSetByViewModelSet[viewModelSet] = eventHandlerSet;
    }

    public void Unlink(MainWindowViewModel mainWindowViewModel, RubiksCubeControlViewModel cubeViewModel)
    {
        var viewModelSet = new ViewModelSet(mainWindowViewModel, cubeViewModel);
        var eventHandlerSet = _eventHandlerSetByViewModelSet[viewModelSet];

        cubeEventSubscriptionManager.Unsubscribe(viewModelSet.CubeControl, eventHandlerSet.CubeEventHandler);
        mainWindowEventSubscriptionManager.Unsubscribe(viewModelSet.MainWindow, eventHandlerSet.MainWindowHandler);
        moveRubiksCubeEventSubscriptionManager.Unsubscribe(mainWindowHandler,
            eventHandlerSet.MoveRubiksCubeEventHandlers);

        _eventHandlerSetByViewModelSet.Remove(viewModelSet);
    }

    private sealed record ViewModelSet(
        MainWindowViewModel MainWindow,
        RubiksCubeControlViewModel CubeControl);

    private sealed record EventHandlerSet(
        IRubiksCubeControlEventHandler CubeEventHandler,
        IMainWindowEventHandler MainWindowHandler,
        IReadOnlyList<IMoveRubiksCubeEventHandler> MoveRubiksCubeEventHandlers);
}