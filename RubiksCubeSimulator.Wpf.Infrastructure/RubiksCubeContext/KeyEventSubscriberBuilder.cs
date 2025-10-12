using System.Windows.Input;
using RubiksCubeSimulator.Wpf.Events;
using RubiksCubeSimulator.Wpf.Events.EventArgs;
using RubiksCubeSimulator.Wpf.Infrastructure.EventPublishers.Builders;
using RubiksCubeSimulator.Wpf.Infrastructure.EventSubscribers.Builders;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext;

public interface IKeySubscriberBuilder
{
    public ISubscriber<KeyEventArgs> Build(IRubiksCubeContext cubeContext);
}

internal sealed class KeySubscriberBuilder(
    IKeyRubiksCubePublisherBuilder keyCubePublisherBuilder,
    IMovingRubiksCubePublisherBuilder movingCubePublisherBuilder,
    IMovedRubiksCubePublisherBuilder movedCubePublisherBuilder,
    IMoveArrowSetterBuilder moveArrowSetterBuilder,
    IMoveApplierBuilder moveApplierBuilder) : IKeySubscriberBuilder
{
    public ISubscriber<KeyEventArgs> Build(IRubiksCubeContext cubeContext)
    {
        var keyCubePublisher = keyCubePublisherBuilder.Build();
        var movingCubePublisher = movingCubePublisherBuilder.Build();
        var movedCubePublisher = movedCubePublisherBuilder.Build();

        var moveArrowSetter = moveArrowSetterBuilder.Build(cubeContext);
        var moveApplier = moveApplierBuilder.Build(cubeContext);

        keyCubePublisher.Subscribe(movingCubePublisher);
        keyCubePublisher.Subscribe(movedCubePublisher);

        movingCubePublisher.Subscribe(movedCubePublisher);
        movedCubePublisher.Subscribe(movingCubePublisher);

        Subscribe(cubeContext.MainCubeViewModel, movingCubePublisher);
        Subscribe(cubeContext.MainCubeViewModel, movedCubePublisher);

        movingCubePublisher.Subscribe(moveArrowSetter);
        movedCubePublisher.Subscribe(moveArrowSetter);

        movedCubePublisher.Subscribe(moveApplier);

        return keyCubePublisher;
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