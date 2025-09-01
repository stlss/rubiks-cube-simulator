using System.Windows;
using RubiksCubeSimulator.Wpf.Events.EventArgs.Enums;

namespace RubiksCubeSimulator.Wpf.Events.EventArgs.MouseRubiksCubeEventArgs;

public sealed class MouseMoveRubiksCubeEventArgs : System.EventArgs
{
    public required FaceName FaceName { get; init; }

    public required int StickerNumber { get; init; }

    public required Point? RelativeMousePosition { get; init; }
}