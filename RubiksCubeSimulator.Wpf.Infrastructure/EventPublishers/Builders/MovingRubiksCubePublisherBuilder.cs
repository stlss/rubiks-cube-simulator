namespace RubiksCubeSimulator.Wpf.Infrastructure.EventPublishers.Builders;

internal interface IMovingRubiksCubePublisherBuilder
{
    public IMovingRubiksCubePublisher Build();
}

internal sealed class MovingRubiksCubePublisherBuilder : IMovingRubiksCubePublisherBuilder
{
    public IMovingRubiksCubePublisher Build()
    {
        return new MovingRubiksCubePublisher();
    }
}