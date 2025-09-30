using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;
using RubiksCubeSimulator.Wpf.Events.EventArgs.Enums;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext.Managers;

internal interface IRubiksCubeManager
{
    public RubiksCubeViewModel Create(RubiksCube cube);

    public void Update(RubiksCubeViewModel cubeViewModel, RubiksCube cube);
}

internal sealed class RubiksCubeManager(
    IRubiksCubeFaceManager faceViewModelManager)
    : IRubiksCubeManager
{
    public RubiksCubeViewModel Create(RubiksCube cube)
    {
        var upFaceViewModel = faceViewModelManager.Create(FaceName.Up, cube.UpFace);
        var rightFaceViewModel = faceViewModelManager.Create(FaceName.Right, cube.RightFace);
        var leftFaceViewModel = faceViewModelManager.Create(FaceName.Left, cube.FrontFace);

        var cubeViewModel = new RubiksCubeViewModel
        {
            CubeDimension = cube.Dimension,
            UpFaceViewModel = upFaceViewModel,
            RightFaceViewModel = rightFaceViewModel,
            LeftFaceViewModel = leftFaceViewModel,
        };

        return cubeViewModel;
    }

    public void Update(RubiksCubeViewModel cubeViewModel, RubiksCube cube)
    {
        var faceViewModels = new List<RubiksCubeFaceViewModel>
        {
            cubeViewModel.UpFaceViewModel, cubeViewModel.RightFaceViewModel, cubeViewModel.LeftFaceViewModel,
        };

        var faces = new List<RubiksCubeFace>
        {
            cube.UpFace, cube.RightFace, cube.FrontFace,
        };

        var facesWithViewModels = faceViewModels.Zip(faces,
            (faceViewModel, face) => (FaceViewModel: faceViewModel, Face: face));

        foreach (var (faceViewModel, face) in facesWithViewModels)
        {
            faceViewModelManager.Update(faceViewModel, face);
        }
    }
}