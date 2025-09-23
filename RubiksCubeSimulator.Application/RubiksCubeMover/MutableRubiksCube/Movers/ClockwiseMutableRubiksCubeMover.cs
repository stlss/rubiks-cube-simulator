using RubiksCubeSimulator.Application.Extensions;

namespace RubiksCubeSimulator.Application.RubiksCubeMover.MutableRubiksCube.Movers;

internal interface IClockwiseMutableRubiksCubeMover : IMutableRubiksCubeMover
{
}

internal sealed class ClockwiseMutableRubiksCubeMover : IClockwiseMutableRubiksCubeMover
{
    public void MoveMutableRubiksCubeUp(MutableRubiksCube cube, int sliceNumber)
    {
        var frontColors = cube.FrontFace.StickerColors;
        var leftColors = cube.LeftFace.StickerColors;
        var backColors = cube.BackFace.StickerColors;
        var rightColors = cube.RightFace.StickerColors;

        var copyRightColors = Enumerable.Range(0, cube.Dimension)
            .Select(j => rightColors[sliceNumber, j])
            .ToArray();

        var reversedSliceNumber = ReverseSliceNumber(sliceNumber, cube.Dimension);

        for (var i = cube.Dimension - 1; i >= 0; i--)
        {
            rightColors[sliceNumber, i] = backColors[sliceNumber, i];
            backColors[sliceNumber, i] = leftColors[sliceNumber, i];
            leftColors[sliceNumber, i] = frontColors[sliceNumber, i];
            frontColors[sliceNumber, i] = copyRightColors[i];
        }

        if (sliceNumber == 0) cube.UpFace.StickerColors.ClockwiseRotateSquareMatrix();
        else if (reversedSliceNumber == 0) cube.DownFace.StickerColors.CounterclockwiseRotateSquareMatrix();
    }

    public void MoveMutableRubiksCubeDown(MutableRubiksCube cube, int sliceNumber)
    {
        var frontColors = cube.FrontFace.StickerColors;
        var rightColors = cube.RightFace.StickerColors;
        var backColors = cube.BackFace.StickerColors;
        var leftColors = cube.LeftFace.StickerColors;

        var reversedSliceNumber = ReverseSliceNumber(sliceNumber, cube.Dimension);

        var copyLeftColors = Enumerable.Range(0, cube.Dimension)
            .Select(i => leftColors[reversedSliceNumber, i])
            .ToArray();

        for (var i = 0; i < cube.Dimension; i++)
        {
            leftColors[reversedSliceNumber, i] = backColors[reversedSliceNumber, i];
            backColors[reversedSliceNumber, i] = rightColors[reversedSliceNumber, i];
            rightColors[reversedSliceNumber, i] = frontColors[reversedSliceNumber, i];
            frontColors[reversedSliceNumber, i] = copyLeftColors[i];
        }

        if (sliceNumber == 0) cube.DownFace.StickerColors.ClockwiseRotateSquareMatrix();
        else if (reversedSliceNumber == 0) cube.UpFace.StickerColors.CounterclockwiseRotateSquareMatrix();
    }


    public void MoveMutableRubiksCubeRight(MutableRubiksCube cube, int sliceNumber)
    {
        var frontColors = cube.FrontFace.StickerColors;
        var upColors = cube.UpFace.StickerColors;
        var backColors = cube.BackFace.StickerColors;
        var downColors = cube.DownFace.StickerColors;

        var reversedSliceNumber = ReverseSliceNumber(sliceNumber, cube.Dimension);

        var copyDownColors = Enumerable.Range(0, cube.Dimension)
            .Select(i => downColors[i, reversedSliceNumber])
            .ToArray();

        for (var i = cube.Dimension - 1; i >= 0; i--)
        {
            downColors[i, reversedSliceNumber] = backColors[ReverseIndex(i), sliceNumber];
            backColors[ReverseIndex(i), sliceNumber] = upColors[i, reversedSliceNumber];
            upColors[i, reversedSliceNumber] = frontColors[i, reversedSliceNumber];
            frontColors[i, reversedSliceNumber] = copyDownColors[i];
        }

        if (sliceNumber == 0) cube.RightFace.StickerColors.ClockwiseRotateSquareMatrix();
        else if (reversedSliceNumber == 0) cube.LeftFace.StickerColors.CounterclockwiseRotateSquareMatrix();

        return;

        int ReverseIndex(int index) => cube.Dimension - index - 1;
    }

