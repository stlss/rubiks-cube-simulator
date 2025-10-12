using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves;

namespace RubiksCubeSimulator.Domain.Services;

public interface IMoveGenerator
{
    public IReadOnlyList<MoveBase> Generate(int cubeDimension);
}