namespace RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

public sealed class RubiksCubeControlViewModel
{
    public RubiksCubeFaceControlViewModel UpFaceViewModel { get; init; } = new();

    public RubiksCubeFaceControlViewModel RightFaceViewModel { get; init; } = new();

    public RubiksCubeFaceControlViewModel LeftFaceViewModel { get; init; } = new();
}