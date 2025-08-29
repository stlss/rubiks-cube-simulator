using System.Windows.Media;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.App.Infrastructure.RubiksCubeControlViewModelMappers;

internal interface IRubiksCubeStickerControlViewModelMapper
{
    public RubiksCubeStickerControlViewModel Map(RubiksCubeStickerColor stickerColor);

    public void Map(RubiksCubeStickerColor stickerColor, RubiksCubeStickerControlViewModel stickerViewModel);
}

internal sealed class RubiksCubeStickerControlViewModelMappers : IRubiksCubeStickerControlViewModelMapper
{
    private static readonly Dictionary<RubiksCubeStickerColor, SolidColorBrush> BrushByStickerColor = new()
    {
        [RubiksCubeStickerColor.White] = CreateBrush(255, 255, 255),
        [RubiksCubeStickerColor.Blue] = CreateBrush(36,90,255),
        [RubiksCubeStickerColor.Red] = CreateBrush(222,28,48),
        [RubiksCubeStickerColor.Yellow] = CreateBrush(240,255,16),
        [RubiksCubeStickerColor.Green] = CreateBrush(98,232,102),
        [RubiksCubeStickerColor.Orange] = CreateBrush(248,176,24),
    };

    private static SolidColorBrush CreateBrush(byte r, byte g, byte b)
    {
        return new SolidColorBrush(Color.FromRgb(r, g, b));
    }

    public RubiksCubeStickerControlViewModel Map(RubiksCubeStickerColor stickerColor)
    {
        var brush = BrushByStickerColor[stickerColor];

        var viewModel = new RubiksCubeStickerControlViewModel
        {
            StickerColorBrush = brush,
        };

        return viewModel;
    }

    public void Map(RubiksCubeStickerColor stickerColor, RubiksCubeStickerControlViewModel stickerViewModel)
    {
        stickerViewModel.StickerColorBrush = BrushByStickerColor[stickerColor];
    }
}