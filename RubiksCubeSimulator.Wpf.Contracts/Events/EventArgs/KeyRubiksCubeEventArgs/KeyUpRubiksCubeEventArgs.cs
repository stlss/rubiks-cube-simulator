using RubiksCubeSimulator.Wpf.Contracts.Events.EventArgs.Enums;

namespace RubiksCubeSimulator.Wpf.Contracts.Events.EventArgs.KeyRubiksCubeEventArgs;

public sealed class KeyUpRubiksCubeEventArgs : System.EventArgs
{
    public required MoveKey MoveKey { get; init; }

    public required bool ShiftPressed { get; init; }
}