using System.Windows.Media;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.App.Infrastructure.ViewModelBuilders.RubiksCubeControls;

internal interface IRubiksCubeStickerControlViewModelBuilder
{
    public RubiksCubeStickerControlViewModel Build(RubiksCubeColor color);
}

internal sealed class RubiksCubeStickerControlViewModelBuilder : IRubiksCubeStickerControlViewModelBuilder
{
    private static readonly Dictionary<RubiksCubeColor, SolidColorBrush> BrushByColor = new()
    {
        [RubiksCubeColor.White] = CreateBrush(255, 255, 255),
        [RubiksCubeColor.Blue] = CreateBrush(36,90,255),
        [RubiksCubeColor.Red] = CreateBrush(222,28,48),
        [RubiksCubeColor.Yellow] = CreateBrush(240,255,16),
        [RubiksCubeColor.Green] = CreateBrush(98,232,102),
        [RubiksCubeColor.Orange] = CreateBrush(248,176,24),
    };

    private static SolidColorBrush CreateBrush(byte r, byte g, byte b)
    {
        return new SolidColorBrush(Color.FromRgb(r, g, b));
    }

    public RubiksCubeStickerControlViewModel Build(RubiksCubeColor color)
    {
        var brush = BrushByColor[color];

        var viewModel = new RubiksCubeStickerControlViewModel
        {
            StickerColorBrush = brush,
        };

        return viewModel;
    }
}