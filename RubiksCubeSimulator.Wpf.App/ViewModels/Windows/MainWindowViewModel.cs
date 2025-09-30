using RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.App.ViewModels.Windows;

internal sealed class MainWindowViewModel : MainWindowViewModelBase
{
    private readonly IRubiksCubeContext _cubeContext;

    public RubiksCubeControlViewModel CubeViewModel => _cubeContext.CubeViewModel;

    public MainWindowViewModel()
    {
        _cubeContext = ServiceProvider.RubiksCubeContextBuilder.Build(3);
        ServiceProvider.RubiksCubeContextLinker.Link(_cubeContext, this);
    }
}