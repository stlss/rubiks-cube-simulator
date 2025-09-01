using RubiksCubeSimulator.Domain.Services;
using RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext.Mappers;

namespace RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext;

public interface IRubiksCubeContextBuilder
{
    public IRubiksCubeContext Build(int cubeDimension);
}

internal sealed class RubiksCubeContextBuilder(
    IRubiksCubeBuilder cubeBuilder,
    IRubiksCubeMapper cubeMapper,
    IRubiksCubeMover cubeMover) : IRubiksCubeContextBuilder
{
    public IRubiksCubeContext Build(int cubeDimension)
    {
        var domainCube = cubeBuilder.BuildSolvedRubiksCube(cubeDimension);
        return new RubiksCubeContext(domainCube, cubeMapper, cubeMover);
    }
}