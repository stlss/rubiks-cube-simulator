using RubiksCubeSimulator.Wpf.App.Infrastructure.EventHandlers.MoveRubiksCubeEventHandlers.EventArgs;

namespace RubiksCubeSimulator.Wpf.App.Infrastructure.EventHandlers.MoveRubiksCubeEventHandlers;

internal interface IMoveRubiksCubeEventHandler
{
    public void OnMovingRubiksCube(object? sender, MovingRubiksCubeEventArgs e);

    public void OnMovedRubiksCube(object? sender, MovedRubiksCubeEventArgs e);
}