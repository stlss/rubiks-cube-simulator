namespace RubiksCubeSimulator.Application.RubiksCubeMover.MutableRubiksCube;

internal sealed record MutableRubiksCube(
    int Dimension,
    MutableRubiksCubeFace UpFace,
    MutableRubiksCubeFace RightFace,
    MutableRubiksCubeFace FrontFace,
    MutableRubiksCubeFace DownFace,
    MutableRubiksCubeFace LeftFace,
    MutableRubiksCubeFace BackFace);