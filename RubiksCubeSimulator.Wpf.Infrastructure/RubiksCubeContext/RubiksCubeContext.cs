using RubiksCubeSimulator.Domain.Services;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves;
using RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext.Managers;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext;

public interface IRubiksCubeContext
{
    public RubiksCubeControlViewModel CubeViewModel { get; }

    public void Move(RubiksCubeMoveBase moveBase);
}

internal sealed class RubiksCubeContext(
    RubiksCube cube,
    IRubiksCubeManager cubeManager,
    IRubiksCubeMover cubeMover) : IRubiksCubeContext
{
    private RubiksCube _cube = cube;

    public RubiksCubeControlViewModel CubeViewModel { get; } = cubeManager.Create(cube);

    public void Move(RubiksCubeMoveBase moveBase)
    {
        _cube = cubeMover.MoveRubiksCube(_cube, moveBase);
        cubeManager.Update(CubeViewModel, _cube);
    }
}