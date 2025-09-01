namespace RubiksCubeSimulator.Wpf.Events;

public interface ISubscriber<in T> where T : System.EventArgs
{
    public void OnEvent(object sender, T mouseMoveEventArgs);
}