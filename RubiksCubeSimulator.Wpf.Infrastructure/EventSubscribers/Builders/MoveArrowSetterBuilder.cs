using RubiksCubeSimulator.Wpf.Infrastructure.MoveServices;
using RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext;

namespace RubiksCubeSimulator.Wpf.Infrastructure.EventSubscribers.Builders;

internal interface IMoveArrowSetterBuilder
{
    public IMoveArrowSetter Build(IRubiksCubeContext cubeContext);
}

internal sealed class MoveArrowSetterBuilder(IMoveBuilder moveBuilder) : IMoveArrowSetterBuilder
{
    public IMoveArrowSetter Build(IRubiksCubeContext cubeContext)
    {
        return new MoveArrowSetter(cubeContext, moveBuilder);
    }
}