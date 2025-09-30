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
        var result = move.AxisName switch
        {
            AxisName.X => "X",
            AxisName.Y => "Y",
            AxisName.Z => "Z",
            _ => throw new InvalidEnumArgumentException(nameof(move.AxisName), (int)move.AxisName, typeof(AxisName)),
        };

        if (move.Direction == MoveDirection.Counterclockwise) result += "'";
        return result;
    }

    private static string RubiksCubeSliceMoveToString(SliceMove move)
    {
        var result = move.FaceName switch
        {
            FaceName.Up => "U",
            FaceName.Right => "R",
            FaceName.Front => "F",
            FaceName.Down => "D",
            FaceName.Left => "L",
            FaceName.Back => "B",
            _ => throw new InvalidEnumArgumentException(nameof(move.FaceName), (int)move.FaceName, typeof(FaceName)),
        };

        if (move.Direction == MoveDirection.Counterclockwise) result += "'";
        return result;
    }
}