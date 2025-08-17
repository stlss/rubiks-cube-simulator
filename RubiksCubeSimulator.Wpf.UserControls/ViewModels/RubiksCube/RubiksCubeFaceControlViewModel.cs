using System.Windows;

namespace RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

public sealed class RubiksCubeFaceControlViewModel
{
    private const int DefaultDimension = 3;

    public IReadOnlyList<RubiksCubeStickerControlViewModel> StickerViewModels { get; init; } =
        Enumerable.Range(0, DefaultDimension * DefaultDimension)
            .Select(_ => new RubiksCubeStickerControlViewModel())
            .ToList();

    public int CubeDimension { get; init; } = DefaultDimension;

    public Thickness BorderThickness => new(1.5 * DefaultDimension / CubeDimension);


    public void ClearMoveArrows()
    {
        foreach (var stickerViewModel in StickerViewModels) stickerViewModel.ArrowDirection = null;
    }

    internal void SetMoveArrows(ArrowDirection arrowDirection)
    {
        foreach (var stickerViewModel in StickerViewModels) stickerViewModel.ArrowDirection = arrowDirection;
    }

    internal void SetRowMoveArrows(ArrowDirection arrowDirection, int row)
    {
        var start = row * CubeDimension;
        var end = start + CubeDimension;

        for (var i = start; i < end; i++) StickerViewModels[i].ArrowDirection = arrowDirection;
    }

    internal void SetColumnMoveArrows(ArrowDirection arrowDirection, int column)
    {
        var start = column;
        var end = StickerViewModels.Count;
        var step = CubeDimension;

        for (var i = start; i < end; i += step) StickerViewModels[i].ArrowDirection = arrowDirection;
    }
}