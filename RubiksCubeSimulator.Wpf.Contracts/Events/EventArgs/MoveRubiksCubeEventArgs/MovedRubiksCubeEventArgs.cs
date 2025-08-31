using System.Windows;
using RubiksCubeSimulator.Wpf.Contracts.Events.EventArgs.Enums;

namespace RubiksCubeSimulator.Wpf.Contracts.Events.EventArgs.MoveRubiksCubeEventArgs;

public sealed class MovedRubiksCubeEventArgs : System.EventArgs
{
    public required FaceName FaceName { get; init; }

    public required int StickerNumber { get; init; }

    public required Point RelativeMousePosition { get; init; }


    public required MoveKey MoveKey { get; init; }

    public required bool ShiftPressed { get; init; }

    public required bool MouseInsideCube { get; init; }
}