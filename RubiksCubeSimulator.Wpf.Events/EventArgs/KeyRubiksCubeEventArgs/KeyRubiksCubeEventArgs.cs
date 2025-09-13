using RubiksCubeSimulator.Wpf.Events.EventArgs.Enums;

namespace RubiksCubeSimulator.Wpf.Events.EventArgs.KeyRubiksCubeEventArgs;

public sealed class KeyRubiksCubeEventArgs : System.EventArgs
{
    public KeyAction KeyAction { get; init; }

    public required MoveKey MoveKey { get; init; }
}