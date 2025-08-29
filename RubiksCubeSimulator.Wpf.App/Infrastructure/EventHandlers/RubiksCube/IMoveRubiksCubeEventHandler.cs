using RubiksCubeSimulator.Wpf.App.Infrastructure.EventHandlers.RubiksCube.EventArgs;

namespace RubiksCubeSimulator.Wpf.App.Infrastructure.EventHandlers.RubiksCube;

internal interface IMoveRubiksCubeEventHandler
{
    public void OnMovingRubiksCube(object? sender, MovingRubiksCubeEventArgs e);

    public void OnMovedRubiksCube(object? sender, MovedRubiksCubeEventArgs e);
}