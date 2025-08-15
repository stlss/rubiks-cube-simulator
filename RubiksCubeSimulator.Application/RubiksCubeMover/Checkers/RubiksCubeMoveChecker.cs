using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves;

namespace RubiksCubeSimulator.Application.RubiksCubeMover.Checkers;

public interface IRubiksCubeMoveChecker
{
    public bool IsCorrectRubiksCubeSliceMove(RubiksCubeSliceMove move, int cubeDimension);

    public bool IsCorrectWholeRubiksCubeMove(WholeRubiksCubeMove move);
}

internal sealed class RubiksCubeMoveChecker : IRubiksCubeMoveChecker
{
    public bool IsCorrectRubiksCubeSliceMove(RubiksCubeSliceMove move, int cubeDimension)
    {
        var isCorrectFace = Enum.IsDefined(move.Face);
        var isCorrectDirection = Enum.IsDefined(move.Direction);
        var isCorrectSlice = 0 <= move.Slice && move.Slice < cubeDimension;

        return isCorrectFace && isCorrectDirection && isCorrectSlice;
    }

    public bool IsCorrectWholeRubiksCubeMove(WholeRubiksCubeMove move)
    {
        var isCorrectAxis = Enum.IsDefined(move.Axis);
        var isCorrectDirection = Enum.IsDefined(move.Direction);

        return isCorrectAxis && isCorrectDirection;
    }
}