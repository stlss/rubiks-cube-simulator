using RubiksCubeSimulator.Wpf.Infrastructure.MoveServices;
using RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext;

namespace RubiksCubeSimulator.Wpf.Infrastructure.EventSubscribers.Builders;

internal interface IMoveApplierBuilder
{
    public IMoveApplier Build(IRubiksCubeContext cubeContext);
}

internal sealed class MoveApplierBuilder(IMoveBuilder moveBuilder) : IMoveApplierBuilder
{
    public IMoveApplier Build(IRubiksCubeContext cubeContext)
    {
        return new MoveApplier(cubeContext, moveBuilder);
    }
}