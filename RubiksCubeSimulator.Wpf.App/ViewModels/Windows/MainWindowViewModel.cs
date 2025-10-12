using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RubiksCubeSimulator.Wpf.App.Infrastructure;
using RubiksCubeSimulator.Wpf.Events;
using RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.ButtonGroup;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.App.ViewModels.Windows;

internal sealed class MainWindowViewModel : ObservableObject
{
    private readonly IRubiksCubeServiceProvider _serviceProvider = new RubiksCubeServiceProvider();

    private readonly Dictionary<RubiksCubeViewModel, IRubiksCubeContext> _mapSubCubeViewModelToContext;
    private readonly Dictionary<IRubiksCubeContext, ISubscriber<KeyEventArgs>> _mapCubeContextToKeyEventSubscriber;

    private IRubiksCubeContext _selectedCubeContext;
    private readonly HashSet<Key> _pressedKeys = [];


    private IRubiksCubeContext SelectedCubeContext
    {
        get => _selectedCubeContext;
        set
        {
            _selectedCubeContext = value;
            OnPropertyChanged(nameof(SelectedMainCubeViewModel));
        }
    }

    public RubiksCubeListViewModel CubeListViewModel { get; }

    public ButtonGroupViewModel ButtonGroupViewModel { get; }

    public RubiksCubeViewModel SelectedMainCubeViewModel => _selectedCubeContext.MainCubeViewModel;


    public bool IsCubeListControlEnabled => _pressedKeys.Count == 0;

    public bool IsButtonGroupControlEnabled => _pressedKeys.Count == 0;


    public IRelayCommand<KeyEventArgs> KeyDownCommand { get; }

    public IRelayCommand<KeyEventArgs> KeyUpCommand { get; }


    public MainWindowViewModel()
    {
        var cubeContexts = CreateCubeContexts(6);
        _selectedCubeContext = cubeContexts[1];

        _mapSubCubeViewModelToContext = CreateMapSubCubeViewModelToContext(cubeContexts);
        _mapCubeContextToKeyEventSubscriber = CreateMapCubeContextToKeyEventSubscriber(cubeContexts);

        CubeListViewModel = CreateCubeListViewModel(cubeContexts, SelectedCubeContext);
        CubeListViewModel.PropertyChanged += OnSelectedCubeViewModelPropertyChanged;

        ButtonGroupViewModel = CreateButtonGroupViewModel();

        KeyDownCommand = new RelayCommand<KeyEventArgs>(keyEventArgs => HandleKeyEventArgs(keyEventArgs!));
        KeyUpCommand = new RelayCommand<KeyEventArgs>(keyEventArgs => HandleKeyEventArgs(keyEventArgs!));
    }


    private List<IRubiksCubeContext> CreateCubeContexts(int cubeContextCount)
    {
        return Enumerable.Range(2, cubeContextCount)
            .Select(_serviceProvider.RubiksCubeContextBuilder.Build)
            .ToList();
    }

    private static Dictionary<RubiksCubeViewModel, IRubiksCubeContext> CreateMapSubCubeViewModelToContext(
        IEnumerable<IRubiksCubeContext> cubeContexts)
    {
        return cubeContexts
            .Select(cubeContext => (cubeContext.SubCubeViewModel, cubeContext))
            .ToDictionary();
    }

    private Dictionary<IRubiksCubeContext, ISubscriber<KeyEventArgs>> CreateMapCubeContextToKeyEventSubscriber(
        IEnumerable<IRubiksCubeContext> cubeContexts)
    {
        return cubeContexts
            .Select(cubeContext => (cubeContext, _serviceProvider.KeyEventSubscriberBuilder.Build(cubeContext)))
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

    private static ButtonGroupViewModel CreateButtonGroupViewModel()
    {
        return new ButtonGroupViewModel();
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

        SelectedCubeContext = cubeContext;
    }


    private void HandleKeyEventArgs(KeyEventArgs keyEventArgs)
    {
        NotifyKeyEventSubscriber(keyEventArgs);
        ChangePressedKeys(keyEventArgs);
    }

    private void NotifyKeyEventSubscriber(KeyEventArgs keyEventArgs)
    {
        var keyEventSubscriber = _mapCubeContextToKeyEventSubscriber[SelectedCubeContext];
        keyEventSubscriber.OnEvent(this, keyEventArgs);
    }

    private void ChangePressedKeys(KeyEventArgs keyEventArgs)
    {
        if (keyEventArgs.IsDown) _pressedKeys.Add(keyEventArgs.Key);
        else if (keyEventArgs.IsUp) _pressedKeys.Remove(keyEventArgs.Key);

        OnPropertyChanged(nameof(IsCubeListControlEnabled));
        OnPropertyChanged(nameof(IsButtonGroupControlEnabled));
    }
}