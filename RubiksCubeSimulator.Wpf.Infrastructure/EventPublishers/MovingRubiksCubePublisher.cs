using RubiksCubeSimulator.Wpf.Contracts.Events;
using RubiksCubeSimulator.Wpf.Contracts.Events.EventArgs.KeyRubiksCubeEventArgs;
using RubiksCubeSimulator.Wpf.Contracts.Events.EventArgs.MouseRubiksCubeEventArgs;
using RubiksCubeSimulator.Wpf.Contracts.Events.EventArgs.MoveRubiksCubeEventArgs;

namespace RubiksCubeSimulator.Wpf.Infrastructure.EventPublishers;

public sealed class MovingRubiksCubePublisher :
    PublisherBase<MovingRubiksCubeEventArgs>,
    ISubscriber<MouseMoveRubiksCubeEventArgs>,
    ISubscriber<KeyDownRubiksCubeEventArgs>
{
    private MouseMoveRubiksCubeEventArgs? _lastMouseMoveEventArgs;
    private readonly object _lock = new();

    public void OnEvent(object sender, MouseMoveRubiksCubeEventArgs mouseMoveEventArgs)
    {
        lock (_lock)
        {
            _lastMouseMoveEventArgs = mouseMoveEventArgs;
        }
    }

    public void OnEvent(object sender, KeyDownRubiksCubeEventArgs keyDownEventArgs)
    {
        MouseMoveRubiksCubeEventArgs? lastMouseMoveEventArgsSnapshot;

        lock (_lock)
        {
            lastMouseMoveEventArgsSnapshot = _lastMouseMoveEventArgs;
        }

        if (lastMouseMoveEventArgsSnapshot == null || lastMouseMoveEventArgsSnapshot.MouseLeaved) return;

        var cubeMovingEventArgs = new MovingRubiksCubeEventArgs
        {
            FaceName = lastMouseMoveEventArgsSnapshot.FaceName!.Value,
            StickerNumber = lastMouseMoveEventArgsSnapshot.StickerNumber!.Value,
            RelativeMousePosition = lastMouseMoveEventArgsSnapshot.RelativeMousePosition!.Value,
            MoveKey = keyDownEventArgs.MoveKey,
            ShiftPressed = keyDownEventArgs.ShiftPressed,
        };

        NotifySubscribers(cubeMovingEventArgs);
    }
}