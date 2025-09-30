using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves;

namespace RubiksCubeSimulator.Application.RubiksCubeMover.Checkers;

internal interface IRubiksCubeMoveChecker
{
    public bool Check(SliceMove move, int cubeDimension);

    public bool Check(WholeMove move);
}

internal sealed class RubiksCubeMoveChecker : IRubiksCubeMoveChecker
{
    public bool Check(SliceMove move, int cubeDimension)
    {
        var isCorrectFace = Enum.IsDefined(move.FaceName);
        var isCorrectDirection = Enum.IsDefined(move.Direction);
        var isCorrectSlice = 0 <= move.Slice && move.Slice < cubeDimension;

        return isCorrectFace && isCorrectDirection && isCorrectSlice;
    }

    public bool Check(WholeMove move)
    {
        var isCorrectAxis = Enum.IsDefined(move.AxisName);
        var isCorrectDirection = Enum.IsDefined(move.Direction);

        return isCorrectAxis && isCorrectDirection;
    }
}