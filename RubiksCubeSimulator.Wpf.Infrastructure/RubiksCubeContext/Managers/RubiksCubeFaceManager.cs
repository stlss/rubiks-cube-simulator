using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;
using RubiksCubeSimulator.Wpf.Events.EventArgs.Enums;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext.Managers;

internal interface IRubiksCubeFaceManager
{
    public RubiksCubeFaceControlViewModel Create(FaceName faceName, RubiksCubeFace cubeFace);

    public void Update(RubiksCubeFaceControlViewModel cubeFaceViewModel, RubiksCubeFace cubeFace);
}

internal sealed class RubiksCubeFaceManager(
    IRubiksCubeStickerColorManager stickerViewModelManager)
    : IRubiksCubeFaceManager
{
    public RubiksCubeFaceControlViewModel Create(FaceName faceName, RubiksCubeFace cubeFace)
    {
        var cubeDimension = cubeFace.StickerColors.Length;

        var stickerNumbers = Enumerable.Range(0, cubeDimension * cubeDimension);
        var stickerColors = cubeFace.StickerColors.SelectMany(row => row);

        var stickerViewModels = stickerNumbers
            .Zip(stickerColors, (number, color) => (Number: number, Color: color))
            .Select(pair => stickerViewModelManager.Create(pair.Number, pair.Color))
            .ToList();

        var viewModel = new RubiksCubeFaceControlViewModel
        {
            FaceName = faceName,
            CubeDimension = cubeDimension,
            StickerViewModels = stickerViewModels,
        };

        return viewModel;
    }

    public void Update(RubiksCubeFaceControlViewModel cubeFaceViewModel, RubiksCubeFace cubeFace)
    {
        var stickersWithViewModels = cubeFace.StickerColors
            .SelectMany(row => row)
            .Zip(cubeFaceViewModel.StickerViewModels,
                (sticker, stickerViewModel) => (Sticker: sticker, StickerViewModel: stickerViewModel));

        foreach (var (sticker, stickerViewModel) in stickersWithViewModels)
        {
            stickerViewModelManager.Update(stickerViewModel, sticker);
        }
    }
}