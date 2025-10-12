using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RubiksCubeSimulator.Wpf.UserControls.ViewModels.ButtonGroup;

public sealed class ButtonGroupViewModel : ObservableObject
{
    public IRelayCommand? RecoverSelectedCubeCommand { get; init; }

    public IRelayCommand? ShuffleSelectedCubeCommand { get; init; }

    public IRelayCommand? RecoverAllCubesCommand { get; init; }

    public IRelayCommand? ShuffleAllCubesCommand { get; init; }
}