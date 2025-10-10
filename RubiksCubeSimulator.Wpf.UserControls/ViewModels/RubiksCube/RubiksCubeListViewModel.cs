using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

public sealed class RubiksCubeListViewModel : ObservableObject
{
    public ObservableCollection<RubiksCubeViewModel> CubeViewModels { get; init; } =
        new(Enumerable.Range(2, 4).Select(_ => new RubiksCubeViewModel()).ToList());

    private RubiksCubeViewModel? _selectedCubeViewModel;

    public RubiksCubeViewModel? SelectedCubeViewModel
    {
        get => _selectedCubeViewModel;
        set => SetProperty(ref _selectedCubeViewModel, value);
    }
}