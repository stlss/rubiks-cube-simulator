using RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.App.ViewModels.Windows;

internal sealed class MainWindowViewModel : MainWindowViewModelBase
{
    private readonly IRubiksCubeContext _cubeContext;

    public RubiksCubeControlViewModel CubeViewModel => _cubeContext.CubeViewModel;


    public MainWindowViewModel()
    {
        _cubeContext = CreateCubeContext();
        Subscribe();
    }


    private IRubiksCubeContext CreateCubeContext()
    {
        return ServiceProvider.RubiksCubeContextBuilder.Build(3);
    }

    private void Subscribe()
    {
        Subscribe(ServiceProvider.KeyRubiksCubePublisher);

        ServiceProvider.KeyRubiksCubePublisher.Subscribe(ServiceProvider.MovingRubiksCubePublisher);

        _cubeContext.CubeViewModel.UpFaceViewModel.Subscribe(ServiceProvider.MovingRubiksCubePublisher);
        _cubeContext.CubeViewModel.RightFaceViewModel.Subscribe(ServiceProvider.MovingRubiksCubePublisher);
        _cubeContext.CubeViewModel.LeftFaceViewModel.Subscribe(ServiceProvider.MovingRubiksCubePublisher);

        ServiceProvider.KeyRubiksCubePublisher.Subscribe(ServiceProvider.MovedRubiksCubePublisher);

        _cubeContext.CubeViewModel.UpFaceViewModel.Subscribe(ServiceProvider.MovedRubiksCubePublisher);
        _cubeContext.CubeViewModel.RightFaceViewModel.Subscribe(ServiceProvider.MovedRubiksCubePublisher);
        _cubeContext.CubeViewModel.LeftFaceViewModel.Subscribe(ServiceProvider.MovedRubiksCubePublisher);

        var arrowSetter = ServiceProvider.ArrowSetterBuilder.Build(_cubeContext);

        ServiceProvider.MovingRubiksCubePublisher.Subscribe(arrowSetter);
        ServiceProvider.MovedRubiksCubePublisher.Subscribe(arrowSetter);
    }
}