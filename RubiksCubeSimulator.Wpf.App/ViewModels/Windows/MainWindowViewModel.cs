using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RubiksCubeSimulator.Wpf.App.Infrastructure.RubiksCubeServiceProvider;
using RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.App.ViewModels.Windows;

internal sealed class MainWindowViewModel : ObservableObject
{
    private readonly IRubiksCubeContext _cubeContext;

    public RubiksCubeControlViewModel CubeViewModel => _cubeContext.CubeViewModel;


    public MainWindowViewModel() : this(new RubiksCubeServiceProvider())
    {
    }

    private MainWindowViewModel(IRubiksCubeServiceProvider serviceProvider)
    {
        var cubeBuilderContextBuilder = serviceProvider.GetRubiksCubeContextBuilder();
        _cubeContext = cubeBuilderContextBuilder.Build(3);

        KeyDownCommand = new RelayCommand<KeyEventArgs>(e => KeyDown?.Invoke(this, e));
        KeyUpCommand = new RelayCommand<KeyEventArgs>(e => KeyUp?.Invoke(this, e));
    }


    public event KeyEventHandler? KeyDown;
    public event KeyEventHandler? KeyUp;


    public IRelayCommand<KeyEventArgs>? KeyDownCommand { get; private init; }

    public IRelayCommand<KeyEventArgs>? KeyUpCommand { get; private init; }
}