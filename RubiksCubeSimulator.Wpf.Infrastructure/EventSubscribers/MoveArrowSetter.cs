using RubiksCubeSimulator.Wpf.Events;
using RubiksCubeSimulator.Wpf.Events.EventArgs;
using RubiksCubeSimulator.Wpf.Infrastructure.MoveServices;
using RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext;

namespace RubiksCubeSimulator.Wpf.Infrastructure.EventSubscribers;

public interface IMoveArrowSetter :
    ISubscriber<MovingRubiksCubeEventArgs>,
    ISubscriber<MovedRubiksCubeEventArgs>
{
}

internal sealed class MoveArrowSetter(
    IRubiksCubeContext cubeContext,
    IMoveBuilder moveBuilder) : IMoveArrowSetter
{
    public void OnEvent(object sender, MovingRubiksCubeEventArgs movingCubeEventArgs)
    {
        var move = moveBuilder.Build(
            cubeDimension: cubeContext.CubeViewModel.CubeDimension,
            movingCubeEventArgs.FaceName,
            movingCubeEventArgs.StickerNumber,
            movingCubeEventArgs.RelativeMousePosition,
            movingCubeEventArgs.MoveKey,
            movingCubeEventArgs.ShiftPressed);

        cubeContext.ShowMoveArrows(move);
    }

    public void OnEvent(object sender, MovedRubiksCubeEventArgs movedCubeEventArgs)
    {
        cubeContext.ClearMoveArrows();
    }
}