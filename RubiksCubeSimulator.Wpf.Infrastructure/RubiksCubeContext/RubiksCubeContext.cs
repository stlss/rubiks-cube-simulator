using RubiksCubeSimulator.Domain.Services;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves;
using RubiksCubeSimulator.Wpf.Infrastructure.MoveServices;
using RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext.Managers;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext;

public interface IRubiksCubeContext
{
    public RubiksCubeControlViewModel CubeViewModel { get; }

    public void Move(MoveBase moveBase);

    public void ShowMoveArrows(MoveBase move);

    public void ClearMoveArrows();
}

internal sealed class RubiksCubeContext(
    RubiksCube cube,
    IRubiksCubeManager cubeManager,
    IRubiksCubeMover cubeMover,
    IMoveShower moveShower) : IRubiksCubeContext
{
    private RubiksCube _cube = cube;

    public RubiksCubeControlViewModel CubeViewModel { get; } = cubeManager.Create(cube);

    public void Move(MoveBase moveBase)
    {
        _cube = cubeMover.Move(_cube, moveBase);
        cubeManager.Update(CubeViewModel, _cube);
    }

    public void ShowMoveArrows(MoveBase move)
    {
        moveShower.ShowMoveArrows(CubeViewModel, move);
    }

    public void ClearMoveArrows()
    {
        moveShower.ClearMoveArrows(CubeViewModel);
    }
}