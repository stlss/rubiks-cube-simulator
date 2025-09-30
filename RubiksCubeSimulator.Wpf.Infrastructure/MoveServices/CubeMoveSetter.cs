using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves;
using RubiksCubeSimulator.Wpf.Infrastructure.MoveServices.Mappers;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube.Enums;

namespace RubiksCubeSimulator.Wpf.Infrastructure.MoveServices;

internal interface ICubeMoveSetter
{
    public void ShowMoveArrows(RubiksCubeControlViewModel cubeViewModel, MoveBase move);

    public void ClearMoveArrows(RubiksCubeControlViewModel cubeViewModel);
}

internal sealed class CubeCubeMoveSetter(
    IFaceMoveSetter faceMoveSetter,
    IMoveDirectionMapper moveDirectionMapper,
    ISliceNumberMapper sliceNumberMapper) : ICubeMoveSetter
{
    public void ShowMoveArrows(RubiksCubeControlViewModel cubeViewModel, MoveBase move)
    {
        ClearMoveArrows(cubeViewModel);

        switch (move)
        {
            case WholeMove wholeMove:
                ShowMoveArrows(cubeViewModel, wholeMove);
                break;

            case SliceMove sliceMove:
                ShowMoveArrows(cubeViewModel, sliceMove);
                break;
        }
    }

    private void ShowMoveArrows(RubiksCubeControlViewModel cubeViewModel, WholeMove move)
    {
        var moveDirection = moveDirectionMapper.Map(move.AxisName, move.Direction);
        SetMoveArrows(cubeViewModel, moveDirection);
    }

    private void ShowMoveArrows(RubiksCubeControlViewModel cubeViewModel, SliceMove move)
    {
        var moveDirection = moveDirectionMapper.Map(move.FaceName, move.Direction);
        var sliceNumber = sliceNumberMapper.Map(cubeViewModel.CubeDimension, move.FaceName, move.Slice);
        SetMoveArrows(cubeViewModel, moveDirection, sliceNumber);
    }

    private void SetMoveArrows(
        RubiksCubeControlViewModel cubeViewModel,
        MoveDirection moveDirection)
    {
        switch (moveDirection)
        {
            case MoveDirection.Left:
                faceMoveSetter.SetMoveArrows(cubeViewModel.LeftFaceViewModel, ArrowDirection.Left);
                faceMoveSetter.SetMoveArrows(cubeViewModel.RightFaceViewModel, ArrowDirection.Left);
                break;

            case MoveDirection.Right:
                faceMoveSetter.SetMoveArrows(cubeViewModel.LeftFaceViewModel, ArrowDirection.Right);
                faceMoveSetter.SetMoveArrows(cubeViewModel.RightFaceViewModel, ArrowDirection.Right);
                break;

            case MoveDirection.LeftTop:
                faceMoveSetter.SetMoveArrows(cubeViewModel.LeftFaceViewModel, ArrowDirection.Top);
                faceMoveSetter.SetMoveArrows(cubeViewModel.UpFaceViewModel, ArrowDirection.Top);
                break;

            case MoveDirection.LeftBottom:
                faceMoveSetter.SetMoveArrows(cubeViewModel.LeftFaceViewModel, ArrowDirection.Bottom);
                faceMoveSetter.SetMoveArrows(cubeViewModel.UpFaceViewModel, ArrowDirection.Bottom);
                break;

            case MoveDirection.RightTop:
                faceMoveSetter.SetMoveArrows(cubeViewModel.RightFaceViewModel, ArrowDirection.Top);
                faceMoveSetter.SetMoveArrows(cubeViewModel.UpFaceViewModel, ArrowDirection.Left);
                break;

            case MoveDirection.RightBottom:
                faceMoveSetter.SetMoveArrows(cubeViewModel.RightFaceViewModel, ArrowDirection.Bottom);
                faceMoveSetter.SetMoveArrows(cubeViewModel.UpFaceViewModel, ArrowDirection.Right);
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(moveDirection), moveDirection, null);
        }
    }

    private void SetMoveArrows(
        RubiksCubeControlViewModel cubeViewModel,
        MoveDirection moveDirection,
        int sliceNumber)
    {
        switch (moveDirection)
        {
            case MoveDirection.Left:
                faceMoveSetter.SetRowMoveArrows(cubeViewModel.LeftFaceViewModel, ArrowDirection.Left, sliceNumber);
                faceMoveSetter.SetRowMoveArrows(cubeViewModel.RightFaceViewModel, ArrowDirection.Left, sliceNumber);
                break;

            case MoveDirection.Right:
                faceMoveSetter.SetRowMoveArrows(cubeViewModel.LeftFaceViewModel, ArrowDirection.Right, sliceNumber);
                faceMoveSetter.SetRowMoveArrows(cubeViewModel.RightFaceViewModel, ArrowDirection.Right, sliceNumber);
                break;

            case MoveDirection.LeftTop:
                faceMoveSetter.SetColumnMoveArrows(cubeViewModel.LeftFaceViewModel, ArrowDirection.Top, sliceNumber);
                faceMoveSetter.SetColumnMoveArrows(cubeViewModel.UpFaceViewModel, ArrowDirection.Top, sliceNumber);
                break;

            case MoveDirection.LeftBottom:
                faceMoveSetter.SetColumnMoveArrows(cubeViewModel.LeftFaceViewModel, ArrowDirection.Bottom, sliceNumber);
                faceMoveSetter.SetColumnMoveArrows(cubeViewModel.UpFaceViewModel, ArrowDirection.Bottom, sliceNumber);
                break;

            case MoveDirection.RightTop:
                faceMoveSetter.SetColumnMoveArrows(cubeViewModel.RightFaceViewModel, ArrowDirection.Top, sliceNumber);

                faceMoveSetter.SetRowMoveArrows(
                    cubeViewModel.UpFaceViewModel,
                    ArrowDirection.Left,
                    row: GetReversedSliceNumber());

                break;

            case MoveDirection.RightBottom:
                faceMoveSetter.SetColumnMoveArrows(
                    cubeViewModel.RightFaceViewModel,
                    ArrowDirection.Bottom,
                    sliceNumber);

                faceMoveSetter.SetRowMoveArrows(
                    cubeViewModel.UpFaceViewModel,
                    ArrowDirection.Right,
                    row: GetReversedSliceNumber());

                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(moveDirection), moveDirection, null);
        }

        return;

        int GetReversedSliceNumber() => cubeViewModel.CubeDimension - sliceNumber - 1;
    }

    public void ClearMoveArrows(RubiksCubeControlViewModel cubeViewModel)
    {
        faceMoveSetter.ClearMoveArrows(cubeViewModel.UpFaceViewModel);
        faceMoveSetter.ClearMoveArrows(cubeViewModel.RightFaceViewModel);
        faceMoveSetter.ClearMoveArrows(cubeViewModel.LeftFaceViewModel);
    }
}