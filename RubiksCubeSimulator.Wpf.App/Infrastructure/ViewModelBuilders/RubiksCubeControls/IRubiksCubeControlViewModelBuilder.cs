using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.App.Infrastructure.ViewModelBuilders.RubiksCubeControls;

internal interface IRubiksCubeControlViewModelBuilder
{
    public RubiksCubeControlViewModel Build(RubiksCube cube);
}

internal sealed class RubiksCubeControlViewModelBuilder(
    IRubiksCubeFaceControlViewModelBuilder faceViewModelBuilder)
    : IRubiksCubeControlViewModelBuilder
{
    public RubiksCubeControlViewModel Build(RubiksCube cube)
    {
        var upFaceViewModel = faceViewModelBuilder.Build(cube.UpFace);
        var rightFaceViewModel = faceViewModelBuilder.Build(cube.RightFace);
        var frontFaceViewModel = faceViewModelBuilder.Build(cube.FrontFace);

        var cubeViewModel = new RubiksCubeControlViewModel
        {
            UpFaceViewModel = upFaceViewModel,
            RightFaceViewModel = rightFaceViewModel,
            FrontFaceViewModel = frontFaceViewModel,
        };

        return cubeViewModel;
    }
}