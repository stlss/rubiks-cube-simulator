using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.App.Infrastructure.RubiksCubeControlViewModelBuilders;

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
        var upFaceViewModel = faceViewModelBuilder.Build(cube.Dimension, cube.UpFace);
        var rightFaceViewModel = faceViewModelBuilder.Build(cube.Dimension, cube.RightFace);
        var leftFaceViewModel = faceViewModelBuilder.Build(cube.Dimension, cube.FrontFace);

        var cubeViewModel = new RubiksCubeControlViewModel
        {
            CubeDimension = cube.Dimension,
            UpFaceViewModel = upFaceViewModel,
            RightFaceViewModel = rightFaceViewModel,
            LeftFaceViewModel = leftFaceViewModel,
        };

        return cubeViewModel;
    }
}