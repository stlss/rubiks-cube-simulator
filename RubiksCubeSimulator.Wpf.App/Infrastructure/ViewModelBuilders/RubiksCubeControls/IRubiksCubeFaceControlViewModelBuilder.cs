using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.App.Infrastructure.ViewModelBuilders.RubiksCubeControls;

internal interface IRubiksCubeFaceControlViewModelBuilder
{
    public RubiksCubeFaceControlViewModel Build(RubiksCubeFace cubeFace);
}

internal sealed class RubiksCubeFaceControlViewModelBuilder(
    IRubiksCubeStickerControlViewModelBuilder stickerViewModelBuilder)
    : IRubiksCubeFaceControlViewModelBuilder
{
    public RubiksCubeFaceControlViewModel Build(RubiksCubeFace cubeFace)
    {
        var stickerViewModels = cubeFace.StickerColors
            .SelectMany(row => row)
            .Select(stickerViewModelBuilder.Build)
            .ToList();

        var viewModel = new RubiksCubeFaceControlViewModel
        {
            StickerViewModels = stickerViewModels,
        };

        return viewModel;
    }
}