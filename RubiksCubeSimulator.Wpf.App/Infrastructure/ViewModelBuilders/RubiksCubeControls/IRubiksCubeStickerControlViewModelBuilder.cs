using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.App.Infrastructure.ViewModelBuilders.RubiksCubeControls;

internal interface IRubiksCubeStickerControlViewModelBuilder
{
    public RubiksCubeStickerControlViewModel Build(RubiksCubeColor color);
}

internal sealed class RubiksCubeStickerControlViewModelBuilder : IRubiksCubeStickerControlViewModelBuilder
{
    public RubiksCubeStickerControlViewModel Build(RubiksCubeColor color)
    {
        throw new NotImplementedException();
    }
}