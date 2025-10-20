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
    private readonly Dictionary<IRubiksCubeContext, ISubscriber<KeyEventArgs>> _mapCubeContextToKeySubscriber;

    private IRubiksCubeContext _selectedCubeContext;
    private readonly HashSet<Key> _pressedKeys = [];

    private bool _isHandledKey = true;
    private bool _isShuffling;


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


    public bool IsCubeListControlEnabled => _pressedKeys.Count == 0 && !_isShuffling;

    public bool IsButtonGroupControlEnabled => _pressedKeys.Count == 0;


    public IRelayCommand<KeyEventArgs> KeyDownCommand { get; }

    public IRelayCommand<KeyEventArgs> KeyUpCommand { get; }


    public MainWindowViewModel()
    {
        var cubeContexts = CreateCubeContexts(6);
        _selectedCubeContext = cubeContexts[1];

        _mapSubCubeViewModelToContext = CreateMapSubCubeViewModelToContext(cubeContexts);
        _mapCubeContextToKeySubscriber = CreateMapCubeContextToKeySubscriber(cubeContexts);

        CubeListViewModel = CreateCubeListViewModel(cubeContexts, SelectedCubeContext);
        CubeListViewModel.PropertyChanged += OnSelectedCubeViewModelPropertyChanged;

        ButtonGroupViewModel = CreateButtonGroupViewModel();

        KeyDownCommand = new RelayCommand<KeyEventArgs>(e => HandleKeyEventArgs(e!), _ => _isHandledKey);
        KeyUpCommand = new RelayCommand<KeyEventArgs>(e => HandleKeyEventArgs(e!), _ => _isHandledKey);
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

    private Dictionary<IRubiksCubeContext, ISubscriber<KeyEventArgs>> CreateMapCubeContextToKeySubscriber(
        IEnumerable<IRubiksCubeContext> cubeContexts)
    {
        return cubeContexts
            .Select(cubeContext => (cubeContext, _serviceProvider.KeySubscriberBuilder.Build(cubeContext)))
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

    private ButtonGroupViewModel CreateButtonGroupViewModel()
    {
        const int delayTime = 15;
        var isEnabledButtons = true;

        return new ButtonGroupViewModel
        {
            ResetSelectedCubeCommand = new RelayCommand(ResetSelectedCube, GetIsEnabledButtons),
            ShuffleSelectedCubeCommand = new AsyncRelayCommand(ShuffleSelectedCube, GetIsEnabledButtons),
            ResetAllCubesCommand = new RelayCommand(ResetAllCubes, GetIsEnabledButtons),
            ShuffleAllCubesCommand = new AsyncRelayCommand(ShuffleAllCubes, GetIsEnabledButtons),
        };

        void ResetSelectedCube() => SelectedCubeContext.Recover();

        async Task ShuffleSelectedCube() => await ExecuteWithDisableAsync(() => SelectedCubeContext.ShuffleAsync(delayTime));

        void ResetAllCubes()
        {
            var cubeContexts = GetCubeContexts();
            foreach (var cubeContext in cubeContexts) cubeContext.Recover();
        }

        async Task ShuffleAllCubes() => await ExecuteWithDisableAsync(async () =>
        {
            var cubeContexts = GetCubeContexts();
            var shuffleTasks = cubeContexts.Select(cubeContext => cubeContext.ShuffleAsync(delayTime));
            await Task.WhenAll(shuffleTasks);
        });

        async Task ExecuteWithDisableAsync(Func<Task> func)
        {
            Disable();
            await func();
            Enable();
        }

        void Disable()
        {
            isEnabledButtons = _isHandledKey = false;
            _isShuffling = true;

            NotifyCanExecuteChangedButtonCommands();
            NotifyCanExecuteChangedKeyCommands();
            OnPropertyChanged(nameof(IsCubeListControlEnabled));
        }

        void Enable()
        {
            isEnabledButtons = _isHandledKey = true;
            _isShuffling = false;

            NotifyCanExecuteChangedButtonCommands();
            NotifyCanExecuteChangedKeyCommands();
            OnPropertyChanged(nameof(IsCubeListControlEnabled));
        }

        IReadOnlyList<IRubiksCubeContext> GetCubeContexts() => _mapCubeContextToKeySubscriber.Keys.ToList();

        bool GetIsEnabledButtons() => isEnabledButtons;
    }

    private void NotifyCanExecuteChangedButtonCommands()
    {
        ButtonGroupViewModel.ResetSelectedCubeCommand!.NotifyCanExecuteChanged();
        ButtonGroupViewModel.ShuffleSelectedCubeCommand!.NotifyCanExecuteChanged();
        ButtonGroupViewModel.ResetAllCubesCommand!.NotifyCanExecuteChanged();
        ButtonGroupViewModel.ShuffleAllCubesCommand!.NotifyCanExecuteChanged();
    }

    private void NotifyCanExecuteChangedKeyCommands()
    {
        KeyDownCommand.NotifyCanExecuteChanged();
        KeyUpCommand.NotifyCanExecuteChanged();
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
        var keyEventSubscriber = _mapCubeContextToKeySubscriber[SelectedCubeContext];
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