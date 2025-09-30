namespace RubiksCubeSimulator.Wpf.Infrastructure.EventPublishers.Builders;

internal interface IKeyRubiksCubePublisherBuilder
{
    public IKeyRubiksCubePublisher Build();
}

internal sealed class KeyRubiksCubePublisherBuilder : IKeyRubiksCubePublisherBuilder
{
    public IKeyRubiksCubePublisher Build()
    {
        return new KeyRubiksCubePublisher();
    }
}