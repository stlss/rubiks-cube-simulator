using System.Windows;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves.Enums;
using EventEnums = RubiksCubeSimulator.Wpf.Events.EventArgs.Enums;

namespace RubiksCubeSimulator.Wpf.Infrastructure.MoveServices;

internal interface IMoveBuilder
{
    public MoveBase Build(
        int cubeDimension,
        EventEnums.FaceName faceName,
        int stickerNumber,
        Point relativeMousePosition,
        EventEnums.MoveKey moveKey,
        bool shiftPressed);
}

internal sealed class MoveBuilder : IMoveBuilder
{
    public MoveBase Build(
        int cubeDimension,
        EventEnums.FaceName faceName,
        int stickerNumber,
        Point relativeMousePosition,
        EventEnums.MoveKey moveKey,
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

    private static FaceName GetMoveFace(
        int cubeDimension,
        EventEnums.FaceName faceName,
        int stickerNumber,
        Point relativeMousePosition,
        EventEnums.MoveKey moveKey)
    {
        return faceName switch
        {
            EventEnums.FaceName.Up => GetMoveFaceWhenUpFaceName(
                cubeDimension,
                stickerNumber,
                relativeMousePosition,
                moveKey),

            EventEnums.FaceName.Right => moveKey switch
            {
                EventEnums.MoveKey.W => FaceName.Front,
                EventEnums.MoveKey.A => FaceName.Up,
                EventEnums.MoveKey.S => FaceName.Front,
                EventEnums.MoveKey.D => FaceName.Up,
                _ => throw new ArgumentOutOfRangeException(nameof(moveKey), moveKey, null)
            },

            EventEnums.FaceName.Left => moveKey switch
            {
                EventEnums.MoveKey.W => FaceName.Right,
                EventEnums.MoveKey.A => FaceName.Up,
                EventEnums.MoveKey.S => FaceName.Right,
                EventEnums.MoveKey.D => FaceName.Up,
                _ => throw new ArgumentOutOfRangeException(nameof(moveKey), moveKey, null)
            },

            _ => throw new ArgumentOutOfRangeException(nameof(faceName), faceName, null)
        };
    }

    private static FaceName GetMoveFaceWhenUpFaceName(
        int cubeDimension,
        int stickerNumber,
        Point relativeMousePosition,
        EventEnums.MoveKey moveKey)
    {
        if (IsMousePointedLowerLeftFacePart(cubeDimension, stickerNumber, relativeMousePosition))
        {
            return moveKey switch
            {
                EventEnums.MoveKey.W => FaceName.Right,
                EventEnums.MoveKey.A => FaceName.Front,
                EventEnums.MoveKey.S => FaceName.Right,
                EventEnums.MoveKey.D => FaceName.Front,
                _ => throw new ArgumentOutOfRangeException(nameof(moveKey), moveKey, null)
            };
        }

        return moveKey switch
        {
            EventEnums.MoveKey.W => FaceName.Front,
            EventEnums.MoveKey.A => FaceName.Right,
            EventEnums.MoveKey.S => FaceName.Front,
            EventEnums.MoveKey.D => FaceName.Right,
            _ => throw new ArgumentOutOfRangeException(nameof(moveKey), moveKey, null)
        };
    }

    private static MoveDirection GetMoveDirection(
        int cubeDimension,
        EventEnums.FaceName faceName,
        int stickerNumber,
        Point relativeMousePosition,
        EventEnums.MoveKey moveKey)
    {
        if (faceName == EventEnums.FaceName.Up)
        {
            if (IsMousePointedLowerLeftFacePart(cubeDimension, stickerNumber, relativeMousePosition))
            {
                return moveKey switch
                {
                    EventEnums.MoveKey.W => MoveDirection.Clockwise,
                    EventEnums.MoveKey.A => MoveDirection.Counterclockwise,
                    EventEnums.MoveKey.S => MoveDirection.Counterclockwise,
                    EventEnums.MoveKey.D => MoveDirection.Clockwise,
                    _ => throw new ArgumentOutOfRangeException(nameof(moveKey), moveKey, null)
                };
            }

            return moveKey switch
            {
                EventEnums.MoveKey.W => MoveDirection.Counterclockwise,
                EventEnums.MoveKey.A => MoveDirection.Counterclockwise,
                EventEnums.MoveKey.S => MoveDirection.Clockwise,
                EventEnums.MoveKey.D => MoveDirection.Clockwise,
                _ => throw new ArgumentOutOfRangeException(nameof(moveKey), moveKey, null)
            };
        }

        if (faceName == EventEnums.FaceName.Right)
        {
            return moveKey switch
            {
                EventEnums.MoveKey.W => MoveDirection.Counterclockwise,
                EventEnums.MoveKey.A => MoveDirection.Clockwise,
                EventEnums.MoveKey.S => MoveDirection.Clockwise,
                EventEnums.MoveKey.D => MoveDirection.Counterclockwise,
                _ => throw new ArgumentOutOfRangeException(nameof(moveKey), moveKey, null)
            };
        }

        if (faceName == EventEnums.FaceName.Left)
        {
            return moveKey switch
            {
                EventEnums.MoveKey.W => MoveDirection.Clockwise,
                EventEnums.MoveKey.A => MoveDirection.Clockwise,
                EventEnums.MoveKey.S => MoveDirection.Counterclockwise,
                EventEnums.MoveKey.D => MoveDirection.Counterclockwise,
                _ => throw new ArgumentOutOfRangeException(nameof(moveKey), moveKey, null)
            };
        }

        throw new ArgumentOutOfRangeException(nameof(faceName), faceName, null);
    }

    private static AxisName GetAxisName(FaceName faceName)
    {
        return faceName switch
        {
            FaceName.Up => AxisName.Y,
            FaceName.Right => AxisName.X,
            FaceName.Front => AxisName.Z,
            FaceName.Down => AxisName.Y,
            FaceName.Left => AxisName.X,
            FaceName.Back => AxisName.Z,
            _ => throw new ArgumentOutOfRangeException(nameof(faceName), faceName, null)
        };
    }

    private static int GetSliceNumber(
        int cubeDimension,
        EventEnums.FaceName faceName,
        int stickerNumber,
        Point relativeMousePosition,
        EventEnums.MoveKey moveKey)
    {
        if (faceName == EventEnums.FaceName.Up)
        {
            if (IsMousePointedLowerLeftFacePart(cubeDimension, stickerNumber, relativeMousePosition))
            {
                if (moveKey is EventEnums.MoveKey.W or EventEnums.MoveKey.S)
                {
                    return ReverseIndex(cubeDimension, GetColumnIndex(cubeDimension, stickerNumber));
                }

                return ReverseIndex(cubeDimension, GetRowIndex(cubeDimension, stickerNumber));
            }

            if (moveKey is EventEnums.MoveKey.W or EventEnums.MoveKey.S)
            {
                return ReverseIndex(cubeDimension, GetRowIndex(cubeDimension, stickerNumber));
            }

            return ReverseIndex(cubeDimension, GetColumnIndex(cubeDimension, stickerNumber));
        }

        if (faceName == EventEnums.FaceName.Right)
        {
            if (moveKey is EventEnums.MoveKey.W or EventEnums.MoveKey.S)
            {
                return GetColumnIndex(cubeDimension, stickerNumber);
            }

            return GetRowIndex(cubeDimension, stickerNumber);
        }

        if (faceName == EventEnums.FaceName.Left)
        {
            if (moveKey is EventEnums.MoveKey.W or EventEnums.MoveKey.S)
            {
                return ReverseIndex(cubeDimension, GetColumnIndex(cubeDimension, stickerNumber));
            }

            return GetRowIndex(cubeDimension, stickerNumber);
        }

        throw new ArgumentOutOfRangeException(nameof(faceName), faceName, null);
    }

    private static bool IsMousePointedLowerLeftFacePart(int cubeDimension, int stickerNumber,
        Point relativeMousePosition)
    {
        var row = stickerNumber / cubeDimension;
        var column = stickerNumber % cubeDimension;

        return row > column || (row == column && relativeMousePosition.X <= relativeMousePosition.Y);
    }

    private static int GetRowIndex(int cubeDimension, int stickerNumber) => stickerNumber / cubeDimension;

    private static int GetColumnIndex(int cubeDimension, int stickerNumber) => stickerNumber % cubeDimension;

    private static int ReverseIndex(int cubeDimension, int index) => cubeDimension - index - 1;
}