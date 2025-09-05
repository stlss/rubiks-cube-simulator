using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RubiksCubeSimulator.Wpf.App.Infrastructure;
using RubiksCubeSimulator.Wpf.Events;
using RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.App.ViewModels.Windows;

internal sealed class MainWindowViewModel : ObservableObject, IPublisher<KeyEventArgs>
{
    private readonly IRubiksCubeContext _cubeContext;

    public RubiksCubeControlViewModel CubeViewModel => _cubeContext.CubeViewModel;


    public MainWindowViewModel() : this(new RubiksCubeServiceProvider())
    {
    }

    private MainWindowViewModel(IRubiksCubeServiceProvider serviceProvider)
    {
        var cubeBuilderContextBuilder = serviceProvider.RubiksCubeContextBuilder;
        _cubeContext = cubeBuilderContextBuilder.Build(3);

        KeyDownCommand = new RelayCommand<KeyEventArgs>(keyEventArgs => NotifySubscribers(keyEventArgs!));
        KeyUpCommand = new RelayCommand<KeyEventArgs>(keyEventArgs => NotifySubscribers(keyEventArgs!));
    }


    public IRelayCommand<KeyEventArgs>? KeyDownCommand { get; private init; }

    public IRelayCommand<KeyEventArgs>? KeyUpCommand { get; private init; }


    private readonly List<ISubscriber<KeyEventArgs>> _keyEventSubscribers = [];

    public void Subscribe(ISubscriber<KeyEventArgs> subscriber)
    {
        _keyEventSubscribers.Add(subscriber);
    }

    public void Unsubscribe(ISubscriber<KeyEventArgs> subscriber)
    {
        _keyEventSubscribers.Remove(subscriber);
    }

    private void NotifySubscribers(KeyEventArgs keyEventArgs)
    {
        foreach (var subscriber in _keyEventSubscribers) subscriber.OnEvent(this, keyEventArgs);
    }
}