using System.Windows;
using RubiksCubeSimulator.Wpf.Events;
using RubiksCubeSimulator.Wpf.Events.EventArgs.Enums;
using RubiksCubeSimulator.Wpf.Events.EventArgs.MoveRubiksCubeEventArgs;
using RubiksCubeSimulator.Wpf.Infrastructure.RubiksCubeContext;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube.Enums;

namespace RubiksCubeSimulator.Wpf.Infrastructure.EventSubscribers;

public interface IMoveArrowSetter :
    ISubscriber<MovingRubiksCubeEventArgs>,
    ISubscriber<MovedRubiksCubeEventArgs>
{
}

internal sealed class MoveArrowSetter(IRubiksCubeContext cubeContext) : IMoveArrowSetter
{
    public void OnEvent(object sender, MovingRubiksCubeEventArgs keyCubeEventArgs)
    {
        var moveDirection = GetMoveDirection(keyCubeEventArgs);

        var sliceNumber = GetSliceNumber(keyCubeEventArgs);
        cubeContext.CubeViewModel.SetSliceMoveArrows(moveDirection, sliceNumber);
    }

    private MoveDirection GetMoveDirection(MovingRubiksCubeEventArgs movingEventArgs)
    {
        if (movingEventArgs.FaceName == FaceName.Up)
        {
            if (IsMousePointedLowerLeftFacePart(movingEventArgs.StickerNumber, movingEventArgs.RelativeMousePosition))
            {
                return movingEventArgs.MoveKey switch
                {
                    MoveKey.W => MoveDirection.LeftTop,
                    MoveKey.A => MoveDirection.RightTop,
                    MoveKey.S => MoveDirection.LeftBottom,
                    MoveKey.D => MoveDirection.RightBottom,
                    _ => throw new ArgumentOutOfRangeException(nameof(movingEventArgs.MoveKey), movingEventArgs.MoveKey, null)
                };
            }

            return movingEventArgs.MoveKey switch
            {
                MoveKey.W => MoveDirection.RightTop,
                MoveKey.A => MoveDirection.LeftBottom,
                MoveKey.S => MoveDirection.RightBottom,
                MoveKey.D => MoveDirection.LeftTop,
                _ => throw new ArgumentOutOfRangeException(nameof(movingEventArgs.MoveKey), movingEventArgs.MoveKey, null)
            };
        }

        if (movingEventArgs.FaceName == FaceName.Right)
        {
            return movingEventArgs.MoveKey switch
            {
                MoveKey.W => MoveDirection.RightTop,
                MoveKey.A => MoveDirection.Left,
                MoveKey.S => MoveDirection.RightBottom,
                MoveKey.D => MoveDirection.Right,
                _ => throw new ArgumentOutOfRangeException(nameof(movingEventArgs.MoveKey), movingEventArgs.MoveKey, null)
            };
        }

        if (movingEventArgs.FaceName == FaceName.Left)
        {
            return movingEventArgs.MoveKey switch
            {
                MoveKey.W => MoveDirection.LeftTop,
                MoveKey.A => MoveDirection.Left,
                MoveKey.S => MoveDirection.LeftBottom,
                MoveKey.D => MoveDirection.Right,
                _ => throw new ArgumentOutOfRangeException(nameof(movingEventArgs.MoveKey), movingEventArgs.MoveKey, null)
            };
        }

        throw new ArgumentOutOfRangeException(nameof(movingEventArgs.FaceName), movingEventArgs.FaceName, null);
    }

    private int GetSliceNumber(MovingRubiksCubeEventArgs movingEventArgs)
    {
        if (movingEventArgs.FaceName == FaceName.Up)
        {
            if (IsMousePointedLowerLeftFacePart(movingEventArgs.StickerNumber, movingEventArgs.RelativeMousePosition))
            {
                if (movingEventArgs.MoveKey is MoveKey.W or MoveKey.S)
                {
                    return GetColumnIndex(movingEventArgs.StickerNumber);
                }

                return ReverseIndex(GetRowIndex(movingEventArgs.StickerNumber));
            }

            if (movingEventArgs.MoveKey is MoveKey.W or MoveKey.S)
            {
                return ReverseIndex(GetRowIndex(movingEventArgs.StickerNumber));
            }

            return GetColumnIndex(movingEventArgs.StickerNumber);
        }

        if (movingEventArgs.FaceName is FaceName.Right or FaceName.Left)
        {
            if (movingEventArgs.MoveKey is MoveKey.W or MoveKey.S)
            {
                return GetColumnIndex(movingEventArgs.StickerNumber);
            }

            return GetRowIndex(movingEventArgs.StickerNumber);
        }

        throw new ArgumentOutOfRangeException(nameof(movingEventArgs.FaceName), movingEventArgs.FaceName, null);
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


    public void OnEvent(object sender, MovedRubiksCubeEventArgs keyCubeEventArgs)
    {
        cubeContext.CubeViewModel.ClearMoveArrows();
    }
}