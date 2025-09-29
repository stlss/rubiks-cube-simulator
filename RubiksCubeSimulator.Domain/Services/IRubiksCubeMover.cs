using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves;

namespace RubiksCubeSimulator.Domain.Services;

public interface IRubiksCubeMover
{
    public RubiksCube MoveRubiksCube(RubiksCube cube, MoveBase move);

    public RubiksCube MoveRubiksCube(RubiksCube cube, IEnumerable<MoveBase> move);
}