using System.Windows;

namespace RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

public sealed class RubiksCubeFaceControlViewModel
{
    private const int DefaultDimension = 3;

    public int Dimension { get; init; } = DefaultDimension;

    public Thickness BorderThickness => new(1.5 * DefaultDimension / Dimension);

    public IReadOnlyList<RubiksCubeStickerControlViewModel> StickerControlViewModels { get; init; } =
        Enumerable.Repeat(new RubiksCubeStickerControlViewModel(), 25).ToList();
}