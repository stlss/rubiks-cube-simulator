using CommunityToolkit.Mvvm.ComponentModel;
using RubiksCubeSimulator.Domain.Services;
using RubiksCubeSimulator.Wpf.App.Infrastructure.Providers;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.App.ViewModels.Windows;

internal sealed class MainWindowViewModel : ObservableObject
{
    private readonly IRubiksCubeBuilder _cubeBuilder;

    private RubiksCubeControlViewModel _cubeControlViewModel = new();

    public MainWindowViewModel() : this(new RubiksCubeServiceProvider())
    {
    }

    private MainWindowViewModel(IRubiksCubeServiceProvider serviceProvider)
    {
        _cubeBuilder = serviceProvider.GetRubiksCubeBuilder();
    }

    public RubiksCubeControlViewModel CubeControlViewModel
    {
        get => _cubeControlViewModel;
        set => SetProperty(ref _cubeControlViewModel, value);
    }
}