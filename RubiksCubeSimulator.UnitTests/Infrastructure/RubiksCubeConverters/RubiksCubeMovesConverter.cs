using System.ComponentModel;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves.Enums;

namespace RubiksCubeSimulator.UnitTests.Infrastructure.RubiksCubeConverters;

internal static class RubiksCubeMovesConverter
{
    public static string RubiksCubeMovesToString(IEnumerable<MoveBase> moves)
    {
        return $"[{string.Join(", ", moves.Select(RubiksCubeMoveToString))}]";
    }

    private static string RubiksCubeMoveToString(MoveBase move)
    {
        var result = move switch
        {
            WholeMove wholeCubeMove => WholeRubiksCubeMoveToString(wholeCubeMove),
            SliceMove sliceMove => RubiksCubeSliceMoveToString(sliceMove),
            _ => throw new NotSupportedException(),
        };

        return result;
    }

    private static string WholeRubiksCubeMoveToString(WholeMove move)
    {
        var result = move.Axis switch
        {
            AxisName.X => "X",
            AxisName.Y => "Y",
            AxisName.Z => "Z",
            _ => throw new InvalidEnumArgumentException(nameof(move.Axis), (int)move.Axis, typeof(AxisName)),
        };

        if (move.Direction == MoveDirection.Counterclockwise) result += "'";
        return result;
    }

    private static string RubiksCubeSliceMoveToString(SliceMove move)
    {
        var result = move.Face switch
        {
            MoveFace.Up => "U",
            MoveFace.Right => "R",
            MoveFace.Front => "F",
            MoveFace.Down => "D",
            MoveFace.Left => "L",
            MoveFace.Back => "B",
            _ => throw new InvalidEnumArgumentException(nameof(move.Face), (int)move.Face, typeof(MoveFace)),
        };

        if (move.Direction == MoveDirection.Counterclockwise) result += "'";
        return result;
    }
}