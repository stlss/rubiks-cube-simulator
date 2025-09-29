using System.Windows;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves.Enums;
using RubiksCubeSimulator.Wpf.Events.EventArgs.Enums;

namespace RubiksCubeSimulator.Wpf.Infrastructure.MoveServices;

internal interface IMoveBuilder
{
    public MoveBase Build(
        int cubeDimension,
        FaceName faceName,
        int stickerNumber,
        Point relativeMousePosition,
        MoveKey moveKey,
        bool shiftPressed);
}

internal sealed class MoveBuilder : IMoveBuilder
{
    public MoveBase Build(
        int cubeDimension,
        FaceName faceName,
        int stickerNumber,
        Point relativeMousePosition,
        MoveKey moveKey,
        bool shiftPressed)
    {
        var moveFace = GetMoveFace(cubeDimension, faceName, stickerNumber, relativeMousePosition, moveKey);
        var moveDirection = GetMoveDirection(cubeDimension, faceName, stickerNumber, relativeMousePosition, moveKey);

        if (shiftPressed)
        {
            var axisName = GetAxisName(moveFace);
            var wholeMove = new WholeMove(axisName, moveDirection);
            return wholeMove;
        }

        var sliceNumber = GetSliceNumber(cubeDimension, faceName, stickerNumber, relativeMousePosition, moveKey);
        var sliceMove = new SliceMove(moveFace, moveDirection, sliceNumber);
        return sliceMove;
    }

    private static MoveFace GetMoveFace(
        int cubeDimension,
        FaceName faceName,
        int stickerNumber,
        Point relativeMousePosition,
        MoveKey moveKey)
    {
        if (faceName == FaceName.Up)
        {
            if (IsMousePointedLowerLeftFacePart(cubeDimension, stickerNumber, relativeMousePosition))
            {
                return moveKey switch
                {
                    MoveKey.W => MoveFace.Right,
                    MoveKey.A => MoveFace.Front,
                    MoveKey.S => MoveFace.Right,
                    MoveKey.D => MoveFace.Front,
                    _ => throw new ArgumentOutOfRangeException(nameof(moveKey), moveKey, null)
                };
            }

            return moveKey switch
            {
                MoveKey.W => MoveFace.Front,
                MoveKey.A => MoveFace.Right,
                MoveKey.S => MoveFace.Front,
                MoveKey.D => MoveFace.Right,
                _ => throw new ArgumentOutOfRangeException(nameof(moveKey), moveKey, null)
            };
        }

        if (faceName == FaceName.Right)
        {
            return moveKey switch
            {
                MoveKey.W => MoveFace.Front,
                MoveKey.A => MoveFace.Up,
                MoveKey.S => MoveFace.Front,
                MoveKey.D => MoveFace.Up,
                _ => throw new ArgumentOutOfRangeException(nameof(moveKey), moveKey, null)
            };
        }

        if (faceName == FaceName.Left)
        {
            return moveKey switch
            {
                MoveKey.W => MoveFace.Right,
                MoveKey.A => MoveFace.Up,
                MoveKey.S => MoveFace.Right,
                MoveKey.D => MoveFace.Up,
                _ => throw new ArgumentOutOfRangeException(nameof(moveKey), moveKey, null)
            };
        }

        throw new ArgumentOutOfRangeException(nameof(faceName), faceName, null);
    }
    
    private static MoveDirection GetMoveDirection(
        int cubeDimension,
        FaceName faceName,
        int stickerNumber,
        Point relativeMousePosition,
        MoveKey moveKey)
    {
        if (faceName == FaceName.Up)
        {
            if (IsMousePointedLowerLeftFacePart(cubeDimension, stickerNumber, relativeMousePosition))
            {
                return moveKey switch
                {
                    MoveKey.W => MoveDirection.Clockwise,
                    MoveKey.A => MoveDirection.Counterclockwise,
                    MoveKey.S => MoveDirection.Counterclockwise,
                    MoveKey.D => MoveDirection.Clockwise,
                    _ => throw new ArgumentOutOfRangeException(nameof(moveKey), moveKey, null)
                };
            }

            return moveKey switch
            {
                MoveKey.W => MoveDirection.Counterclockwise,
                MoveKey.A => MoveDirection.Counterclockwise,
                MoveKey.S => MoveDirection.Clockwise,
                MoveKey.D => MoveDirection.Clockwise,
                _ => throw new ArgumentOutOfRangeException(nameof(moveKey), moveKey, null)
            };
        }

        if (faceName == FaceName.Right)
        {
            return moveKey switch
            {
                MoveKey.W => MoveDirection.Counterclockwise,
                MoveKey.A => MoveDirection.Clockwise,
                MoveKey.S => MoveDirection.Clockwise,
                MoveKey.D => MoveDirection.Counterclockwise,
                _ => throw new ArgumentOutOfRangeException(nameof(moveKey), moveKey, null)
            };
        }

        if (faceName == FaceName.Left)
        {
            return moveKey switch
            {
                MoveKey.W => MoveDirection.Clockwise,
                MoveKey.A => MoveDirection.Clockwise,
                MoveKey.S => MoveDirection.Counterclockwise,
                MoveKey.D => MoveDirection.Counterclockwise,
                _ => throw new ArgumentOutOfRangeException(nameof(moveKey), moveKey, null)
            };
        }

        throw new ArgumentOutOfRangeException(nameof(faceName), faceName, null);
    }

    private static AxisName GetAxisName(MoveFace moveFace)
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

    private static int GetSliceNumber(
        int cubeDimension,
        FaceName faceName,
        int stickerNumber,
        Point relativeMousePosition,
        MoveKey moveKey)
    {
        if (faceName == FaceName.Up)
        {
            if (IsMousePointedLowerLeftFacePart(cubeDimension, stickerNumber, relativeMousePosition))
            {
                if (moveKey is MoveKey.W or MoveKey.S)
                {
                    return ReverseIndex(cubeDimension, GetColumnIndex(cubeDimension, stickerNumber));
                }

                return ReverseIndex(cubeDimension, GetRowIndex(cubeDimension, stickerNumber));
            }

            if (moveKey is MoveKey.W or MoveKey.S)
            {
                return ReverseIndex(cubeDimension, GetRowIndex(cubeDimension, stickerNumber));
            }

            return GetColumnIndex(cubeDimension, stickerNumber);
        }

        if (faceName == FaceName.Right)
        {
            if (moveKey is MoveKey.W or MoveKey.S)
            {
                return GetColumnIndex(cubeDimension, stickerNumber);
            }

            return GetRowIndex(cubeDimension, stickerNumber);
        }

        if (faceName == FaceName.Left)
        {
            if (moveKey is MoveKey.W or MoveKey.S)
            {
                return ReverseIndex(cubeDimension, GetColumnIndex(cubeDimension, stickerNumber));
            }

            return GetRowIndex(cubeDimension, stickerNumber);
        }

        throw new ArgumentOutOfRangeException(nameof(faceName), faceName, null);
    }

    private static bool IsMousePointedLowerLeftFacePart(int cubeDimension, int stickerNumber, Point relativeMousePosition)
    {
        var row = stickerNumber / cubeDimension;
        var column = stickerNumber % cubeDimension;

        return row > column || (row == column && relativeMousePosition.X <= relativeMousePosition.Y);
    }

    private static int GetRowIndex(int cubeDimension, int stickerNumber) => stickerNumber / cubeDimension;

    private static int GetColumnIndex(int cubeDimension, int stickerNumber) => stickerNumber % cubeDimension;

    private static int ReverseIndex(int cubeDimension, int index) => cubeDimension - index - 1;
}