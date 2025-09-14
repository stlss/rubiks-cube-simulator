using RubiksCubeSimulator.Wpf.Events;
using RubiksCubeSimulator.Wpf.Events.EventArgs.Enums;
using RubiksCubeSimulator.Wpf.Events.EventArgs.KeyRubiksCubeEventArgs;
using RubiksCubeSimulator.Wpf.Events.EventArgs.MouseRubiksCubeEventArgs;
using RubiksCubeSimulator.Wpf.Events.EventArgs.MoveRubiksCubeEventArgs;

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

    public void OnEvent(object sender, MovingRubiksCubeEventArgs inputKeyCubeEventArgs)
    {
        _lastMovingRubiksCubeEventArgs = inputKeyCubeEventArgs;
    }

    public void OnEvent(object sender, MouseMoveRubiksCubeEventArgs inputKeyCubeEventArgs)
    {
        lock (_lock) _lastMouseMoveEventArgs = inputKeyCubeEventArgs;
    }

    public void OnEvent(object sender, InputKeyRubiksCubeEventArgs inputKeyCubeEventArgs)
    {
        if (inputKeyCubeEventArgs.KeyAction != KeyAction.Up || inputKeyCubeEventArgs.InputKey == InputKey.Shift) return;

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