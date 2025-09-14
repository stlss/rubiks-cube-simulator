using RubiksCubeSimulator.Wpf.Events;
using RubiksCubeSimulator.Wpf.Events.EventArgs.Enums;
using RubiksCubeSimulator.Wpf.Events.EventArgs.KeyRubiksCubeEventArgs;
using RubiksCubeSimulator.Wpf.Events.EventArgs.MouseRubiksCubeEventArgs;
using RubiksCubeSimulator.Wpf.Events.EventArgs.MoveRubiksCubeEventArgs;

namespace RubiksCubeSimulator.Wpf.Infrastructure.EventPublishers;

public interface IMovingRubiksCubePublisher :
    IPublisher<MovingRubiksCubeEventArgs>,
    ISubscriber<MovedRubiksCubeEventArgs>,
    ISubscriber<MouseMoveRubiksCubeEventArgs>,
    ISubscriber<InputKeyRubiksCubeEventArgs>
{
}

internal sealed class MovingRubiksCubePublisher :
    PublisherBase<MovingRubiksCubeEventArgs>,
    IMovingRubiksCubePublisher
{
    private MovingRubiksCubeEventArgs? _lastMovingCubeEventArgs;
    private bool _shiftPressed;

    private MouseMoveRubiksCubeEventArgs? _lastMouseMoveEventArgs;
    private readonly object _lock = new();

    public void OnEvent(object sender, MovedRubiksCubeEventArgs inputKeyCubeEventArgs)
    {
        _lastMovingCubeEventArgs = null;
    }

    public void OnEvent(object sender, MouseMoveRubiksCubeEventArgs inputKeyCubeEventArgs)
    {
        lock (_lock) _lastMouseMoveEventArgs = inputKeyCubeEventArgs;
    }

    public void OnEvent(object sender, InputKeyRubiksCubeEventArgs inputKeyCubeEventArgs)
    {
        if (inputKeyCubeEventArgs.InputKey == InputKey.Shift)
        {
            _shiftPressed = inputKeyCubeEventArgs.KeyAction switch
            {
                KeyAction.Down => true,
                KeyAction.Up => false,
                _ => _shiftPressed
            };

            if (_lastMovingCubeEventArgs == null) return;

            _lastMovingCubeEventArgs = CreateMovingCubeEventArgs();
            NotifySubscribers(_lastMovingCubeEventArgs);

            return;
        }

        if (inputKeyCubeEventArgs.KeyAction != KeyAction.Down) return;

        MouseMoveRubiksCubeEventArgs? lastMouseMoveEventArgsSnapshot;
        lock (_lock) lastMouseMoveEventArgsSnapshot = _lastMouseMoveEventArgs;
        if (lastMouseMoveEventArgsSnapshot == null) return;

        var relativeMousePosition = lastMouseMoveEventArgsSnapshot.RelativeMousePosition;
        if (relativeMousePosition == null) return;

        _lastMovingCubeEventArgs = CreateMovingCubeEventArgs(lastMouseMoveEventArgsSnapshot, inputKeyCubeEventArgs);
        NotifySubscribers(_lastMovingCubeEventArgs );
    }

    private MovingRubiksCubeEventArgs CreateMovingCubeEventArgs()
    {
        return new MovingRubiksCubeEventArgs
        {
            FaceName = _lastMovingCubeEventArgs!.FaceName,
            StickerNumber = _lastMovingCubeEventArgs.StickerNumber,
            RelativeMousePosition = _lastMovingCubeEventArgs.RelativeMousePosition,
            MoveKey = _lastMovingCubeEventArgs.MoveKey,
            ShiftPressed = _shiftPressed,
        };
    }

    private MovingRubiksCubeEventArgs CreateMovingCubeEventArgs(
        MouseMoveRubiksCubeEventArgs mouseMoveCubeEventArgs,
        InputKeyRubiksCubeEventArgs inputKeyCubeEventArgs)
    {
        return new MovingRubiksCubeEventArgs
        {
            FaceName = mouseMoveCubeEventArgs.FaceName,
            StickerNumber = mouseMoveCubeEventArgs.StickerNumber,
            RelativeMousePosition = mouseMoveCubeEventArgs.RelativeMousePosition!.Value,
            MoveKey = (MoveKey)inputKeyCubeEventArgs.InputKey,
            ShiftPressed = _shiftPressed,
        };
    }
}