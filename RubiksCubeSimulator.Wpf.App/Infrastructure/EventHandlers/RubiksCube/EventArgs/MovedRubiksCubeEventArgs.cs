using System.Windows;
using System.Windows.Input;

namespace RubiksCubeSimulator.Wpf.App.Infrastructure.EventHandlers.RubiksCube.EventArgs;

internal sealed class MovedRubiksCubeEventArgs : System.EventArgs
{
    public required FaceName FaceName { get; init; }

    public required int StickerNumber { get; init; }

    public required Point RelativeMousePosition { get; init; }


    public required Key PressedMoveKey { get; init; }

    public required bool PressedShift { get; init; }
}