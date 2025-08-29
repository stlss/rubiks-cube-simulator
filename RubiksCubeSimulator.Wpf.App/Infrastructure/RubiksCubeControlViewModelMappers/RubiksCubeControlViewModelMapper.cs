using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.App.Infrastructure.RubiksCubeControlViewModelMappers;

internal interface IRubiksCubeControlViewModelMapper
{
    public RubiksCubeControlViewModel Map(RubiksCube cube);
}

internal sealed class RubiksCubeControlViewModelMapper(
    IRubiksCubeFaceControlViewModelMapper faceViewModelMapper)
    : IRubiksCubeControlViewModelMapper
{
    public RubiksCubeControlViewModel Map(RubiksCube cube)
    {
        var upFaceViewModel = faceViewModelMapper.Map(cube.Dimension, cube.UpFace);
        var rightFaceViewModel = faceViewModelMapper.Map(cube.Dimension, cube.RightFace);
        var leftFaceViewModel = faceViewModelMapper.Map(cube.Dimension, cube.FrontFace);

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