using System.Windows.Input;
using RubiksCubeSimulator.Wpf.Events;
using RubiksCubeSimulator.Wpf.Events.EventArgs.Enums;
using RubiksCubeSimulator.Wpf.Events.EventArgs.KeyRubiksCubeEventArgs;

namespace RubiksCubeSimulator.Wpf.Infrastructure.EventPublishers;

public interface IKeyRubiksCubePublisher :
    IPublisher<KeyRubiksCubeEventArgs>,
    ISubscriber<KeyEventArgs>
{
}

internal class KeyRubiksCubePublisher :
    PublisherBase<KeyRubiksCubeEventArgs>,
    IKeyRubiksCubePublisher
{
    private MoveKey? _moveKey;

    public void OnEvent(object sender, KeyEventArgs keyEventArgs)
    {
        if (keyEventArgs.IsDown) OnKeyDown(keyEventArgs);
        else if (keyEventArgs.IsUp) OnKeyUp(keyEventArgs);
    }

    private void OnKeyDown(KeyEventArgs keyEventArgs)
    {
        var moveKey = ConvertToMoveKey(keyEventArgs.Key);
        if (moveKey == null || _moveKey != null) return;

        _moveKey = moveKey;
        NotifySubscribers(CreateKeyCubeEventArgs(KeyAction.Down));
    }

    private void OnKeyUp(KeyEventArgs keyEventArgs)
    {
        var moveKey = ConvertToMoveKey(keyEventArgs.Key);
        if (moveKey == null || moveKey != _moveKey) return;

        NotifySubscribers(CreateKeyCubeEventArgs(KeyAction.Up));
        _moveKey = null;
    }

    private static MoveKey? ConvertToMoveKey(Key key)
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

    private KeyRubiksCubeEventArgs CreateKeyCubeEventArgs(KeyAction keyAction)
    {
        return new KeyRubiksCubeEventArgs
        {
            MoveKey = _moveKey!.Value,
            KeyAction = keyAction,
        };
    }
}