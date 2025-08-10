namespace RubiksCubeSimulator.Application.RubiksCubeMover.MutableRubiksCube;

public sealed record MutableRubiksCube(
    int Dimension,
    MutableRubiksCubeFace UpFace,
    MutableRubiksCubeFace RightFace,
    MutableRubiksCubeFace FrontFace,
    MutableRubiksCubeFace DownFace,
    MutableRubiksCubeFace LeftFace,
    MutableRubiksCubeFace BackFace);