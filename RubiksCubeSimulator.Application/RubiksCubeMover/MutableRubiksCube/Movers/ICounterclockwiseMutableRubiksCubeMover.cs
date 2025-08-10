namespace RubiksCubeSimulator.Application.RubiksCubeMover.MutableRubiksCube.Movers;

public interface ICounterclockwiseMutableRubiksCubeMover
{
    public MutableRubiksCube MoveMutableRubiksCubeUp(MutableRubiksCube cube, int sliceNumber);

    public MutableRubiksCube MoveMutableRubiksCubeRight(MutableRubiksCube cube, int sliceNumber);

    public MutableRubiksCube MoveMutableRubiksCubeFront(MutableRubiksCube cube, int sliceNumber);


    public MutableRubiksCube MoveMutableRubiksCubeDown(MutableRubiksCube cube, int sliceNumber);

    public MutableRubiksCube MoveMutableRubiksCubeLeft(MutableRubiksCube cube, int sliceNumber);

    public MutableRubiksCube MoveMutableRubiksCubeBack(MutableRubiksCube cube, int sliceNumber);
}

internal sealed class CounterclockwiseMutableRubiksCubeMover(
    IClockwiseMutableRubiksCubeMover clockwiseMutableCubeMover)
    : ICounterclockwiseMutableRubiksCubeMover
{
    public MutableRubiksCube MoveMutableRubiksCubeUp(MutableRubiksCube cube, int sliceNumber)
    {
        var reversedSliceNumber = GetReversedSliceNumber(sliceNumber, cube.Dimension);
        var movedCube = clockwiseMutableCubeMover.MoveMutableRubiksCubeDown(cube, reversedSliceNumber);

        return movedCube;
    }

    public MutableRubiksCube MoveMutableRubiksCubeRight(MutableRubiksCube cube, int sliceNumber)
    {
        var reversedSliceNumber = GetReversedSliceNumber(sliceNumber, cube.Dimension);
        var movedCube = clockwiseMutableCubeMover.MoveMutableRubiksCubeLeft(cube, reversedSliceNumber);

        return movedCube;
    }

    public MutableRubiksCube MoveMutableRubiksCubeFront(MutableRubiksCube cube, int sliceNumber)
    {
        var reversedSliceNumber = GetReversedSliceNumber(sliceNumber, cube.Dimension);
        var movedCube = clockwiseMutableCubeMover.MoveMutableRubiksCubeBack(cube, reversedSliceNumber);

        return movedCube;
    }


    public MutableRubiksCube MoveMutableRubiksCubeDown(MutableRubiksCube cube, int sliceNumber)
    {
        var reversedSliceNumber = GetReversedSliceNumber(sliceNumber, cube.Dimension);
        var movedCube = clockwiseMutableCubeMover.MoveMutableRubiksCubeUp(cube, reversedSliceNumber);

        return movedCube;
    }

    public MutableRubiksCube MoveMutableRubiksCubeLeft(MutableRubiksCube cube, int sliceNumber)
    {
        var reversedSliceNumber = GetReversedSliceNumber(sliceNumber, cube.Dimension);
        var movedCube = clockwiseMutableCubeMover.MoveMutableRubiksCubeRight(cube, reversedSliceNumber);

        return movedCube;
    }

    public MutableRubiksCube MoveMutableRubiksCubeBack(MutableRubiksCube cube, int sliceNumber)
    {
        var reversedSliceNumber = GetReversedSliceNumber(sliceNumber, cube.Dimension);
        var movedCube = clockwiseMutableCubeMover.MoveMutableRubiksCubeFront(cube, reversedSliceNumber);

        return movedCube;
    }


    private static int GetReversedSliceNumber(int sliceNumber, int cubeDimension) => cubeDimension - sliceNumber - 1;
}