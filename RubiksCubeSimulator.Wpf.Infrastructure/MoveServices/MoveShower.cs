using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.Infrastructure.MoveServices;

internal interface IMoveShower
{
    public void ShowMoveArrows(RubiksCubeControlViewModel cubeViewModel, MoveBase move);

    public void ClearMoveArrows(RubiksCubeControlViewModel cubeViewModel);
}

internal sealed class MoveShower : IMoveShower
{
    public void ShowMoveArrows(RubiksCubeControlViewModel cubeViewModel, MoveBase move)
    {
        throw new NotImplementedException();
    }

    public void ClearMoveArrows(RubiksCubeControlViewModel cubeViewModel)
    {
        throw new NotImplementedException();
    }
}