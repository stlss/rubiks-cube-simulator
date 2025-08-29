using RubiksCubeSimulator.Wpf.App.Infrastructure.EventHandlers;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.App.Infrastructure.EventSubscriptionManagers;

internal interface IRubiksCubeControlEventSubscriptionManager
{
    public void Subscribe(RubiksCubeControlViewModel cubeViewModel, IRubiksCubeControlEventHandler cubeEventHandler);

    public void Unsubscribe(RubiksCubeControlViewModel cubeViewModel, IRubiksCubeControlEventHandler cubeEventHandler);
}

internal sealed class RubiksCubeControlEventSubscriptionManager : IRubiksCubeControlEventSubscriptionManager
{
    public void Subscribe(RubiksCubeControlViewModel cubeViewModel, IRubiksCubeControlEventHandler cubeEventHandler)
    {
        cubeViewModel.UpFaceViewModel.RelativeMouseMove += cubeEventHandler.OnRelativeMouseMoveUpFace;
        cubeViewModel.RightFaceViewModel.RelativeMouseMove += cubeEventHandler.OnRelativeMouseMoveRightFace;
        cubeViewModel.LeftFaceViewModel.RelativeMouseMove += cubeEventHandler.OnRelativeMouseMoveLeftFace;
    }

    public void Unsubscribe(RubiksCubeControlViewModel cubeViewModel, IRubiksCubeControlEventHandler cubeEventHandler)
    {
        cubeViewModel.UpFaceViewModel.RelativeMouseMove -= cubeEventHandler.OnRelativeMouseMoveUpFace;
        cubeViewModel.RightFaceViewModel.RelativeMouseMove -= cubeEventHandler.OnRelativeMouseMoveRightFace;
        cubeViewModel.LeftFaceViewModel.RelativeMouseMove -= cubeEventHandler.OnRelativeMouseMoveLeftFace;
    }
}