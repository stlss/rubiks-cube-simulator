using System.Windows;
using System.Windows.Input;
using RubiksCubeSimulator.Wpf.App.Infrastructure.EventHandlers.RubiksCube.EventArgs;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

namespace RubiksCubeSimulator.Wpf.App.Infrastructure.EventHandlers.RubiksCube;

internal sealed class RubiksCubeMoveArrowSetter(RubiksCubeControlViewModel cubeViewModel)
    : IMoveRubiksCubeEventHandler
{
    public void OnMovingRubiksCube(object? sender, MovingRubiksCubeEventArgs e)
    {
        var moveDirection = GetMoveDirection(e);

        if (e.PressedShift)
        {
            cubeViewModel.SetWholeCubeMoveArrows(moveDirection);
            return;
        }

        var sliceNumber = GetSliceNumber(e);
        cubeViewModel.SetSliceMoveArrows(moveDirection, sliceNumber);
    }

    private MoveDirection GetMoveDirection(MovingRubiksCubeEventArgs e)
    {
        if (e.FaceName == FaceName.Up)
        {
            if (IsMousePointedLowerLeftFacePart(e.StickerNumber, e.RelativeMousePosition))
            {
                return e.PressedMoveKey switch
                {
                    Key.W => MoveDirection.LeftTop,
                    Key.A => MoveDirection.RightTop,
                    Key.S => MoveDirection.LeftBottom,
                    Key.D => MoveDirection.RightBottom,
                    _ => throw new ArgumentOutOfRangeException(nameof(e.PressedMoveKey), e.PressedMoveKey, null)
                };
            }

            return e.PressedMoveKey switch
            {
                Key.W => MoveDirection.RightTop,
                Key.A => MoveDirection.LeftBottom,
                Key.S => MoveDirection.RightBottom,
                Key.D => MoveDirection.LeftTop,
                _ => throw new ArgumentOutOfRangeException(nameof(e.PressedMoveKey), e.PressedMoveKey, null)
            };
        }

        if (e.FaceName == FaceName.Right)
        {
            return e.PressedMoveKey switch
            {
                Key.W => MoveDirection.RightTop,
                Key.A => MoveDirection.Left,
                Key.S => MoveDirection.RightBottom,
                Key.D => MoveDirection.Right,
                _ => throw new ArgumentOutOfRangeException(nameof(e.PressedMoveKey), e.PressedMoveKey, null)
            };
        }

        if (e.FaceName == FaceName.Left)
        {
            return e.PressedMoveKey switch
            {
                Key.W => MoveDirection.LeftTop,
                Key.A => MoveDirection.Left,
                Key.S => MoveDirection.LeftBottom,
                Key.D => MoveDirection.Right,
                _ => throw new ArgumentOutOfRangeException(nameof(e.PressedMoveKey), e.PressedMoveKey, null)
            };
        }

        throw new ArgumentOutOfRangeException(nameof(e.FaceName), e.FaceName, null);
    }

    private int GetSliceNumber(MovingRubiksCubeEventArgs e)
    {
        if (e.FaceName == FaceName.Up)
        {
            if (IsMousePointedLowerLeftFacePart(e.StickerNumber, e.RelativeMousePosition))
            {
                if (e.PressedMoveKey == Key.W || e.PressedMoveKey == Key.S)
                {
                    return e.StickerNumber % cubeViewModel.CubeDimension;
                }

                return cubeViewModel.CubeDimension - e.StickerNumber / cubeViewModel.CubeDimension - 1;
            }

            if (e.PressedMoveKey == Key.W || e.PressedMoveKey == Key.S)
            {
                return cubeViewModel.CubeDimension - e.StickerNumber / cubeViewModel.CubeDimension - 1;
            }

            return e.StickerNumber % cubeViewModel.CubeDimension;
        }

        if (e.FaceName == FaceName.Right || e.FaceName == FaceName.Left)
        {
            if (e.PressedMoveKey == Key.W || e.PressedMoveKey == Key.S)
            {
                return e.StickerNumber % cubeViewModel.CubeDimension;
            }

            return e.StickerNumber / cubeViewModel.CubeDimension;
        }

        throw new ArgumentOutOfRangeException(nameof(e.FaceName), e.FaceName, null);
    }


    private bool IsMousePointedLowerLeftFacePart(int stickerNumber, Point relativeMousePosition)
    {
        var row = stickerNumber / cubeViewModel.CubeDimension;
        var column = stickerNumber % cubeViewModel.CubeDimension;

        return row > column || (row == column && relativeMousePosition.X <= relativeMousePosition.Y);
    }

    public void OnMovedRubiksCube(object? sender, MovedRubiksCubeEventArgs e)
    {
        cubeViewModel.ClearMoveArrows();
    }
}