    public void MoveMutableRubiksCubeLeft(MutableRubiksCube cube, int sliceNumber)
    {
        var frontColors = cube.FrontFace.StickerColors;
        var downColors = cube.DownFace.StickerColors;
        var backColors = cube.BackFace.StickerColors;
        var upColors = cube.UpFace.StickerColors;

        var reversedSliceNumber = ReverseSliceNumber(sliceNumber, cube.Dimension);

        var copyUpColors = Enumerable.Range(0, cube.Dimension)
            .Select(i => upColors[i, sliceNumber])
            .ToArray();

        for (var i = 0; i < cube.Dimension; i++)
        {
            upColors[i, sliceNumber] = backColors[ReverseIndex(i), reversedSliceNumber];
            backColors[ReverseIndex(i), reversedSliceNumber] = downColors[i, sliceNumber];
            downColors[i, sliceNumber] = frontColors[i, sliceNumber];
            frontColors[i, sliceNumber] = copyUpColors[i];
        }

        if (sliceNumber == 0) cube.LeftFace.StickerColors.ClockwiseRotateSquareMatrix();
        else if (reversedSliceNumber == 0) cube.RightFace.StickerColors.CounterclockwiseRotateSquareMatrix();

        return;

        int ReverseIndex(int index) => cube.Dimension - index - 1;
    }


    public void MoveMutableRubiksCubeFront(MutableRubiksCube cube, int sliceNumber)
    {
        var rightColors = cube.RightFace.StickerColors;
        var downColors = cube.DownFace.StickerColors;
        var leftColors = cube.LeftFace.StickerColors;
        var upColors = cube.UpFace.StickerColors;

        var reversedSliceNumber = ReverseSliceNumber(sliceNumber, cube.Dimension);

        var copyUpColors = Enumerable.Range(0, cube.Dimension)
            .Select(i => upColors[reversedSliceNumber, i])
            .ToArray();

        for (var i = 0; i < cube.Dimension; i++)
        {
            upColors[reversedSliceNumber, i] = leftColors[ReverseIndex(i), reversedSliceNumber];
            leftColors[ReverseIndex(i), reversedSliceNumber] = downColors[sliceNumber, ReverseIndex(i)];
            downColors[sliceNumber, ReverseIndex(i)] = rightColors[i, sliceNumber];
            rightColors[i, sliceNumber] = copyUpColors[i];
        }

        if (sliceNumber == 0) cube.FrontFace.StickerColors.ClockwiseRotateSquareMatrix();
        else if (reversedSliceNumber == 0) cube.BackFace.StickerColors.CounterclockwiseRotateSquareMatrix();

        return;

        int ReverseIndex(int index) => cube.Dimension - index - 1;
    }

    public void MoveMutableRubiksCubeBack(MutableRubiksCube cube, int sliceNumber)
    {
        var rightColors = cube.RightFace.StickerColors;
        var upColors = cube.UpFace.StickerColors;
        var leftColors = cube.LeftFace.StickerColors;
        var downColors = cube.DownFace.StickerColors;

        var reversedSliceNumber = ReverseSliceNumber(sliceNumber, cube.Dimension);

        var copyDownColors = Enumerable.Range(0, cube.Dimension)
            .Select(i => downColors[reversedSliceNumber, i])
            .ToArray();

        for (var i = 0; i < cube.Dimension; i++)
        {
            downColors[reversedSliceNumber, i] = leftColors[i, sliceNumber];
            leftColors[i, sliceNumber] = upColors[sliceNumber, ReverseIndex(i)];
            upColors[sliceNumber, ReverseIndex(i)] = rightColors[ReverseIndex(i), reversedSliceNumber];
            rightColors[ReverseIndex(i), reversedSliceNumber] = copyDownColors[i];
        }

        if (sliceNumber == 0) cube.BackFace.StickerColors.ClockwiseRotateSquareMatrix();
        else if (reversedSliceNumber == 0) cube.FrontFace.StickerColors.CounterclockwiseRotateSquareMatrix();

        return;

        int ReverseIndex(int index) => cube.Dimension - index - 1;
    }


    private static int ReverseSliceNumber(int sliceNumber, int cubeDimension) => cubeDimension - sliceNumber - 1;
}