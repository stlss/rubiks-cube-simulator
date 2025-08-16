namespace RubiksCubeSimulator.Application.RubiksCubeMover.MutableRubiksCube.Movers;

public interface ICounterclockwiseMutableRubiksCubeMover : IMutableRubiksCubeMover
{
}

internal sealed class CounterclockwiseMutableRubiksCubeMover(
    IClockwiseMutableRubiksCubeMover clockwiseMutableCubeMover)
    : ICounterclockwiseMutableRubiksCubeMover
{
    public void MoveMutableRubiksCubeUp(MutableRubiksCube cube, int sliceNumber)
    {
        var reversedSliceNumber = ReverseSliceNumber(sliceNumber, cube.Dimension);
        clockwiseMutableCubeMover.MoveMutableRubiksCubeDown(cube, reversedSliceNumber);
    }

    public void MoveMutableRubiksCubeRight(MutableRubiksCube cube, int sliceNumber)
    {
        var reversedSliceNumber = ReverseSliceNumber(sliceNumber, cube.Dimension);
        clockwiseMutableCubeMover.MoveMutableRubiksCubeLeft(cube, reversedSliceNumber);
    }

    public void MoveMutableRubiksCubeFront(MutableRubiksCube cube, int sliceNumber)
    {
        var reversedSliceNumber = ReverseSliceNumber(sliceNumber, cube.Dimension);
        clockwiseMutableCubeMover.MoveMutableRubiksCubeBack(cube, reversedSliceNumber);
    }


    public void MoveMutableRubiksCubeDown(MutableRubiksCube cube, int sliceNumber)
    {
        var reversedSliceNumber = ReverseSliceNumber(sliceNumber, cube.Dimension);
        clockwiseMutableCubeMover.MoveMutableRubiksCubeUp(cube, reversedSliceNumber);
    }

    public void MoveMutableRubiksCubeLeft(MutableRubiksCube cube, int sliceNumber)
    {
        var reversedSliceNumber = ReverseSliceNumber(sliceNumber, cube.Dimension);
        clockwiseMutableCubeMover.MoveMutableRubiksCubeRight(cube, reversedSliceNumber);
    }

    public void MoveMutableRubiksCubeBack(MutableRubiksCube cube, int sliceNumber)
    {
        var reversedSliceNumber = ReverseSliceNumber(sliceNumber, cube.Dimension);
        clockwiseMutableCubeMover.MoveMutableRubiksCubeFront(cube, reversedSliceNumber);
    }


    private static int ReverseSliceNumber(int sliceNumber, int cubeDimension) => cubeDimension - sliceNumber - 1;
}