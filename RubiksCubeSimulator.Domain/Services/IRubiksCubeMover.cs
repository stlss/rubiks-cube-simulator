using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves;

namespace RubiksCubeSimulator.Domain.Services;

public interface IRubiksCubeMover
{
    public RubiksCube MoveRubiksCube(RubiksCube cube, RubiksCubeMoveBase move);

    public RubiksCube MoveRubiksCube(RubiksCube cube, IEnumerable<RubiksCubeMoveBase> move);
}