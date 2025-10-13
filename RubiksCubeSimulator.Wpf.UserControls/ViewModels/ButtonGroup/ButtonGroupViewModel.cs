using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RubiksCubeSimulator.Wpf.UserControls.ViewModels.ButtonGroup;

public sealed class ButtonGroupViewModel : ObservableObject
{
    public IRelayCommand? ResetSelectedCubeCommand { get; init; }

    public IRelayCommand? ShuffleSelectedCubeCommand { get; init; }

    public IRelayCommand? ResetAllCubesCommand { get; init; }

    public IRelayCommand? ShuffleAllCubesCommand { get; init; }
}