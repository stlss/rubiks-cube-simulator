using RubiksCubeSimulator.Domain.Services;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves;
using RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext.Mappers;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext;

public interface IRubiksCubeContext
{
    public RubiksCubeControlViewModel CubeViewModel { get; }

    public void Move(RubiksCubeMoveBase moveBase);
}

internal sealed class RubiksCubeContext(
    RubiksCube domainCube,
    IRubiksCubeMapper cubeMapper,
    IRubiksCubeMover cubeMover) : IRubiksCubeContext
{
    private RubiksCube _domainCube = domainCube;

    public RubiksCubeControlViewModel CubeViewModel { get; private set; } = cubeMapper.Map(domainCube);

    public void Move(RubiksCubeMoveBase moveBase)
    {
        _domainCube = cubeMover.MoveRubiksCube(_domainCube, moveBase);
        CubeViewModel = cubeMapper.Map(_domainCube);
    }
}