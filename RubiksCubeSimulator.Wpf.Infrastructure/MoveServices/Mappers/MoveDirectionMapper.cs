using ViewModelEnums = RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube.Enums;
using DomainEnums = RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves.Enums;

namespace RubiksCubeSimulator.Wpf.Infrastructure.MoveServices.Mappers;

internal interface IMoveDirectionMapper
{
    public ViewModelEnums.MoveDirection Map(DomainEnums.AxisName axisName, DomainEnums.MoveDirection moveDirection);

    public ViewModelEnums.MoveDirection Map(DomainEnums.FaceName faceName, DomainEnums.MoveDirection moveDirection);
}

internal sealed class MoveDirectionMapper : IMoveDirectionMapper
{
    public ViewModelEnums.MoveDirection Map(DomainEnums.FaceName faceName, DomainEnums.MoveDirection moveDirection)
    {
        var axisName = Map(faceName);
        var viewModelMoveDirection = Map(axisName, moveDirection);
        return viewModelMoveDirection;
    }

    private static DomainEnums.AxisName Map(DomainEnums.FaceName faceName)
    {
        return faceName switch
        {
            DomainEnums.FaceName.Up => DomainEnums.AxisName.Y,
            DomainEnums.FaceName.Right => DomainEnums.AxisName.X,
            DomainEnums.FaceName.Front => DomainEnums.AxisName.Z,
            DomainEnums.FaceName.Down => DomainEnums.AxisName.Y,
            DomainEnums.FaceName.Left => DomainEnums.AxisName.X,
            DomainEnums.FaceName.Back => DomainEnums.AxisName.Z,
            _ => throw new ArgumentOutOfRangeException(nameof(faceName), faceName, null),
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