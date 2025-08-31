using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.App.Infrastructure.RubiksCubeControlViewModelMappers;

internal interface IRubiksCubeFaceControlViewModelMapper
{
    public RubiksCubeFaceControlViewModel Map(RubiksCubeFace cubeFace);

    public void Map(RubiksCubeFace cubeFace, RubiksCubeFaceControlViewModel cubeFaceViewModel);
}

internal sealed class RubiksCubeFaceControlViewModelMappers(
    IRubiksCubeStickerControlViewModelMapper stickerViewModelMapper)
    : IRubiksCubeFaceControlViewModelMapper
{
    public RubiksCubeFaceControlViewModel Map(RubiksCubeFace cubeFace)
    {
        var cubeDimension = cubeFace.StickerColors.Length;

        var stickerNumbers = Enumerable.Range(0, cubeDimension * cubeDimension);
        var stickerColors = cubeFace.StickerColors.SelectMany(row => row);

        var stickerViewModels = stickerNumbers
            .Zip(stickerColors, (number, color) => (Number: number, Color: color))
            .Select(pair => stickerViewModelMapper.Map(pair.Number, pair.Color))
            .ToList();

        var viewModel = new RubiksCubeFaceControlViewModel
        {
            CubeDimension = cubeDimension,
            StickerViewModels = stickerViewModels,
        };

        return viewModel;
    }

    public void Map(RubiksCubeFace cubeFace, RubiksCubeFaceControlViewModel cubeFaceViewModel)
    {
        var stickersWithViewModels = cubeFace.StickerColors
            .SelectMany(row => row)
            .Zip(cubeFaceViewModel.StickerViewModels,
                (sticker, stickerViewModel) => (Sticker: sticker, StickerViewModel: stickerViewModel));

        foreach (var (sticker, stickerViewModel) in stickersWithViewModels)
        {
            stickerViewModelMapper.Map(sticker, stickerViewModel);
        }
    }
}