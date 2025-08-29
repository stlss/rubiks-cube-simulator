using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.App.Infrastructure.EventHandlers.RubiksCube;

internal interface IMoveRubiksCubeEventHandlerFactory
{
    public IReadOnlyList<IMoveRubiksCubeEventHandler> CreateMoveRubiksCubeEventHandlers(
        RubiksCubeControlViewModel cubeViewModel);
}

internal sealed class MoveRubiksCubeEventHandlerFactory : IMoveRubiksCubeEventHandlerFactory
{
    public IReadOnlyList<IMoveRubiksCubeEventHandler> CreateMoveRubiksCubeEventHandlers(
        RubiksCubeControlViewModel cubeViewModel)
    {
        return new List<IMoveRubiksCubeEventHandler>
        {
            new RubiksCubeMoveArrowSetter(cubeViewModel)
        };
    }
}