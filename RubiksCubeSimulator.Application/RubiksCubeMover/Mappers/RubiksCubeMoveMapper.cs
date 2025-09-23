using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves.Enums;

namespace RubiksCubeSimulator.Application.RubiksCubeMover.Mappers;

internal interface IRubiksCubeMoveMapper
{
    public IReadOnlyList<RubiksCubeSliceMove> Map(WholeRubiksCubeMove move, int cubeDimension);
}

internal sealed class RubiksCubeMoveMapper : IRubiksCubeMoveMapper
{
    private static readonly Dictionary<AxisName, MoveFace> FaceMoveByAxisName = new()
    {
        [AxisName.X] = MoveFace.Right,
        [AxisName.Y] = MoveFace.Up,
        [AxisName.Z] = MoveFace.Front
    };

    public IReadOnlyList<RubiksCubeSliceMove> Map(WholeRubiksCubeMove move, int cubeDimension)
    {
        var sliceMoves = new List<RubiksCubeSliceMove>(cubeDimension);

        var faceMove = FaceMoveByAxisName[move.Axis];
        var directionMove = move.Direction;

        for (var sliceNumber = 0; sliceNumber < cubeDimension; sliceNumber++)
        {
            sliceMoves.Add(new RubiksCubeSliceMove(faceMove, directionMove, sliceNumber));
        }

        return sliceMoves;
    }
}