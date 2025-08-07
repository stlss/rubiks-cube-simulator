using CommunityToolkit.Mvvm.ComponentModel;
using RubiksCubeSimulator.Domain.Services;
using RubiksCubeSimulator.Wpf.App.Infrastructure.Providers;
using RubiksCubeSimulator.Wpf.App.Infrastructure.ViewModelBuilders.RubiksCubeControls;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.App.ViewModels.Windows;

internal sealed class MainWindowViewModel : ObservableObject
{
    private readonly IRubiksCubeBuilder _cubeBuilder;
    private readonly IRubiksCubeControlViewModelBuilder _cubeControlViewModelBuilder;

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
        _cubeBuilder = serviceProvider.GetRubiksCubeBuilder();
        _cubeControlViewModelBuilder = serviceProvider.GetRubiksCubeControlViewModelBuilder();

        SetCubeControlViewModel();
    }

    private void SetCubeControlViewModel()
    {
        var cube = _cubeBuilder.BuildSolvedRubiksCube(3);
        CubeControlViewModel = _cubeControlViewModelBuilder.Build(cube);
    }
}