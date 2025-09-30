namespace RubiksCubeSimulator.Application.RubiksCubeMover.MutableRubiksCube.Movers;

internal interface ICounterclockwiseMutableRubiksCubeMover : IMutableRubiksCubeMover
{
}

internal sealed class CounterclockwiseMutableRubiksCubeMover(
    IClockwiseMutableRubiksCubeMover clockwiseMutableCubeMover)
    : ICounterclockwiseMutableRubiksCubeMover
{
    public void MoveUp(MutableRubiksCube cube, int sliceNumber)
    {
        var reversedSliceNumber = ReverseSliceNumber(sliceNumber, cube.Dimension);
        clockwiseMutableCubeMover.MoveDown(cube, reversedSliceNumber);
    }

    public void MoveRight(MutableRubiksCube cube, int sliceNumber)
    {
        var reversedSliceNumber = ReverseSliceNumber(sliceNumber, cube.Dimension);
        clockwiseMutableCubeMover.MoveLeft(cube, reversedSliceNumber);
    }

    public void MoveFront(MutableRubiksCube cube, int sliceNumber)
    {
        var reversedSliceNumber = ReverseSliceNumber(sliceNumber, cube.Dimension);
        clockwiseMutableCubeMover.MoveBack(cube, reversedSliceNumber);
    }


    public void MoveDown(MutableRubiksCube cube, int sliceNumber)
    {
        var reversedSliceNumber = ReverseSliceNumber(sliceNumber, cube.Dimension);
        clockwiseMutableCubeMover.MoveUp(cube, reversedSliceNumber);
    }

    public void MoveLeft(MutableRubiksCube cube, int sliceNumber)
    {
        var reversedSliceNumber = ReverseSliceNumber(sliceNumber, cube.Dimension);
        clockwiseMutableCubeMover.MoveRight(cube, reversedSliceNumber);
    }

    public void MoveBack(MutableRubiksCube cube, int sliceNumber)
    {
        var reversedSliceNumber = ReverseSliceNumber(sliceNumber, cube.Dimension);
        clockwiseMutableCubeMover.MoveFront(cube, reversedSliceNumber);
    }


    private static int ReverseSliceNumber(int sliceNumber, int cubeDimension) => cubeDimension - sliceNumber - 1;
}