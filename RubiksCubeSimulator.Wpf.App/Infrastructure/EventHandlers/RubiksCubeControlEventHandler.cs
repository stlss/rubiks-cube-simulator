using System.Windows;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.App.Infrastructure.EventHandlers;

internal interface IRubiksCubeControlEventHandler
{
    public FaceName? FaceName { get; }

    public int? StickerNumber { get; }

    public Point? RelativeMouserPosition { get; }


    public void OnRelativeMouseMoveUpFace(object? sender, RelativeMouseMoveRubiksCubeFaceEventArgs e);

    public void OnRelativeMouseMoveRightFace(object? sender, RelativeMouseMoveRubiksCubeFaceEventArgs e);

    public void OnRelativeMouseMoveLeftFace(object? sender, RelativeMouseMoveRubiksCubeFaceEventArgs e);
}

internal sealed class RubiksCubeControlEventHandler : IRubiksCubeControlEventHandler
{
    public FaceName? FaceName { get; private set; }

    public int? StickerNumber { get; private set; }

    public Point? RelativeMouserPosition { get; private set; }


    public void OnRelativeMouseMoveUpFace(object? sender, RelativeMouseMoveRubiksCubeFaceEventArgs e)
    {
        FaceName = e.RelativeMousePosition != null ? EventHandlers.FaceName.Up : null;
        StickerNumber = e.StickerNumber;
        RelativeMouserPosition = e.RelativeMousePosition;
    }

    public void OnRelativeMouseMoveRightFace(object? sender, RelativeMouseMoveRubiksCubeFaceEventArgs e)
    {
        FaceName = e.RelativeMousePosition != null ? EventHandlers.FaceName.Right : null;
        StickerNumber = e.StickerNumber;
        RelativeMouserPosition = e.RelativeMousePosition;
    }

    public void OnRelativeMouseMoveLeftFace(object? sender, RelativeMouseMoveRubiksCubeFaceEventArgs e)
    {
        FaceName = e.RelativeMousePosition != null ? EventHandlers.FaceName.Left : null;
        StickerNumber = e.StickerNumber;
        RelativeMouserPosition = e.RelativeMousePosition;
    }
}

internal enum FaceName
{
    Up,
    Right,
    Left,
}