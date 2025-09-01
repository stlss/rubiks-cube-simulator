using System.Windows.Input;
using RubiksCubeSimulator.Wpf.Events.EventArgs.Enums;

namespace RubiksCubeSimulator.Wpf.Events.EventArgs.KeyRubiksCubeEventArgs;

public sealed class KeyRubiksCubeEventArgs : System.EventArgs
{
    public required MoveKey MoveKey { get; init; }

    public required bool ShiftPressed { get; init; }

    public KeyAction KeyAction { get; init; }
}