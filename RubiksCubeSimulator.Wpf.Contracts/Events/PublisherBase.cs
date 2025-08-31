namespace RubiksCubeSimulator.Wpf.Contracts.Events;

public abstract class PublisherBase<T> where T : System.EventArgs
{
    private readonly List<ISubscriber<T>> _subscribers = [];

    public void Subscribe(ISubscriber<T> subscriber)
    {
        _subscribers.Add(subscriber);
    }

    public void Unsubscribe(ISubscriber<T> subscriber)
    {
        _subscribers.Remove(subscriber);
    }

    protected void NotifySubscribers(T eventArgs)
    {
        foreach (var subscriber in _subscribers) subscriber.OnEvent(this, eventArgs);
    }
}