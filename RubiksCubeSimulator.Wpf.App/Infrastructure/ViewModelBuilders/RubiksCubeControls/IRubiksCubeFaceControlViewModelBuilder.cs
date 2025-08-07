using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.App.Infrastructure.ViewModelBuilders.RubiksCubeControls;

internal interface IRubiksCubeFaceControlViewModelBuilder
{
    public RubiksCubeFaceControlViewModel Build(RubiksCubeFace cubeFace);
}

internal sealed class RubiksCubeFaceControlViewModelBuilder : IRubiksCubeFaceControlViewModelBuilder
{
    public RubiksCubeFaceControlViewModel Build(RubiksCubeFace cubeFace)
    {
        throw new NotImplementedException();
    }
}