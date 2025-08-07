using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.App.Infrastructure.ViewModelBuilders.RubiksCubeControls;

internal interface IRubiksCubeControlViewModelBuilder
{
    public RubiksCubeControlViewModel Build(RubiksCube cube);
}

internal sealed class RubiksCubeControlViewModelBuilder : IRubiksCubeControlViewModelBuilder
{
    public RubiksCubeControlViewModel Build(RubiksCube cube)
    {
        throw new NotImplementedException();
    }
}