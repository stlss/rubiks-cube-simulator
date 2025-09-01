using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;
using RubiksCubeSimulator.Wpf.Events.EventArgs.Enums;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext.Mappers;

internal interface IRubiksCubeFaceMapper
{
    public RubiksCubeFaceControlViewModel Map(FaceName faceName, RubiksCubeFace cubeFace);

    public void Map(RubiksCubeFace cubeFace, RubiksCubeFaceControlViewModel cubeFaceViewModel);
}

internal sealed class RubiksCubeFaceMapper(
    IRubiksCubeStickerColorMapper stickerViewModelMapper)
    : IRubiksCubeFaceMapper
{
    public RubiksCubeFaceControlViewModel Map(FaceName faceName, RubiksCubeFace cubeFace)
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
            FaceName = faceName,
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