using RubiksCubeSimulator.Wpf.Events;

namespace RubiksCubeSimulator.Wpf.Infrastructure.EventPublishers;

public abstract class PublisherBase<T> : IPublisher<T> where T : EventArgs
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