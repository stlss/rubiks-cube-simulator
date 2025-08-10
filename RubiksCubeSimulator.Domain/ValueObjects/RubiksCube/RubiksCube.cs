namespace RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;

public sealed record RubiksCube(
    int Dimension,
    RubiksCubeFace UpFace,
    RubiksCubeFace RightFace,
    RubiksCubeFace FrontFace,
    RubiksCubeFace DownFace,
    RubiksCubeFace LeftFace,
    RubiksCubeFace BackFace);