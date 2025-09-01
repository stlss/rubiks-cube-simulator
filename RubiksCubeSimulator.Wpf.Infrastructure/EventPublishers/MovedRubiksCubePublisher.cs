using RubiksCubeSimulator.Wpf.Events;
using RubiksCubeSimulator.Wpf.Events.EventArgs.KeyRubiksCubeEventArgs;
using RubiksCubeSimulator.Wpf.Events.EventArgs.MouseRubiksCubeEventArgs;
using RubiksCubeSimulator.Wpf.Events.EventArgs.MoveRubiksCubeEventArgs;

namespace RubiksCubeSimulator.Wpf.Infrastructure.EventPublishers;

public sealed class MovedRubiksCubePublisher :
    PublisherBase<MovedRubiksCubeEventArgs>,
    ISubscriber<MouseMoveRubiksCubeEventArgs>,
    ISubscriber<KeyUpRubiksCubeEventArgs>
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

    public void OnEvent(object sender, KeyUpRubiksCubeEventArgs keyUpEventArgs)
    {
        MouseMoveRubiksCubeEventArgs? lastMouseMoveEventArgsSnapshot;

        lock (_lock)
        {
            lastMouseMoveEventArgsSnapshot = _lastMouseMoveEventArgs;
        }

        if (lastMouseMoveEventArgsSnapshot == null) return;

        var cubeMovingEventArgs = new MovedRubiksCubeEventArgs
        {
            FaceName = lastMouseMoveEventArgsSnapshot.FaceName,
            StickerNumber = lastMouseMoveEventArgsSnapshot.StickerNumber,
            RelativeMousePosition = lastMouseMoveEventArgsSnapshot.RelativeMousePosition,
            MoveKey = keyUpEventArgs.MoveKey,
            ShiftPressed = keyUpEventArgs.ShiftPressed,
        };

        NotifySubscribers(cubeMovingEventArgs);
    }
}