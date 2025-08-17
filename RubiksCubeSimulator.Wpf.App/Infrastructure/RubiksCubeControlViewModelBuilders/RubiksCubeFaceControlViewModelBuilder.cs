using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.App.Infrastructure.RubiksCubeControlViewModelBuilders;

internal interface IRubiksCubeFaceControlViewModelBuilder
{
    public RubiksCubeFaceControlViewModel Build(int cubeDimension, RubiksCubeFace cubeFace);
}

internal sealed class RubiksCubeFaceControlViewModelBuilder(
    IRubiksCubeStickerControlViewModelBuilder stickerViewModelBuilder)
    : IRubiksCubeFaceControlViewModelBuilder
{
    public RubiksCubeFaceControlViewModel Build(int cubeDimension, RubiksCubeFace cubeFace)
    {
        var stickerViewModels = cubeFace.StickerColors
            .SelectMany(row => row)
            .Select(stickerViewModelBuilder.Build)
            .ToList();

        var viewModel = new RubiksCubeFaceControlViewModel
        {
            CubeDimension = cubeDimension,
            StickerViewModels = stickerViewModels,
        };

        return viewModel;
    }
}