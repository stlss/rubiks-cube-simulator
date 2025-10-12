using RubiksCubeSimulator.Domain.Services;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves;
using RubiksCubeSimulator.Wpf.Infrastructure.MoveServices;
using RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext.Managers;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext;

public interface IRubiksCubeContext
{
    public RubiksCubeViewModel MainCubeViewModel { get; }

    public RubiksCubeViewModel SubCubeViewModel { get; }

    public void Move(MoveBase move);

    public void Recover();

    public void ShowMoveArrows(MoveBase move);

    public void ClearMoveArrows();
}

internal sealed class RubiksCubeContext(
    RubiksCube cube,
    IRubiksCubeManager cubeManager,
    IRubiksCubeMover cubeMover,
    IRubiksCubeBuilder cubeBuilder,
    ICubeMoveSetter cubeMoveSetter) : IRubiksCubeContext
{
    private RubiksCube _cube = cube;

    public RubiksCubeViewModel MainCubeViewModel { get; } = cubeManager.Create(cube);

    public RubiksCubeViewModel SubCubeViewModel { get; } = cubeManager.Create(cube);

    public void Move(MoveBase move)
    {
        _cube = cubeMover.Move(_cube, move);
        cubeManager.Update(MainCubeViewModel, _cube);
        cubeManager.Update(SubCubeViewModel, _cube);
    }

    public void Recover()
    {
        _cube = cubeBuilder.Build(_cube.Dimension);
        cubeManager.Update(MainCubeViewModel, _cube);
        cubeManager.Update(SubCubeViewModel, _cube);
    }

    public void ShowMoveArrows(MoveBase move)
    {
        cubeMoveSetter.ShowMoveArrows(MainCubeViewModel, move);
    }

    public void ClearMoveArrows()
    {
        cubeMoveSetter.ClearMoveArrows(MainCubeViewModel);
    }
}