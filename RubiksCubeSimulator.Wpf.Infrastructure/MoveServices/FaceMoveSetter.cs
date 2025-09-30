using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube.Enums;

namespace RubiksCubeSimulator.Wpf.Infrastructure.MoveServices;

internal interface IFaceMoveSetter
{
    public void SetMoveArrows(RubiksCubeFaceControlViewModel faceViewModel, ArrowDirection arrowDirection);

    public void SetRowMoveArrows(
        RubiksCubeFaceControlViewModel faceViewModel,
        ArrowDirection arrowDirection,
        int row);

    public void SetColumnMoveArrows(
        RubiksCubeFaceControlViewModel faceViewModel,
        ArrowDirection arrowDirection,
        int column);

    public void ClearMoveArrows(RubiksCubeFaceControlViewModel faceViewModel);
}

internal sealed class FaceMoveSetter : IFaceMoveSetter
{
    public void SetMoveArrows(RubiksCubeFaceControlViewModel faceViewModel, ArrowDirection arrowDirection)
    {
        foreach (var stickerViewModel in faceViewModel.StickerViewModels)
        {
            stickerViewModel.ArrowDirection = arrowDirection;
        }
    }

    public void SetRowMoveArrows(
        RubiksCubeFaceControlViewModel faceViewModel,
        ArrowDirection arrowDirection,
        int row)
    {
        var start = row * faceViewModel.CubeDimension;
        var end = start + faceViewModel.CubeDimension;

        for (var i = start; i < end; i++) faceViewModel.StickerViewModels[i].ArrowDirection = arrowDirection;
    }

    public void SetColumnMoveArrows(
        RubiksCubeFaceControlViewModel faceViewModel,
        ArrowDirection arrowDirection,
        int column)
    {
        var start = column;
        var end = faceViewModel.StickerViewModels.Count;
        var step = faceViewModel.CubeDimension;

        for (var i = start; i < end; i += step) faceViewModel.StickerViewModels[i].ArrowDirection = arrowDirection;
    }

    public void ClearMoveArrows(RubiksCubeFaceControlViewModel faceViewModel)
    {
        foreach (var stickerViewModel in faceViewModel.StickerViewModels) stickerViewModel.ArrowDirection = null;
    }
}