namespace RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

public sealed class RubiksCubeViewModel
{
    public int CubeDimension { get; init; } = 3;

    public RubiksCubeFaceViewModel UpFaceViewModel { get; init; } = new();

    public RubiksCubeFaceViewModel RightFaceViewModel { get; init; } = new();

    public RubiksCubeFaceViewModel LeftFaceViewModel { get; init; } = new();
}