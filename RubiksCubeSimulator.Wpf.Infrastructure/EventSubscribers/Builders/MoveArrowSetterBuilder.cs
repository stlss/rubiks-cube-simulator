using RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext;

namespace RubiksCubeSimulator.Wpf.Infrastructure.EventSubscribers.Builders;

public interface IMoveArrowSetterBuilder
{
    public IMoveArrowSetter Build(IRubiksCubeContext cubeContext);
}

internal sealed class MoveArrowSetterBuilder : IMoveArrowSetterBuilder
{
    public IMoveArrowSetter Build(IRubiksCubeContext cubeContext)
    {
        return new MoveArrowSetter(cubeContext);
    }
}