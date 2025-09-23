using System.Windows.Input;
using RubiksCubeSimulator.Wpf.Events;
using RubiksCubeSimulator.Wpf.Events.EventArgs;
using RubiksCubeSimulator.Wpf.Events.EventArgs.Enums;

namespace RubiksCubeSimulator.Wpf.Infrastructure.EventPublishers;

public interface IKeyRubiksCubePublisher :
    IPublisher<InputKeyRubiksCubeEventArgs>,
    ISubscriber<KeyEventArgs>
{
}

internal class KeyRubiksCubePublisher :
    PublisherBase<InputKeyRubiksCubeEventArgs>,
    IKeyRubiksCubePublisher
{
    private InputKey? _inputMoveKey;

    public void OnEvent(object sender, KeyEventArgs inputKeyCubeEventArgs)
    {
        if (inputKeyCubeEventArgs.IsDown) OnKeyDown(inputKeyCubeEventArgs);
        else if (inputKeyCubeEventArgs.IsUp) OnKeyUp(inputKeyCubeEventArgs);
    }

    private void OnKeyDown(KeyEventArgs keyEventArgs)
    {
        var inputKey = ConvertToInputKey(keyEventArgs.Key);
        if (inputKey == null) return;

        if (inputKey == InputKey.Shift)
        {
            NotifySubscribers(CreateKeyCubeEventArgs(KeyAction.Down, InputKey.Shift));
        }
        else if (_inputMoveKey == null)
        {
            NotifySubscribers(CreateKeyCubeEventArgs(KeyAction.Down, inputKey.Value));
            _inputMoveKey = inputKey;
        }
    }

    private void OnKeyUp(KeyEventArgs keyEventArgs)
    {
        var inputKey = ConvertToInputKey(keyEventArgs.Key);
        if (inputKey == null) return;

        if (inputKey == InputKey.Shift)
        {
            NotifySubscribers(CreateKeyCubeEventArgs(KeyAction.Up, InputKey.Shift));
        }
        else if (inputKey == _inputMoveKey)
        {
            NotifySubscribers(CreateKeyCubeEventArgs(KeyAction.Up, inputKey.Value));
            _inputMoveKey = null;
        }
    }

    private static InputKey? ConvertToInputKey(Key key)
    {
        return key switch
        {
            Key.W => InputKey.W,
            Key.A => InputKey.A,
            Key.S => InputKey.S,
            Key.D => InputKey.D,
            Key.LeftShift => InputKey.Shift,
            _ => null,
        };
    }

    private static InputKeyRubiksCubeEventArgs CreateKeyCubeEventArgs(KeyAction keyAction, InputKey inputKey)
    {
        return new InputKeyRubiksCubeEventArgs
        {
            InputKey = inputKey,
            KeyAction = keyAction,
        };
    }
}