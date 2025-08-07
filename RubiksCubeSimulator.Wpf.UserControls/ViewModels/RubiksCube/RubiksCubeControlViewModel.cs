namespace RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

public sealed class RubiksCubeControlViewModel
{
    public RubiksCubeFaceControlViewModel UpFaceControlViewModel { get; init; } = new();

    public RubiksCubeFaceControlViewModel RightFaceControlViewModel { get; init; } = new();

    public RubiksCubeFaceControlViewModel FrontFaceControlViewModel { get; init; } = new();
}