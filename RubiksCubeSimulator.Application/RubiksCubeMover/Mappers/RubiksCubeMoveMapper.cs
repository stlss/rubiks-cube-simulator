using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves.Enums;

namespace RubiksCubeSimulator.Application.RubiksCubeMover.Mappers;

internal interface IRubiksCubeMoveMapper
{
    public IReadOnlyList<SliceMove> Map(WholeMove move, int cubeDimension);
}

internal sealed class RubiksCubeMoveMapper : IRubiksCubeMoveMapper
{
    private static readonly Dictionary<AxisName, FaceName> FaceMoveByAxisName = new()
    {
        [AxisName.X] = FaceName.Right,
        [AxisName.Y] = FaceName.Up,
        [AxisName.Z] = FaceName.Front
    };

    public IReadOnlyList<SliceMove> Map(WholeMove move, int cubeDimension)
    {
        var sliceMoves = new List<SliceMove>(cubeDimension);

        var faceMove = FaceMoveByAxisName[move.AxisName];
        var directionMove = move.Direction;

        for (var sliceNumber = 0; sliceNumber < cubeDimension; sliceNumber++)
        {
            sliceMoves.Add(new SliceMove(faceMove, directionMove, sliceNumber));
        }

        return sliceMoves;
    }
}