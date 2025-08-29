using RubiksCubeSimulator.Wpf.App.Infrastructure.EventHandlers;
using RubiksCubeSimulator.Wpf.App.ViewModels.Windows;

namespace RubiksCubeSimulator.Wpf.App.Infrastructure.EventSubscriptionManagers;

internal interface IMainWindowEventSubscriptionManager
{
    public void Subscribe(MainWindowViewModel mainWindowViewModel,
        IMainWindowEventHandler mainWindowViewModelEventHandler);

    public void Unsubscribe(MainWindowViewModel mainWindowViewModel,
        IMainWindowEventHandler secondMainWindowViewModelEventHandler);
}

internal sealed class MainWindowEventHandlerSubscriptionManager : IMainWindowEventSubscriptionManager
{
    public void Subscribe(MainWindowViewModel mainWindowViewModel,
        IMainWindowEventHandler mainWindowViewModelEventHandler)
    {
        mainWindowViewModel.KeyDown += mainWindowViewModelEventHandler.OnKeyDown;
        mainWindowViewModel.KeyUp += mainWindowViewModelEventHandler.OnKeyUp;
    }

    public void Unsubscribe(MainWindowViewModel mainWindowViewModel,
        IMainWindowEventHandler secondMainWindowViewModelEventHandler)
    {
        mainWindowViewModel.KeyDown -= secondMainWindowViewModelEventHandler.OnKeyDown;
        mainWindowViewModel.KeyUp -= secondMainWindowViewModelEventHandler.OnKeyUp;
    }
}