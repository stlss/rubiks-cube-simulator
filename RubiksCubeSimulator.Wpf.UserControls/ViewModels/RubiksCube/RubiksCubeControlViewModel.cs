namespace RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

public sealed class RubiksCubeControlViewModel
{
    public int CubeDimension { get; init; } = 3;

    public RubiksCubeFaceControlViewModel UpFaceViewModel { get; init; } = new();

    public RubiksCubeFaceControlViewModel RightFaceViewModel { get; init; } = new();

    public RubiksCubeFaceControlViewModel LeftFaceViewModel { get; init; } = new();


    public void ClearMoveArrows()
    {
        UpFaceViewModel.ClearMoveArrows();
        RightFaceViewModel.ClearMoveArrows();
        LeftFaceViewModel.ClearMoveArrows();
    }

    public void SetWholeCubeMoveArrows(MoveDirection moveDirection)
    {
        switch (moveDirection)
        {
            case MoveDirection.Left:
                LeftFaceViewModel.SetMoveArrows(ArrowDirection.Left);
                RightFaceViewModel.SetMoveArrows(ArrowDirection.Left);
                break;

            case MoveDirection.Right:
                LeftFaceViewModel.SetMoveArrows(ArrowDirection.Right);
                RightFaceViewModel.SetMoveArrows(ArrowDirection.Right);
                break;

            case MoveDirection.LeftTop:
                LeftFaceViewModel.SetMoveArrows(ArrowDirection.Top);
                UpFaceViewModel.SetMoveArrows(ArrowDirection.Top);
                break;

            case MoveDirection.LeftBottom:
                LeftFaceViewModel.SetMoveArrows(ArrowDirection.Bottom);
                UpFaceViewModel.SetMoveArrows(ArrowDirection.Bottom);
                break;

            case MoveDirection.RightTop:
                RightFaceViewModel.SetMoveArrows(ArrowDirection.Top);
                UpFaceViewModel.SetMoveArrows(ArrowDirection.Left);
                break;

            case MoveDirection.RightBottom:
                RightFaceViewModel.SetMoveArrows(ArrowDirection.Bottom);
                UpFaceViewModel.SetMoveArrows(ArrowDirection.Right);
                break;
        }
    }

    public void SetSliceMoveArrows(MoveDirection moveDirection, int sliceNumber)
    {
        switch (moveDirection)
        {
            case MoveDirection.Left:
                LeftFaceViewModel.SetRowMoveArrows(ArrowDirection.Left, sliceNumber);
                RightFaceViewModel.SetRowMoveArrows(ArrowDirection.Left, sliceNumber);
                break;

            case MoveDirection.Right:
                LeftFaceViewModel.SetRowMoveArrows(ArrowDirection.Right, sliceNumber);
                RightFaceViewModel.SetRowMoveArrows(ArrowDirection.Right, sliceNumber);
                break;

            case MoveDirection.LeftTop:
                LeftFaceViewModel.SetColumnMoveArrows(ArrowDirection.Top, sliceNumber);
                UpFaceViewModel.SetColumnMoveArrows(ArrowDirection.Top, sliceNumber);
                break;

            case MoveDirection.LeftBottom:
                LeftFaceViewModel.SetColumnMoveArrows(ArrowDirection.Bottom, sliceNumber);
                UpFaceViewModel.SetColumnMoveArrows(ArrowDirection.Bottom, sliceNumber);
                break;

            case MoveDirection.RightTop:
                RightFaceViewModel.SetColumnMoveArrows(ArrowDirection.Top, sliceNumber);
                UpFaceViewModel.SetRowMoveArrows(ArrowDirection.Left, GetReversedSliceNumber());
                break;

            case MoveDirection.RightBottom:
                RightFaceViewModel.SetColumnMoveArrows(ArrowDirection.Bottom, sliceNumber);
                UpFaceViewModel.SetRowMoveArrows(ArrowDirection.Right, GetReversedSliceNumber());
                break;
        }

        return;

        int GetReversedSliceNumber() => CubeDimension - sliceNumber - 1;
    }
}

public enum MoveDirection
{
    Left,
    Right,
    LeftTop,
    LeftBottom,
    RightTop,
    RightBottom,
}