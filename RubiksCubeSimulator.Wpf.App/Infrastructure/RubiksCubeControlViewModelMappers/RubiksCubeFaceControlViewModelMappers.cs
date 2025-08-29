using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.App.Infrastructure.RubiksCubeControlViewModelMappers;

internal interface IRubiksCubeFaceControlViewModelMapper
{
    public RubiksCubeFaceControlViewModel Map(int cubeDimension, RubiksCubeFace cubeFace);
}

internal sealed class RubiksCubeFaceControlViewModelMappers(
    IRubiksCubeStickerControlViewModelMapper stickerViewModelMapper)
    : IRubiksCubeFaceControlViewModelMapper
{
    public RubiksCubeFaceControlViewModel Map(int cubeDimension, RubiksCubeFace cubeFace)
    {
        var stickerViewModels = cubeFace.StickerColors
            .SelectMany(row => row)
            .Select(stickerViewModelMapper.Map)
            .ToList();

        var viewModel = new RubiksCubeFaceControlViewModel
        {
            CubeDimension = cubeDimension,
            StickerViewModels = stickerViewModels,
        };

        return viewModel;
    }
}