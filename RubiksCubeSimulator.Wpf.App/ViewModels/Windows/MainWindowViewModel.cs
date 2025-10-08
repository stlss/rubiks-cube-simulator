using System.Collections.ObjectModel;
using System.ComponentModel;
using RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.App.ViewModels.Windows;

internal sealed class MainWindowViewModel : MainWindowViewModelBase
{
    private readonly Dictionary<RubiksCubeViewModel, IRubiksCubeContext> _mapSubCubeViewModelToContext;

    public RubiksCubeListViewModel CubeListViewModel { get; }

    private RubiksCubeViewModel _selectedMainCubeViewModel;

    public RubiksCubeViewModel SelectedMainCubeViewModel
    {
        get => _selectedMainCubeViewModel;
        private set => SetProperty(ref _selectedMainCubeViewModel, value);
    }

    public MainWindowViewModel()
    {
        var cubeContexts = CreateCubeContexts(6);
        LinkCubeContextsWithWindow(cubeContexts);

        var selectedCubeContext = cubeContexts[1];

        _mapSubCubeViewModelToContext = CreateMapSubCubeViewModelToContext(cubeContexts);
        CubeListViewModel = CreateCubeListViewModel(cubeContexts, selectedCubeContext);
        _selectedMainCubeViewModel = selectedCubeContext.MainCubeViewModel;

        CubeListViewModel.PropertyChanged += OnSelectedCubeViewModelPropertyChanged;
    }

    private List<IRubiksCubeContext> CreateCubeContexts(int cubeContextCount)
    {
        return Enumerable.Range(2, cubeContextCount)
            .Select(ServiceProvider.RubiksCubeContextBuilder.Build)
            .ToList();
    }

    private void LinkCubeContextsWithWindow(IEnumerable<IRubiksCubeContext> cubeContexts)
    {
        foreach (var cubeContext in cubeContexts)
        {
            ServiceProvider.RubiksCubeContextLinker.Link(cubeContext, this);
        }
    }

    private static Dictionary<RubiksCubeViewModel, IRubiksCubeContext> CreateMapSubCubeViewModelToContext(
        IEnumerable<IRubiksCubeContext> cubeContexts)
    {
        return cubeContexts
            .Select(cubeContext => (cubeContext.SubCubeViewModel, cubeContext))
            .ToDictionary();
    }

    private static RubiksCubeListViewModel CreateCubeListViewModel(
        IEnumerable<IRubiksCubeContext> cubeContexts,
        IRubiksCubeContext selectedCubeContext)
    {
        var subCubeViewModels = cubeContexts.Select(cubeContext => cubeContext.SubCubeViewModel);

        return new RubiksCubeListViewModel
        {
            CubeViewModels = new ObservableCollection<RubiksCubeViewModel>(subCubeViewModels),
            SelectedCubeViewModel = selectedCubeContext.SubCubeViewModel,
        };
    }

    private void OnSelectedCubeViewModelPropertyChanged(
        object? sender,
        PropertyChangedEventArgs propertyChangedEventArgs)
    {
        const string propertyName = nameof(RubiksCubeListViewModel.SelectedCubeViewModel);

        if (propertyChangedEventArgs.PropertyName != propertyName)
            return;

        var subCubeViewModel = ((RubiksCubeListViewModel)sender!).SelectedCubeViewModel!;
        var cubeContext = _mapSubCubeViewModelToContext[subCubeViewModel];

        SelectedMainCubeViewModel = cubeContext.MainCubeViewModel;
    }
}