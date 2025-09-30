using RubiksCubeSimulator.Wpf.Events;
using RubiksCubeSimulator.Wpf.Events.EventArgs;
using RubiksCubeSimulator.Wpf.Infrastructure.MoveServices;
using RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext;

namespace RubiksCubeSimulator.Wpf.Infrastructure.EventSubscribers;

internal interface IMoveApplier :
    ISubscriber<MovedRubiksCubeEventArgs>
{
}

internal sealed class MoveApplier(
    IRubiksCubeContext cubeContext,
    IMoveBuilder moveBuilder) : IMoveApplier
{
    public void OnEvent(object sender, MovedRubiksCubeEventArgs movedCubeEventArgs)
    {
        if (movedCubeEventArgs.MoveCanceled) return;

        var move = moveBuilder.Build(
            cubeDimension: cubeContext.CubeViewModel.CubeDimension,
            movedCubeEventArgs.FaceName,
            movedCubeEventArgs.StickerNumber,
            movedCubeEventArgs.RelativeMousePosition!.Value,
            movedCubeEventArgs.MoveKey,
            movedCubeEventArgs.ShiftPressed);

        cubeContext.Move(move);
    }
}