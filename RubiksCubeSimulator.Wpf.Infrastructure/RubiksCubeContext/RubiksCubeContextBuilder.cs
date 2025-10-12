using RubiksCubeSimulator.Domain.Services;
using RubiksCubeSimulator.Wpf.Infrastructure.MoveServices;
using RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext.Managers;

namespace RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext;

public interface IRubiksCubeContextBuilder
{
    public IRubiksCubeContext Build(int cubeDimension);
}

internal sealed class RubiksCubeContextBuilder(
    IRubiksCubeBuilder cubeBuilder,
    IRubiksCubeManager cubeManager,
    IRubiksCubeMover cubeMover,
    ICubeMoveSetter cubeMoveSetter) : IRubiksCubeContextBuilder
{
    public IRubiksCubeContext Build(int cubeDimension)
    {
        var cube = cubeBuilder.Build(cubeDimension);
        return new RubiksCubeContext(cube, cubeManager, cubeMover, cubeBuilder, cubeMoveSetter);
    }
}