using RubiksCubeSimulator.Wpf.Events;
using RubiksCubeSimulator.Wpf.Events.EventArgs.Enums;
using RubiksCubeSimulator.Wpf.Events.EventArgs.KeyRubiksCubeEventArgs;
using RubiksCubeSimulator.Wpf.Events.EventArgs.MouseRubiksCubeEventArgs;
using RubiksCubeSimulator.Wpf.Events.EventArgs.MoveRubiksCubeEventArgs;

namespace RubiksCubeSimulator.Wpf.Infrastructure.EventPublishers;

public interface IMovingRubiksCubePublisher :
    IPublisher<MovingRubiksCubeEventArgs>,
    ISubscriber<MouseMoveRubiksCubeEventArgs>,
    ISubscriber<KeyRubiksCubeEventArgs>
{
}

internal sealed class MovingRubiksCubePublisher :
    PublisherBase<MovingRubiksCubeEventArgs>,
    IMovingRubiksCubePublisher
{
    private MouseMoveRubiksCubeEventArgs? _lastMouseMoveEventArgs;
    private readonly object _lock = new();

    public void OnEvent(object sender, MouseMoveRubiksCubeEventArgs keyEventArgs)
    {
        lock (_lock)
        {
            _lastMouseMoveEventArgs = keyEventArgs;
        }
    }

    public void OnEvent(object sender, KeyRubiksCubeEventArgs keyEventArgs)
    {
        if (keyEventArgs.KeyAction != KeyAction.Down) return;

        MouseMoveRubiksCubeEventArgs? lastMouseMoveEventArgsSnapshot;

        lock (_lock)
        {
            lastMouseMoveEventArgsSnapshot = _lastMouseMoveEventArgs;
        }

        if (lastMouseMoveEventArgsSnapshot == null) return;

        var relativeMousePosition = lastMouseMoveEventArgsSnapshot.RelativeMousePosition;
        if (relativeMousePosition == null) return;

        var cubeMovingEventArgs = new MovingRubiksCubeEventArgs
        {
            FaceName = lastMouseMoveEventArgsSnapshot.FaceName,
            StickerNumber = lastMouseMoveEventArgsSnapshot.StickerNumber,
            RelativeMousePosition = relativeMousePosition.Value,
            MoveKey = keyEventArgs.MoveKey,
            ShiftPressed = keyEventArgs.ShiftPressed,
        };

        NotifySubscribers(cubeMovingEventArgs);
    }
}