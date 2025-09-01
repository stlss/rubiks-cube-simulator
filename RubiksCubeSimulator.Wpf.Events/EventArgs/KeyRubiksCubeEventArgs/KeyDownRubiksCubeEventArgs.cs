using RubiksCubeSimulator.Wpf.Events.EventArgs.Enums;

namespace RubiksCubeSimulator.Wpf.Events.EventArgs.KeyRubiksCubeEventArgs;

public sealed class KeyDownRubiksCubeEventArgs : System.EventArgs
{
    public required MoveKey MoveKey { get; init; }

    public required bool ShiftPressed { get; init; }
}