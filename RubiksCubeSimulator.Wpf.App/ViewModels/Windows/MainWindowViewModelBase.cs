using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RubiksCubeSimulator.Wpf.App.Infrastructure;
using RubiksCubeSimulator.Wpf.Events;

namespace RubiksCubeSimulator.Wpf.App.ViewModels.Windows;

internal abstract class MainWindowViewModelBase : ObservableObject, IPublisher<KeyEventArgs>
{
    protected readonly IRubiksCubeServiceProvider ServiceProvider = new RubiksCubeServiceProvider();

    public IRelayCommand<KeyEventArgs>? KeyDownCommand { get; private init; }

    public IRelayCommand<KeyEventArgs>? KeyUpCommand { get; private init; }


    protected MainWindowViewModelBase()
    {
        KeyDownCommand = new RelayCommand<KeyEventArgs>(keyEventArgs => NotifySubscribers(keyEventArgs!));
        KeyUpCommand = new RelayCommand<KeyEventArgs>(keyEventArgs => NotifySubscribers(keyEventArgs!));
    }


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