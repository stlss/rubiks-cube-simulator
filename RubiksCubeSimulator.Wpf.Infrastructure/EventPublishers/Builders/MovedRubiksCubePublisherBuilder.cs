namespace RubiksCubeSimulator.Wpf.Infrastructure.EventPublishers.Builders;

internal interface IMovedRubiksCubePublisherBuilder
{
    public IMovedRubiksCubePublisher Build();
}

internal sealed class MovedRubiksCubePublisherBuilder : IMovedRubiksCubePublisherBuilder
{
    public IMovedRubiksCubePublisher Build()
    {
        return new MovedRubiksCubePublisher();
    }
}