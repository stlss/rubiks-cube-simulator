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
    ISubscriber<KeyRubiksCubeEventArgs>
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

    public void OnEvent(object sender, MouseMoveRubiksCubeEventArgs mouseMoveCubeEventArgs)
    {
        lock (_lock) _lastMouseMoveEventArgs = mouseMoveCubeEventArgs;
    }

    public void OnEvent(object sender, KeyRubiksCubeEventArgs keyCubeEventArgs)
    {
        if (keyCubeEventArgs.KeyAction != KeyAction.Up) return;

        MouseMoveRubiksCubeEventArgs? lastMouseMoveEventArgsSnapshot;
        lock (_lock) lastMouseMoveEventArgsSnapshot = _lastMouseMoveEventArgs;
        if (lastMouseMoveEventArgsSnapshot == null) return;

        var cubeMovedEventArgs = new MovedRubiksCubeEventArgs
        {
            FaceName = _lastMovingRubiksCubeEventArgs.FaceName,
            StickerNumber = _lastMovingRubiksCubeEventArgs.StickerNumber,
            RelativeMousePosition = _lastMovingRubiksCubeEventArgs.RelativeMousePosition,
            MoveKey = keyCubeEventArgs.MoveKey,
            MoveCanceled = lastMouseMoveEventArgsSnapshot.RelativeMousePosition == null,
        };

        NotifySubscribers(cubeMovedEventArgs);
    }
}