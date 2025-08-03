using CommunityToolkit.Mvvm.ComponentModel;
using RubiksCubeSimulator.Domain.Services;
using RubiksCubeSimulator.WpfApp.Infrastructure.Providers;

namespace RubiksCubeSimulator.WpfApp.ViewModels.Windows;

internal sealed class MainWindowViewModel : ObservableObject
{
    private readonly IRubiksCubeBuilder _cubeBuilder;

    public MainWindowViewModel() : this(new RubiksCubeServiceProvider())
    {
    }

    private MainWindowViewModel(IRubiksCubeServiceProvider serviceProvider)
    {
        _cubeBuilder = serviceProvider.GetRubiksCubeBuilder();
    }
}