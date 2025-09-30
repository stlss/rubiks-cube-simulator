using System.Windows.Input;
using RubiksCubeSimulator.Wpf.Events;
using RubiksCubeSimulator.Wpf.Events.EventArgs;
using RubiksCubeSimulator.Wpf.Infrastructure.EventPublishers.Builders;
using RubiksCubeSimulator.Wpf.Infrastructure.EventSubscribers.Builders;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext;

public interface IRubiksCubeContextLinker
{
    public void Link(IRubiksCubeContext cubeContext, IPublisher<KeyEventArgs> keyEventPublisher);
}

internal sealed class RubiksCubeContextLinker(
    IKeyRubiksCubePublisherBuilder keyCubePublisherBuilder,
    IMovingRubiksCubePublisherBuilder movingCubePublisherBuilder,
    IMovedRubiksCubePublisherBuilder movedCubePublisherBuilder,
    IMoveArrowSetterBuilder moveArrowSetterBuilder,
    IMoveApplierBuilder moveApplierBuilder) : IRubiksCubeContextLinker
{
    public void Link(IRubiksCubeContext cubeContext, IPublisher<KeyEventArgs> keyEventPublisher)
    {
        var keyCubePublisher = keyCubePublisherBuilder.Build();
        var movingCubePublisher = movingCubePublisherBuilder.Build();
        var movedCubePublisher = movedCubePublisherBuilder.Build();

        var moveArrowSetter = moveArrowSetterBuilder.Build(cubeContext);
        var moveApplier = moveApplierBuilder.Build(cubeContext);

        keyEventPublisher.Subscribe(keyCubePublisher);

        keyCubePublisher.Subscribe(movingCubePublisher);
        keyCubePublisher.Subscribe(movedCubePublisher);

        movingCubePublisher.Subscribe(movedCubePublisher);
        movedCubePublisher.Subscribe(movingCubePublisher);

        Subscribe(cubeContext.CubeViewModel, movingCubePublisher);
        Subscribe(cubeContext.CubeViewModel, movedCubePublisher);

        movingCubePublisher.Subscribe(moveArrowSetter);
        movedCubePublisher.Subscribe(moveArrowSetter);

        movedCubePublisher.Subscribe(moveApplier);
    }

    private static void Subscribe(
        RubiksCubeViewModel cubeViewModel,
        ISubscriber<MouseMoveRubiksCubeEventArgs> subscriber)
    {
        cubeViewModel.UpFaceViewModel.Subscribe(subscriber);
        cubeViewModel.RightFaceViewModel.Subscribe(subscriber);
        cubeViewModel.LeftFaceViewModel.Subscribe(subscriber);
    }
}