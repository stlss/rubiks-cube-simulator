using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves.Enums;
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

        ServiceProvider.MovingRubiksCubePublisher.Subscribe(ServiceProvider.MovedRubiksCubePublisher);
        ServiceProvider.MovedRubiksCubePublisher.Subscribe(ServiceProvider.MovingRubiksCubePublisher);

        var moveArrowSetter = ServiceProvider.MoveArrowSetterBuilder.Build(_cubeContext);
        var moveApplier = ServiceProvider.MoveApplierBuilder.Build(_cubeContext);

        ServiceProvider.MovingRubiksCubePublisher.Subscribe(moveArrowSetter);
        ServiceProvider.MovedRubiksCubePublisher.Subscribe(moveArrowSetter);
        ServiceProvider.MovedRubiksCubePublisher.Subscribe(moveApplier);
    }
}