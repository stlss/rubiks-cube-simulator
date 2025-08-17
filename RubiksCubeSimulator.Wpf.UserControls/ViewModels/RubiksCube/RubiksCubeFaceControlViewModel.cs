using System.Windows;

namespace RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

public sealed class RubiksCubeFaceControlViewModel
{
    private const int DefaultDimension = 3;

    public IReadOnlyList<RubiksCubeStickerControlViewModel> StickerViewModels { get; init; } =
        Enumerable.Range(0, DefaultDimension * DefaultDimension)
            .Select(_ => new RubiksCubeStickerControlViewModel())
            .ToList();

    public int Dimension => (int)Math.Sqrt(StickerViewModels.Count);

    public Thickness BorderThickness => new(1.5 * DefaultDimension / Dimension);


    public void ClearArrows()
    {
        foreach (var stickerViewModel in StickerViewModels) stickerViewModel.ArrowDirection = null;
    }

    internal void SetArrows(ArrowDirection arrowDirection)
    {
        foreach (var stickerViewModel in StickerViewModels) stickerViewModel.ArrowDirection = arrowDirection;
    }

    internal void SetRowArrows(ArrowDirection arrowDirection, int row)
    {
        var start = row * Dimension;
        var end = start + Dimension;

        for (var i = start; i < end; i++) StickerViewModels[i].ArrowDirection = arrowDirection;
    }

    internal void SetColumnArrows(ArrowDirection arrowDirection, int column)
    {
        var start = column;
        var end = StickerViewModels.Count;
        var step = Dimension;

        for (var i = start; i < end; i += step) StickerViewModels[i].ArrowDirection = arrowDirection;
    }
}