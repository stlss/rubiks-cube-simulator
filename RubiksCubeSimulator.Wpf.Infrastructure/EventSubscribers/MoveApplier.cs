using System.Windows;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves.Enums;
using RubiksCubeSimulator.Wpf.Events;
using RubiksCubeSimulator.Wpf.Events.EventArgs.Enums;
using RubiksCubeSimulator.Wpf.Events.EventArgs.MoveRubiksCubeEventArgs;
using RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube.Enums;
using DomainMoveDirection = RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves.Enums.MoveDirection;

namespace RubiksCubeSimulator.Wpf.Infrastructure.EventSubscribers;

public interface IMoveApplier :
    ISubscriber<MovedRubiksCubeEventArgs>
{
}

internal sealed class MoveApplier(IRubiksCubeContext cubeContext) : IMoveApplier
{
    public void OnEvent(object sender, MovedRubiksCubeEventArgs movedCubeEventArgs)
    {
        if (movedCubeEventArgs.MoveCanceled) return;

        var moveBase = GetMove(movedCubeEventArgs);

        cubeContext.Move(moveBase);
    }

    private RubiksCubeMoveBase GetMove(MovedRubiksCubeEventArgs movedCubeEventArgs)
    {
        var moveFace = GetMoveFace(movedCubeEventArgs);
        var moveDirection = GetDomainMoveDirection(movedCubeEventArgs);

        if (movedCubeEventArgs.ShiftPressed)
        {
            var axis = GetAxis(moveFace);
            return new WholeRubiksCubeMove(axis, moveDirection);
        }

        var sliceNumber = GetSliceNumber(movedCubeEventArgs);
        return new RubiksCubeSliceMove(moveFace, moveDirection, sliceNumber);
    }

    private MoveFace GetMoveFace(MovedRubiksCubeEventArgs movedCubeEventArgs)
    {
        if (movedCubeEventArgs.FaceName == FaceName.Up)
        {
            if (IsMousePointedLowerLeftFacePart(movedCubeEventArgs.StickerNumber,
                    movedCubeEventArgs.RelativeMousePosition!.Value))
            {
                return movedCubeEventArgs.MoveKey switch
                {
                    MoveKey.W => MoveFace.Right,
                    MoveKey.A => MoveFace.Front,
                    MoveKey.S => MoveFace.Right,
                    MoveKey.D => MoveFace.Front,
                };
            }

            return movedCubeEventArgs.MoveKey switch
            {
                MoveKey.W => MoveFace.Front,
                MoveKey.A => MoveFace.Right,
                MoveKey.S => MoveFace.Front,
                MoveKey.D => MoveFace.Right,
            };
        }

        if (movedCubeEventArgs.FaceName == FaceName.Right)
        {
            return movedCubeEventArgs.MoveKey switch
            {
                MoveKey.W => MoveFace.Front,
                MoveKey.A => MoveFace.Up,
                MoveKey.S => MoveFace.Front,
                MoveKey.D => MoveFace.Up,
            };
        }

        if (movedCubeEventArgs.FaceName == FaceName.Left)
        {
            return movedCubeEventArgs.MoveKey switch
            {
                MoveKey.W => MoveFace.Right,
                MoveKey.A => MoveFace.Up,
                MoveKey.S => MoveFace.Right,
                MoveKey.D => MoveFace.Up,
            };
        }

        throw new ArgumentOutOfRangeException(nameof(movedCubeEventArgs.FaceName), movedCubeEventArgs.FaceName, null);
    }

