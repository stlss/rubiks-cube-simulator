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