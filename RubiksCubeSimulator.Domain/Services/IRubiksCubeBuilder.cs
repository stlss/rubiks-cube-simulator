using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;

namespace RubiksCubeSimulator.Domain.Services;

public interface IRubiksCubeBuilder
{
    public RubiksCube Build(int cubeDimension);
}