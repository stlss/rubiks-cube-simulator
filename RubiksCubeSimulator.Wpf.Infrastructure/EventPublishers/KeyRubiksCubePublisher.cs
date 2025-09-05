using System.Windows.Input;
using RubiksCubeSimulator.Wpf.Events;
using RubiksCubeSimulator.Wpf.Events.EventArgs.Enums;
using RubiksCubeSimulator.Wpf.Events.EventArgs.KeyRubiksCubeEventArgs;

namespace RubiksCubeSimulator.Wpf.Infrastructure.EventPublishers;

public interface IKeyRubiksCubePublisher : IPublisher<KeyRubiksCubeEventArgs>
{
}

internal class KeyRubiksCubePublisher :
    PublisherBase<KeyRubiksCubeEventArgs>,
    IKeyRubiksCubePublisher,
    ISubscriber<KeyEventArgs>
{
    private MoveKey? _moveKey;
    private bool _shiftPressed;


    public void OnEvent(object sender, KeyEventArgs keyEventArgs)
    {
        if (keyEventArgs.IsDown) OnKeyDown(keyEventArgs);
        else if (keyEventArgs.IsUp) OnKeyUp(keyEventArgs);
    }

    private void OnKeyDown(KeyEventArgs keyEventArgs)
    {
        if (IsShift(keyEventArgs.Key))
        {
            _shiftPressed = true;

            if (_moveKey != null) NotifySubscribers(CreateKeyRubiksEventArgs());

            return;
        }

        var moveKey = ConvertToMoveKey(keyEventArgs.Key);

        if (moveKey != null && _moveKey == null)
        {
            _moveKey = moveKey;
            NotifySubscribers(CreateKeyRubiksEventArgs());
        }

        return;

        KeyRubiksCubeEventArgs CreateKeyRubiksEventArgs()
        {
            return new KeyRubiksCubeEventArgs
            {
                MoveKey = _moveKey!.Value,
                ShiftPressed = _shiftPressed,
                KeyAction = KeyAction.Down,
            };
        }
    }

    private void OnKeyUp(KeyEventArgs keyEventArgs)
    {
        if (IsShift(keyEventArgs.Key))
        {
            _shiftPressed = false;

            if (_moveKey != null) NotifySubscribers(CreateKeyRubiksEventArgs());

            return;
        }

        var moveKey = ConvertToMoveKey(keyEventArgs.Key);

        if (moveKey != null && moveKey == _moveKey)
        {
            NotifySubscribers(CreateKeyRubiksEventArgs());
            _moveKey = null;
        }

        return;

        KeyRubiksCubeEventArgs CreateKeyRubiksEventArgs()
        {
            return new KeyRubiksCubeEventArgs
            {
                MoveKey = _moveKey!.Value,
                ShiftPressed = _shiftPressed,
                KeyAction = KeyAction.Up,
            };
        }
    }


    private static bool IsShift(Key key) => key == Key.LeftShift;

    private MoveKey? ConvertToMoveKey(Key key)
    {
        return key switch
        {
            Key.W => MoveKey.W,
            Key.A => MoveKey.A,
            Key.S => MoveKey.S,
            Key.D => MoveKey.D,
            _ => null,
        };
    }
}