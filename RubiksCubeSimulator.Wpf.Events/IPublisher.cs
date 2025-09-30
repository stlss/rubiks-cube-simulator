namespace RubiksCubeSimulator.Wpf.Events;

public interface IPublisher<out T> where T : System.EventArgs
{
    public void Subscribe(ISubscriber<T> subscriber);

    public void Unsubscribe(ISubscriber<T> subscriber);
}