    private DomainMoveDirection GetDomainMoveDirection(MovedRubiksCubeEventArgs movedCubeEventArgs)
    {
        if (movedCubeEventArgs.FaceName == FaceName.Up)
        {
            if (IsMousePointedLowerLeftFacePart(movedCubeEventArgs.StickerNumber,
                    movedCubeEventArgs.RelativeMousePosition!.Value))
            {
                return movedCubeEventArgs.MoveKey switch
                {
                    MoveKey.W => DomainMoveDirection.Clockwise,
                    MoveKey.A => DomainMoveDirection.Counterclockwise,
                    MoveKey.S => DomainMoveDirection.Counterclockwise,
                    MoveKey.D => DomainMoveDirection.Clockwise,
                };
            }

            return movedCubeEventArgs.MoveKey switch
            {
                MoveKey.W => DomainMoveDirection.Counterclockwise,
                MoveKey.A => DomainMoveDirection.Counterclockwise,
                MoveKey.S => DomainMoveDirection.Clockwise,
                MoveKey.D => DomainMoveDirection.Clockwise,
            };
        }

        if (movedCubeEventArgs.FaceName == FaceName.Right)
        {
            return movedCubeEventArgs.MoveKey switch
            {
                MoveKey.W => DomainMoveDirection.Counterclockwise,
                MoveKey.A => DomainMoveDirection.Clockwise,
                MoveKey.S => DomainMoveDirection.Clockwise,
                MoveKey.D => DomainMoveDirection.Counterclockwise,
            };
        }

        if (movedCubeEventArgs.FaceName == FaceName.Left)
        {
            return movedCubeEventArgs.MoveKey switch
            {
                MoveKey.W => DomainMoveDirection.Clockwise,
                MoveKey.A => DomainMoveDirection.Clockwise,
                MoveKey.S => DomainMoveDirection.Counterclockwise,
                MoveKey.D => DomainMoveDirection.Counterclockwise,
            };
        }

        throw new ArgumentOutOfRangeException(nameof(movedCubeEventArgs.FaceName), movedCubeEventArgs.FaceName, null);
    }

    private int GetSliceNumber(MovedRubiksCubeEventArgs movedCubeEventArgs)
    {
        if (movedCubeEventArgs.FaceName == FaceName.Up)
        {
            if (IsMousePointedLowerLeftFacePart(movedCubeEventArgs.StickerNumber,
                    movedCubeEventArgs.RelativeMousePosition!.Value))
            {
                if (movedCubeEventArgs.MoveKey is MoveKey.W or MoveKey.S)
                {
                    return ReverseIndex(GetColumnIndex(movedCubeEventArgs.StickerNumber));
                }

                return ReverseIndex(GetRowIndex(movedCubeEventArgs.StickerNumber));
            }

            if (movedCubeEventArgs.MoveKey is MoveKey.W or MoveKey.S)
            {
                return ReverseIndex(GetRowIndex(movedCubeEventArgs.StickerNumber));
            }

            return GetColumnIndex(movedCubeEventArgs.StickerNumber);
        }

        if (movedCubeEventArgs.FaceName == FaceName.Right)
        {
            if (movedCubeEventArgs.MoveKey is MoveKey.W or MoveKey.S)
            {
                return GetColumnIndex(movedCubeEventArgs.StickerNumber);
            }

            return GetRowIndex(movedCubeEventArgs.StickerNumber);
        }

        if (movedCubeEventArgs.FaceName == FaceName.Left)
        {
            if (movedCubeEventArgs.MoveKey is MoveKey.W or MoveKey.S)
            {
                return ReverseIndex(GetColumnIndex(movedCubeEventArgs.StickerNumber));
            }

            return GetRowIndex(movedCubeEventArgs.StickerNumber);
        }

        throw new ArgumentOutOfRangeException(nameof(movedCubeEventArgs.FaceName), movedCubeEventArgs.FaceName, null);
    }

    private static AxisName GetAxis(MoveFace moveFace)
    {
        return moveFace switch
        {
            MoveFace.Up => AxisName.Y,
            MoveFace.Right => AxisName.X,
            MoveFace.Front => AxisName.Z,
            MoveFace.Down => AxisName.Y,
            MoveFace.Left => AxisName.X,
            MoveFace.Back => AxisName.Z,
            _ => throw new ArgumentOutOfRangeException(nameof(moveFace), moveFace, null)
        };
    }

    private bool IsMousePointedLowerLeftFacePart(int stickerNumber, Point relativeMousePosition)
    {
        var row = stickerNumber / cubeContext.CubeViewModel.CubeDimension;
        var column = stickerNumber % cubeContext.CubeViewModel.CubeDimension;

        return row > column || (row == column && relativeMousePosition.X <= relativeMousePosition.Y);
    }

    private int GetRowIndex(int stickerNumber) => stickerNumber / cubeContext.CubeViewModel.CubeDimension;

    private int GetColumnIndex(int stickerNumber) => stickerNumber % cubeContext.CubeViewModel.CubeDimension;

    private int ReverseIndex(int index) => cubeContext.CubeViewModel.CubeDimension - index - 1;
}