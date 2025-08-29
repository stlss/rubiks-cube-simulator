using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RubiksCubeSimulator.Domain.Services;
using RubiksCubeSimulator.Wpf.App.Infrastructure.RubiksCubeControlViewModelBuilders;
using RubiksCubeSimulator.Wpf.App.Infrastructure.RubiksCubeServiceProvider;
using RubiksCubeSimulator.Wpf.App.Infrastructure.ViewModelEventLinker;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.App.ViewModels.Windows;

internal sealed class MainWindowViewModel : ObservableObject
{
    private readonly IRubiksCubeBuilder _cubeBuilder;
    private readonly IRubiksCubeControlViewModelBuilder _cubeViewModelBuilder;
    private readonly IViewModelEventLinker _viewModelEventLinker;

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
        _viewModelEventLinker = serviceProvider.GetViewModelLinker();

        KeyDownCommand = new RelayCommand<KeyEventArgs>(e => KeyDown?.Invoke(this, e));
        KeyUpCommand = new RelayCommand<KeyEventArgs>(e => KeyUp?.Invoke(this, e));

        SetCubeViewModel();
        LinkViewModels();
    }


    private void SetCubeViewModel()
    {
        var cube = _cubeBuilder.BuildSolvedRubiksCube(2);
        CubeControlViewModel = _cubeViewModelBuilder.Build(cube);
    }

    private void LinkViewModels()
    {
        _viewModelEventLinker.Link(this, CubeControlViewModel);
    }


    public event KeyEventHandler? KeyDown;
    public event KeyEventHandler? KeyUp;


    public IRelayCommand<KeyEventArgs>? KeyDownCommand { get; private init; }

    public IRelayCommand<KeyEventArgs>? KeyUpCommand { get; private init; }
}