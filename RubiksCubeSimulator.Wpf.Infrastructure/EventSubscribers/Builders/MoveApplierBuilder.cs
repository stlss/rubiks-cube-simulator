using RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext;

namespace RubiksCubeSimulator.Wpf.Infrastructure.EventSubscribers.Builders;

public interface IMoveApplierBuilder
{
    public IMoveApplier Build(IRubiksCubeContext cubeContext);
}

internal sealed class MoveApplierBuilder : IMoveApplierBuilder
{
    public IMoveApplier Build(IRubiksCubeContext cubeContext)
    {
        return new MoveApplier(cubeContext);
    }
}