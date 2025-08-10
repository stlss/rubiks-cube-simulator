using RubiksCubeSimulator.Domain.Services;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves;

namespace RubiksCubeSimulator.Application.RubiksCubeMover;

public sealed class RubiksCubeMover : IRubiksCubeMover
{
    public RubiksCube MoveRubiksCube(RubiksCube cube, RubiksCubeMoveBase move)
    {
        throw new NotImplementedException();
    }

    public RubiksCube MoveRubiksCube(RubiksCube cube, IEnumerable<RubiksCubeMoveBase> move)
    {
        throw new NotImplementedException();
    }
}