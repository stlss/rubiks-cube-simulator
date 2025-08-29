using RubiksCubeSimulator.Wpf.App.Infrastructure.EventHandlers;
using RubiksCubeSimulator.Wpf.App.Infrastructure.EventHandlers.MoveRubiksCubeEventHandlers;

namespace RubiksCubeSimulator.Wpf.App.Infrastructure.EventSubscriptionManagers;

internal interface IMoveRubiksCubeEventSubscriptionManager
{
    public void Subscribe(IMainWindowEventHandler mainWindowViewModelEventHandler,
        IEnumerable<IMoveRubiksCubeEventHandler> moveCubeEventHandlers);

    public void Unsubscribe(IMainWindowEventHandler mainWindowViewModelEventHandler,
        IEnumerable<IMoveRubiksCubeEventHandler> cubeEventHandlers);
}

internal sealed class MoveMoveRubiksCubeEventSubscriptionManager : IMoveRubiksCubeEventSubscriptionManager
{
    public void Subscribe(IMainWindowEventHandler mainWindowViewModelEventHandler,
        IEnumerable<IMoveRubiksCubeEventHandler> moveCubeEventHandlers)
    {
        foreach (var moveCubeEventHandler in moveCubeEventHandlers)
        {
            mainWindowViewModelEventHandler.MovingRubiksCube += moveCubeEventHandler.OnMovingRubiksCube;
            mainWindowViewModelEventHandler.MovedRubiksCube += moveCubeEventHandler.OnMovedRubiksCube;
        }
    }

    public void Unsubscribe(IMainWindowEventHandler mainWindowViewModelEventHandler,
        IEnumerable<IMoveRubiksCubeEventHandler> moveCubeEventHandlers)
    {
        foreach (var moveCubeEventHandler in moveCubeEventHandlers)
        {
            mainWindowViewModelEventHandler.MovingRubiksCube -= moveCubeEventHandler.OnMovingRubiksCube;
            mainWindowViewModelEventHandler.MovedRubiksCube -= moveCubeEventHandler.OnMovedRubiksCube;
        }
    }
}