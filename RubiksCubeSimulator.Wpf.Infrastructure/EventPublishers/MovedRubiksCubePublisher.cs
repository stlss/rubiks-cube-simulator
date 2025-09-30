using RubiksCubeSimulator.Wpf.Events;
using RubiksCubeSimulator.Wpf.Events.EventArgs;
using RubiksCubeSimulator.Wpf.Events.EventArgs.Enums;

namespace RubiksCubeSimulator.Wpf.Infrastructure.EventPublishers;

public interface IMovedRubiksCubePublisher :
    IPublisher<MovedRubiksCubeEventArgs>,
    ISubscriber<MovingRubiksCubeEventArgs>,
    ISubscriber<MouseMoveRubiksCubeEventArgs>,
    ISubscriber<InputKeyRubiksCubeEventArgs>
{
}

internal sealed class MovedRubiksCubePublisher :
    PublisherBase<MovedRubiksCubeEventArgs>,
    IMovedRubiksCubePublisher
{
    private MovingRubiksCubeEventArgs _lastMovingRubiksCubeEventArgs = null!;
    private MouseMoveRubiksCubeEventArgs? _lastMouseMoveEventArgs;
    private readonly object _lock = new();

    public void OnEvent(object sender, MovingRubiksCubeEventArgs movingCubeEventArgs)
    {
        _lastMovingRubiksCubeEventArgs = movingCubeEventArgs;
    }

    public void OnEvent(object sender, MouseMoveRubiksCubeEventArgs movingCubeEventArgs)
    {
        lock (_lock) _lastMouseMoveEventArgs = movingCubeEventArgs;
    }

    public void OnEvent(object sender, InputKeyRubiksCubeEventArgs movingCubeEventArgs)
    {
        if (movingCubeEventArgs.KeyAction != KeyAction.Up || movingCubeEventArgs.InputKey == InputKey.Shift) return;

        MouseMoveRubiksCubeEventArgs? lastMouseMoveEventArgsSnapshot;
        lock (_lock) lastMouseMoveEventArgsSnapshot = _lastMouseMoveEventArgs;
        if (lastMouseMoveEventArgsSnapshot == null) return;

        var cubeMovedEventArgs = new MovedRubiksCubeEventArgs
        {
            FaceName = _lastMovingRubiksCubeEventArgs.FaceName,
            StickerNumber = _lastMovingRubiksCubeEventArgs.StickerNumber,
            RelativeMousePosition = _lastMovingRubiksCubeEventArgs.RelativeMousePosition,
            MoveKey = _lastMovingRubiksCubeEventArgs.MoveKey,
            ShiftPressed = _lastMovingRubiksCubeEventArgs.ShiftPressed,
            MoveCanceled = lastMouseMoveEventArgsSnapshot.RelativeMousePosition == null,
        };

        NotifySubscribers(cubeMovedEventArgs);
    }
}