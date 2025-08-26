using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace RubiksCubeSimulator.Wpf.UserControls.Views.Behaviors;

internal sealed class RelativeMouseMoveBehavior : Behavior<FrameworkElement>
{
    public event EventHandler<RelativeMouseEventArgs>? RelativeMouseMove;

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.MouseMove += OnMouseMove;
        AssociatedObject.MouseLeave += OnMouseLeave;
    }

    protected override void OnDetaching()
    {
        AssociatedObject.MouseMove -= OnMouseMove;
        AssociatedObject.MouseLeave -= OnMouseLeave;
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
        var frameworkElement = AssociatedObject;
        var point = e.GetPosition(frameworkElement);

        double width = frameworkElement.ActualWidth;
        double height = frameworkElement.ActualHeight;

        var relativeMousePosition = new Point(
            width > 0 ? point.X / width : 0,
            height > 0 ? point.Y / height : 0
        );

        InvokeMouseMoveEvent(relativeMousePosition);
    }

    private void OnMouseLeave(object sender, MouseEventArgs e)
    {
        InvokeMouseMoveEvent(null);
    }

    private void InvokeMouseMoveEvent(Point? relativeMousePosition)
    {
        var eventArgs = new RelativeMouseEventArgs { RelativeMousePosition = relativeMousePosition };
        RelativeMouseMove?.Invoke(this, eventArgs);
    }
}

public sealed class RelativeMouseEventArgs : EventArgs
{
    public required Point? RelativeMousePosition { get; init; }
}