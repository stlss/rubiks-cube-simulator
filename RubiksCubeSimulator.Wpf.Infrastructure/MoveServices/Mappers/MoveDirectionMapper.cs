using ViewModelEnums = RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube.Enums;
using DomainEnums = RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves.Enums;

namespace RubiksCubeSimulator.Wpf.Infrastructure.MoveServices.Mappers;

internal interface IMoveDirectionMapper
{
    public ViewModelEnums.MoveDirection Map(DomainEnums.AxisName axisName, DomainEnums.MoveDirection moveDirection);

    public ViewModelEnums.MoveDirection Map(DomainEnums.MoveFace moveFace, DomainEnums.MoveDirection moveDirection);
}

internal sealed class MoveDirectionMapper : IMoveDirectionMapper
{
    public ViewModelEnums.MoveDirection Map(DomainEnums.MoveFace moveFace, DomainEnums.MoveDirection moveDirection)
    {
        var axisName = Map(moveFace);
        var viewModelMoveDirection = Map(axisName, moveDirection);
        return viewModelMoveDirection;
    }

    private static DomainEnums.AxisName Map(DomainEnums.MoveFace moveFace)
    {
        return moveFace switch
        {
            DomainEnums.MoveFace.Up => DomainEnums.AxisName.Y,
            DomainEnums.MoveFace.Right => DomainEnums.AxisName.X,
            DomainEnums.MoveFace.Front => DomainEnums.AxisName.Z,
            DomainEnums.MoveFace.Down => DomainEnums.AxisName.Y,
            DomainEnums.MoveFace.Left => DomainEnums.AxisName.X,
            DomainEnums.MoveFace.Back => DomainEnums.AxisName.Z,
            _ => throw new ArgumentOutOfRangeException(nameof(moveFace), moveFace, null),
        };
    }

    public ViewModelEnums.MoveDirection Map(DomainEnums.AxisName axisName, DomainEnums.MoveDirection moveDirection)
    {
        return axisName switch
        {
            DomainEnums.AxisName.X => moveDirection switch
            {
                DomainEnums.MoveDirection.Clockwise => ViewModelEnums.MoveDirection.LeftTop,
                DomainEnums.MoveDirection.Counterclockwise => ViewModelEnums.MoveDirection.LeftBottom,
                _ => throw new ArgumentOutOfRangeException(nameof(moveDirection), moveDirection, null),
            },

            DomainEnums.AxisName.Y => moveDirection switch
            {
                DomainEnums.MoveDirection.Clockwise => ViewModelEnums.MoveDirection.Left,
                DomainEnums.MoveDirection.Counterclockwise => ViewModelEnums.MoveDirection.Right,
                _ => throw new ArgumentOutOfRangeException(nameof(moveDirection), moveDirection, null),
            },

            DomainEnums.AxisName.Z => moveDirection switch
            {
                DomainEnums.MoveDirection.Clockwise => ViewModelEnums.MoveDirection.RightBottom,
                DomainEnums.MoveDirection.Counterclockwise => ViewModelEnums.MoveDirection.RightTop,
                _ => throw new ArgumentOutOfRangeException(nameof(moveDirection), moveDirection, null),
            },

            _ => throw new ArgumentOutOfRangeException(nameof(axisName), axisName, null),
        };
    }
}