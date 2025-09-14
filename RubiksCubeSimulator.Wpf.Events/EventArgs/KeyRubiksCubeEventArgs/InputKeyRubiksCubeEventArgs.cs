using RubiksCubeSimulator.Wpf.Events.EventArgs.Enums;

namespace RubiksCubeSimulator.Wpf.Events.EventArgs.KeyRubiksCubeEventArgs;

public sealed class InputKeyRubiksCubeEventArgs : System.EventArgs
{
    public required KeyAction KeyAction { get; init; }

    public required InputKey InputKey { get; init; }
}