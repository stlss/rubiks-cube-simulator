using CommunityToolkit.Mvvm.ComponentModel;
using RubiksCubeSimulator.Domain.Services;
using RubiksCubeSimulator.Wpf.App.Infrastructure.RubiksCubeControlViewModelBuilders;
using RubiksCubeSimulator.Wpf.App.Infrastructure.RubiksCubeServiceProvider;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.App.ViewModels.Windows;

internal sealed class MainWindowViewModel : ObservableObject
{
    private readonly IRubiksCubeBuilder _cubeBuilder;
    private readonly IRubiksCubeControlViewModelBuilder _cubeViewModelBuilder;

    private RubiksCubeControlViewModel _cubeControlViewModel = new();

    public RubiksCubeControlViewModel CubeControlViewModel
    {
        get => _cubeControlViewModel;
        private set => SetProperty(ref _cubeControlViewModel, value);
    }

    public MainWindowViewModel() : this(new RubiksCubeServiceProvider())
    {
    }

    private MainWindowViewModel(IRubiksCubeServiceProvider serviceProvider)
    {
        _cubeBuilder = serviceProvider.GetCubeBuilder();
        _cubeViewModelBuilder = serviceProvider.GetCubeViewModelBuilder();

        SetCubeViewModel();
    }

    private void SetCubeViewModel()
    {
        var cube = _cubeBuilder.BuildSolvedRubiksCube(3);
        CubeControlViewModel = _cubeViewModelBuilder.Build(cube);
    }
}