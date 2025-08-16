using System.ComponentModel;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves.Enums;

namespace RubiksCubeSimulator.UnitTests.Infrastructure.RubiksCubeProviders;

internal static class OppositeMoveDirectionProvider
{
    public static MoveDirection GetOppositeMoveDirection(MoveDirection moveDirection)
    {
        return moveDirection switch
        {
            MoveDirection.Clockwise => MoveDirection.CounterClockwise,
            MoveDirection.CounterClockwise => MoveDirection.Clockwise,

            _ => throw new InvalidEnumArgumentException(
                nameof(moveDirection),
                (int)moveDirection,
                typeof(MoveDirection)),
        };
    }
}