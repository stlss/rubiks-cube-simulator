using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;
using RubiksCubeSimulator.Wpf.Events.EventArgs.Enums;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext.Mappers;

internal interface IRubiksCubeMapper
{
    public RubiksCubeControlViewModel Map(RubiksCube cube);

    public void Map(RubiksCube cube, RubiksCubeControlViewModel cubeViewModel);
}

internal sealed class RubiksCubeMapper(
    IRubiksCubeFaceMapper faceViewModelMapper)
    : IRubiksCubeMapper
{
    public RubiksCubeControlViewModel Map(RubiksCube cube)
    {
        var upFaceViewModel = faceViewModelMapper.Map(FaceName.Up, cube.UpFace);
        var rightFaceViewModel = faceViewModelMapper.Map(FaceName.Right, cube.RightFace);
        var leftFaceViewModel = faceViewModelMapper.Map(FaceName.Left, cube.FrontFace);

        var cubeViewModel = new RubiksCubeControlViewModel
        {
            CubeDimension = cube.Dimension,
            UpFaceViewModel = upFaceViewModel,
            RightFaceViewModel = rightFaceViewModel,
            LeftFaceViewModel = leftFaceViewModel,
        };

        return cubeViewModel;
    }

    public void Map(RubiksCube cube, RubiksCubeControlViewModel cubeViewModel)
    {
        var faces = new List<RubiksCubeFace>
        {
            cube.UpFace, cube.RightFace, cube.FrontFace,
        };

        var faceViewModels = new List<RubiksCubeFaceControlViewModel>
        {
            cubeViewModel.UpFaceViewModel, cubeViewModel.RightFaceViewModel, cubeViewModel.LeftFaceViewModel,
        };

        var facesWithViewModels = faces.Zip(faceViewModels,
            (face, faceViewModel) => (Face: face, FaceViewModel: faceViewModel));

        foreach (var (face, faceViewModel) in facesWithViewModels)
        {
            faceViewModelMapper.Map(face, faceViewModel);
        }
    }
}