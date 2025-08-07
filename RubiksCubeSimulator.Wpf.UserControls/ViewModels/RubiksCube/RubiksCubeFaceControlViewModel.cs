using System.Windows;

namespace RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

public sealed class RubiksCubeFaceControlViewModel
{
    private const int DefaultDimension = 3;

    public IReadOnlyList<RubiksCubeStickerControlViewModel> StickerControlViewModels { get; init; } =
        Enumerable.Repeat(new RubiksCubeStickerControlViewModel(), DefaultDimension * DefaultDimension).ToList();

    public int Dimension => (int)Math.Sqrt(StickerControlViewModels.Count);

    public Thickness BorderThickness => new(1.5 * DefaultDimension / Dimension);
}